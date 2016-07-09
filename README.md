# EESW Guide Messaging

A wrapper around [ZeroMQ](http://zeromq.org/) that I wrote in 2011 while providing technical support to an [EESW](http://www.stemcymru.org.uk/) project during my Year in Industry with General Dynamics in South Wales.

This wrapper was designed to abstract away the complexities of setting up ZeroMQ, polling for messages in a background thread, and invoking the (student-)attached callbacks correctly for them to work smoothly with .NET GUI applications. This allowed the students to focus on developing the operational logic of the software component for their project.
