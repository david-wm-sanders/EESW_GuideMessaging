// Licensed under the New BSD License. See LICENSE.txt

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using ZMQ;

namespace GuideMessaging
{
    /// <summary>
    /// Command Centre
    /// </summary>
    public class CommandCentre
    {
        #region Private Variables

        private readonly SynchronizationContext _syncContext;

        private Context _publisherContext;
        private Socket _publisherSocket;

        private Context _receiverContext;
        private Socket _receiverSocket;
        private PollItem[] _receiverPollItems;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the Command Centre, using the specified .txt file.
        /// </summary>
        /// <param name="configFile">A .txt file where the first line is the publisherPortNumber and the second line is the receiverPortNumber.</param>
        public CommandCentre(string configFile) : this(File.ReadAllLines(configFile)[0], File.ReadAllLines(configFile)[1]) {}

        /// <summary>
        /// Initializes the Command Centre, setting up a publishing socket and
        /// a receiving socket, and begins waiting for incoming reports.
        /// </summary>
        /// <param name="publisherPortNumber">The port, between 49152 and 65535, to which the publisher should be bound.</param>
        /// <param name="receiverPortNumber">The port, between 49152 and 65535, to which the receiver should be bound.</param>
        public CommandCentre(string publisherPortNumber, string receiverPortNumber)
        {
            // Get the current SynchronizationContext. This allows the library 
            // to work with multi-threaded .NET GUI applications without requiring 
            // third-party library users to invoke functions to update the UI.
            _syncContext = AsyncOperationManager.SynchronizationContext;

            try
            {
                #region Publisher Setup

                _publisherContext = new Context(1);
                _publisherSocket = _publisherContext.Socket(SocketType.PUB);
                _publisherSocket.Bind(String.Format("tcp://*:{0}", publisherPortNumber));

                #endregion

                #region Receiver Setup

                _receiverContext = new Context(1);
                _receiverSocket = _receiverContext.Socket(SocketType.XREP);
                _receiverSocket.Bind(String.Format("tcp://*:{0}", receiverPortNumber));

                // Create a PollItem array, create a poll item for the
                // receiver socket in the zeroth element of the array
                // and set up an event for the PollInHandler.
                _receiverPollItems = new PollItem[1];
                _receiverPollItems[0] = _receiverSocket.CreatePollItem(IOMultiPlex.POLLIN);
                _receiverPollItems[0].PollInHandler += new PollHandler(CommandCentre_PollInHandler);

                #endregion
            }
            catch (ZMQ.Exception ex)
            {
                #region Log and throw an error.

                switch (ex.Errno)
                {
                    case 22:
                        ErrorLogging.LogError(
                            String.Format("CommandCentre: ZMQ Exception: Invalid argument. publisherPortNumber = {0}, receiverPortNumber = {1}", publisherPortNumber, receiverPortNumber));
                        throw new ArgumentException("Invalid argument. Please use values between 49152 and 65535 for both publisherPortNumber and receiverPortNumber.", ex);

                    case 156384717:
                        ErrorLogging.LogError(
                            String.Format("CommandCentre: ZMQ Exception: Address in use. publisherPortNumber == receiverPortNumber"));
                        throw new ArgumentException("Address already in use: receiverPortNumber must be different from publisherPortNumber.", ex);

                    default:
                        ErrorLogging.LogError(String.Format("CommandCentre: ZMQ Exception: {0} : {1}", ex.Errno, ex.Message));
                        throw;
                }

                #endregion
            }

            #region Waiting Thread Setup

            // Start a background task in a separate thread that waits for reports to arrive.
            Thread waitingThread = new Thread(new ThreadStart(WaitForReport));
            waitingThread.IsBackground = true;
            waitingThread.Start();

            #endregion
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Publish a message to all connected Units.
        /// </summary>
        /// <param name="message">Serialized/encoded message data.</param>
        public void Send(byte[] message)
        {
            _publisherSocket.Send(message);
        }

        /// <summary>
        /// Encodes the provided string using UTF-16, and publishes it to all connected Units.
        /// No automatic decoding is done by the Unit when it receives a message sent using this command.
        /// Use <code>Encoding.Unicode.GetString(message.Data)</code> on the Unit-side event to decode the message.
        /// </summary>
        /// <param name="message">Message string.</param>
        public void Send(string message)
        {
            byte[] msg = Encoding.Unicode.GetBytes(message);
            _publisherSocket.Send(msg);
        }

        #endregion

        #region Events

        /// <summary>
        /// Fired when a report is received.
        /// </summary>
        public event MessageReceivedEventHandler ReportReceived;

        private void OnNewReportReceived(NewMessageEventArgs e)
        {
            if (ReportReceived != null)
            {
                ReportReceived(this, e);
            }
        }

        void CommandCentre_PollInHandler(Socket socket, IOMultiPlex revents)
        {
            byte[] intriguingJunk = _receiverSocket.Recv();
            byte[] msg = _receiverSocket.Recv();
            var args = new NewMessageEventArgs(msg);
            _syncContext.Post(e => OnNewReportReceived((NewMessageEventArgs)e), args);
        }

        #endregion

        #region Waiting Thread

        private void WaitForReport()
        {
            while (true)
            {
                _receiverContext.Poll(_receiverPollItems, -1);
            }
        }

        #endregion
    }

