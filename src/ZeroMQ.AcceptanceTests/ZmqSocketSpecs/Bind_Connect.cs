namespace ZeroMQ.AcceptanceTests.ZmqSocketSpecs
{
    using System;
    using System.Threading;
    using ZeroMQ.AcceptanceTests.Fixtures;

    class WhenBindingAndConnectingToATcpIpAddressAndPort : UsingReqRep
    {
        public override void Execute()
        {
            Receiver.Bind("tcp://127.0.0.1:9000");
            Sender.Connect("tcp://127.0.0.1:9000");
        }

        [Spec]
        public void ItShouldNotFail()
        {
            Assert.Null(ExecuteException);
        }
    }

    class WhenBindingToATcpPortAndConnectingToAddressAndPort : UsingReqRep
    {
        public override void Execute()
        {
            Receiver.Bind("tcp://*:9000");
            Sender.Connect("tcp://127.0.0.1:9000");
        }

        [Spec]
        public void ItShouldNotFail()
        {
            Assert.Null(ExecuteException);
        }
    }

    class WhenBindingAndConnectingToANamedInprocAddress : UsingReqRep
    {
        public override void Execute()
        {
            Receiver.Bind("inproc://named");
            Sender.Connect("inproc://named");
        }

        [Spec]
        public void ItShouldNotFail()
        {
            Assert.Null(ExecuteException);
        }
    }

    [Ignore("PGM is broken - LIBZMQ-303")]
    class WhenConnectingToAPgmSocketWithPubAndSub : UsingPubSub
    {
        public override void Execute()
        {
            Sender.Linger = TimeSpan.Zero;
            Sender.Connect("epgm://127.0.0.1;239.192.1.1:5000");

            Receiver.Connect("epgm://127.0.0.1;239.192.1.1:5000");

            // TODO: Is there any other way to ensure the PGM thread has started?
            Thread.Sleep(100);
        }

        [Spec]
        public void ItShouldNotFail()
        {
            Assert.Null(ExecuteException);
        }
    }

    [Ignore("PGM is broken - LIBZMQ-303")]
    class WhenConnectingToAPgmSocketWithAnIncompatibleSocketType : UsingReq
    {
        public override void Execute()
        {
            Socket.Connect("epgm://127.0.0.1;239.192.1.1:5000");
        }

        [Spec]
        public void ItShouldFailBecausePgmIsNotSupported()
        {
            Assert.TypeOf<ZmqSocketException>(ExecuteException);
        }

        [Spec]
        public void ItShouldHaveAnErrorCodeOfEnocompatproto()
        {
            Assert.Equal(ErrorCode.ENOCOMPATPROTO, ((ZmqException)ExecuteException).ErrorCode);
        }

        [Spec]
        public void ItShouldHaveASpecificErrorMessage()
        {
            Assert.Contains("protocol is not compatible with the socket type", ExecuteException.Message);
        }
    }

#if !UNIX
    class WhenBindingToAnIpcAddress : UsingReq
    {
        public override void Execute()
        {
            Socket.Bind("ipc:///tmp/testsock");
        }

        [Spec]
        public void ItShouldFailBecauseIpcIsNotSupportedOnWindows()
        {
            Assert.TypeOf<ZmqSocketException>(ExecuteException);
        }

        [Spec]
        public void ItShouldHaveAnErrorCodeOfEprotonosupport()
        {
            Assert.Equal(ErrorCode.EPROTONOSUPPORT, ((ZmqException)ExecuteException).ErrorCode);
        }

        [Spec]
        public void ItShouldHaveASpecificErrorMessage()
        {
            Assert.Contains("Protocol not supported", ExecuteException.Message);
        }
    }

    class WhenConnectingToAnIpcAddress : UsingReq
    {
        public override void Execute()
        {
            Socket.Connect("ipc:///tmp/testsock");
        }

        [Spec]
        public void ItShouldFailBecauseIpcIsNotSupportedOnWindows()
        {
            Assert.TypeOf<ZmqSocketException>(ExecuteException);
        }

        [Spec]
        public void ItShouldHaveAnErrorCodeOfEprotonosupport()
        {
            Assert.Equal(ErrorCode.EPROTONOSUPPORT, ((ZmqException)ExecuteException).ErrorCode);
        }

        [Spec]
        public void ItShouldHaveASpecificErrorMessage()
        {
            Assert.Contains("Protocol not supported", ExecuteException.Message);
        }
    }
#else
    class WhenBindingAndConnectingToAnIpcAddress : UsingReqRep
    {
        public override void Execute()
        {
            Receiver.Bind("ipc:///tmp/testsock");
            Sender.Connect("ipc:///tmp/testsock");
        }

        [Spec]
        public void ItShouldNotFail()
        {
            Assert.Null(ExecuteException);
        }
    }
#endif
}