    /// <summary>
    /// Unit
    /// </summary>
    public class Unit
    {
        #region Private Variables

        private readonly SynchronizationContext _syncContext;

        private Context _subscriberContext;
        private Socket _subscriberSocket;
        private PollItem[] _subscriberPollItems;

        private Context _senderContext;
        private Socket _senderSocket;
        private PollItem[] _senderPollItems;

        private byte[] _message;
        //private object _happyLock = new object();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the Unit, using the specified .txt file.
        /// </summary>
        /// <param name="configFile">A .txt file where the first line is the IP, the second line is the subscriberPortNumber and the third line is the senderPortNumber.</param>
        public Unit(string configFile) : this(File.ReadAllLines(configFile)[0], File.ReadAllLines(configFile)[1], File.ReadAllLines(configFile)[2]) { }

        /// <summary>
        /// Initializes the Unit, setting up a subscribing socket and
        /// a sending socket, and begins waiting on incoming alerts.
        /// </summary>
        /// <param name="IP">The IP Address of the Command Centre.</param>
        /// <param name="subscriberPortNumber">The port to which the subscriber should be connected.</param>
        /// <param name="senderPortNumber">The port to which the sender should be connected.</param>
        public Unit(string IP, string subscriberPortNumber, string senderPortNumber)
        {
            // Get the current SynchronizationContext. This allows the library 
            // to work with multi-threaded .NET GUI applications without requiring 
            // third-party library users to invoke functions to update the UI.
            _syncContext = AsyncOperationManager.SynchronizationContext;

            try
            {
                #region Subscriber Setup

                _subscriberContext = new Context(1);
                _subscriberSocket = _subscriberContext.Socket(SocketType.SUB);
                // Blanket subscribe to all incoming alerts.
                _subscriberSocket.Subscribe("", Encoding.Unicode);
                _subscriberSocket.Connect(String.Format("tcp://{0}:{1}", IP, subscriberPortNumber));

                // Create a PollItem array, create a poll item for the 
                // subscriber socket in the zeroth element  of the array
                // and set up an event for the PollInHandler.
                _subscriberPollItems = new PollItem[1];
                _subscriberPollItems[0] = _subscriberSocket.CreatePollItem(IOMultiPlex.POLLIN);
                _subscriberPollItems[0].PollInHandler += new PollHandler(Unit_PollInHandler);

                #endregion

                #region Sender Setup

                _senderContext = new Context(1);
                _senderSocket = _senderContext.Socket(SocketType.XREQ);
                _senderSocket.Connect(String.Format("tcp://{0}:{1}", IP, senderPortNumber));

                // Create a PollItem array, create a poll item for the
                // sender socket in the zeroth element of the array
                // and set up an event for the PollOutHandler.
                _senderPollItems = new PollItem[1];
                _senderPollItems[0] = _senderSocket.CreatePollItem(IOMultiPlex.POLLOUT);
                _senderPollItems[0].PollOutHandler += new PollHandler(Unit_PollOutHandler);

                #endregion
            }
            catch (ZMQ.Exception ex)
            {
                #region Log and throw an error.

                switch (ex.Errno)
                {
                    case 22:
                        ErrorLogging.LogError(
                            String.Format("Unit: ZMQ Exception: Invalid argument. IP = {0}, subscriberPortNumber = {1}, receiverPortNumber = {2}", IP, subscriberPortNumber, senderPortNumber));
                        throw new ArgumentException("Invalid argument. Please make sure the IP is valid and that you use values between 49152 and 65535 for both subscriberPortNumber and senderPortNumber.", ex);

                    default:
                        ErrorLogging.LogError(String.Format("Unit: ZMQ Exception: {0} : {1}", ex.Errno, ex.Message));
                        throw;
                }

                #endregion
            }

            #region Waiting Thread Setup

            // Start a background task in a separate thread that waits for alerts to arrive.
            Thread waitingThread = new Thread(new ThreadStart(WaitForAlert));
            waitingThread.IsBackground = true;
            waitingThread.Start();

            #endregion
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Send a message to the Command Centre.
        /// </summary>
        /// <param name="message">Serialized/encoded message data.</param>
        public void Send(byte[] message)
        {
            //lock (_happyLock)
            //{
            //    _message = message;
            //    _senderContext.Poll(_senderPollItems, 0);
            //}
            _message = message;
            _senderContext.Poll(_senderPollItems, 0);
        }

        /// <summary>
        /// Encodes the provided string using UTF-16, and sends it to the Command Centre.
        /// No automatic decoding is done by the Command Centre when it receives a message sent using this command.
        /// Use <code>Encoding.Unicode.GetString(message.Data)</code> on the CC-side event to decode the message.
        /// </summary>
        /// <param name="message">Message string.</param>
        public void Send(string message)
        {
            byte[] msg = Encoding.Unicode.GetBytes(message);
            //lock (_happyLock)
            //{
            //    _message = msg;
            //    _senderContext.Poll(_senderPollItems, 0);
            //}
            _message = msg;
            _senderContext.Poll(_senderPollItems, 0);
        }

        #endregion

        #region Events

        /// <summary>
        /// Fired when an alert is received.
        /// </summary>
        public event MessageReceivedEventHandler AlertReceived;

        private void OnNewAlertReceived(NewMessageEventArgs e)
        {
            if (AlertReceived != null)
            {
                AlertReceived(this, e);
            }
        }

        void Unit_PollInHandler(Socket socket, IOMultiPlex revents)
        {
            byte[] msg = socket.Recv();
            var args = new NewMessageEventArgs(msg);
            _syncContext.Post(e => OnNewAlertReceived((NewMessageEventArgs)e), args);
        }

        void Unit_PollOutHandler(Socket socket, IOMultiPlex revents)
        {
            //lock (_happyLock)
            //{
            //    socket.Send(_message);
            //}
            socket.Send(_message);
        }

        #endregion

        #region Waiting Thread

        private void WaitForAlert()
        {
            while (true)
            {
                _subscriberContext.Poll(_subscriberPollItems, -1);
            }
        }

        #endregion
    }

    #region MessageReceivedEventHandler

    /// <summary>
    /// The event handler for received messages (alerts/reports).
    /// </summary>
    /// <param name="sender">The CommandCentre or Unit instance that received the message.</param>
    /// <param name="message">NewMessageEventArgs containing the message data.</param>
    public delegate void MessageReceivedEventHandler(object sender, NewMessageEventArgs message);

    #endregion

    #region NewMessageEventArgs

    /// <summary>
    /// EventArgs derivative that stores the message data received.
    /// </summary>
    public class NewMessageEventArgs : EventArgs
    {
        private byte[] _data;

        /// <summary>
        /// The message data received.
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// NewMessageEventArgs constructor, that takes the received message data as a parameter.
        /// </summary>
        /// <param name="data"></param>
        public NewMessageEventArgs(byte[] data)
        {
            _data = data;
        }
    }

    #endregion

    #region Error Logging

    /// <summary>
    /// This class provides quick and easy error logging functionality.
    /// </summary>
    public static class ErrorLogging
    {
        internal static void LogError(string message)
        {
            StreamWriter sw = File.AppendText("GuideMessaging_ErrorLog.txt");

            try
            {
                string logLine = String.Format("{0:G}: {1}", DateTime.Now, message);
                sw.WriteLine(logLine);
            }
            finally { sw.Close(); }
        }
    }

    #endregion
}
