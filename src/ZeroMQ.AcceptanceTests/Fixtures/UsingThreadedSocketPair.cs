namespace ZeroMQ.AcceptanceTests.Fixtures
{
    using System;
    using System.Threading;

    abstract class UsingThreadedSocketPair : AcceptanceTest
    {
        protected Func<ZmqSocket> CreateSender;
        protected Func<ZmqSocket> CreateReceiver;

        protected ZmqContext Context;
        protected ZmqSocket Sender;
        protected ZmqSocket Receiver;

        protected Action<ZmqSocket> SenderInit;
        protected Action<ZmqSocket> SenderAction;
        protected Action<ZmqSocket> ReceiverInit;
        protected Action<ZmqSocket> ReceiverAction;

        private readonly ManualResetEvent _receiverReady = new ManualResetEvent(false);

        private readonly SocketType _senderType;
        private readonly SocketType _receiverType;

        private Thread _senderThread;
        private Thread _receiverThread;

        protected UsingThreadedSocketPair(SocketType senderType, SocketType receiverType)
        {
            _senderType = senderType;
            _receiverType = receiverType;

            SenderInit = sck => { };
            ReceiverInit = sck => { };
            SenderAction = sck => { };
            ReceiverAction = sck => { };
        }

        public override void Setup()
        {
            Context = ZmqContext.Create();
            Sender = Context.CreateSocket(_senderType);
            Receiver = Context.CreateSocket(_receiverType);

            _senderThread = new Thread(() =>
            {
                SenderInit(Sender);
                Sender.SendHighWatermark = 1;
                _receiverReady.WaitOne();
                Sender.Connect("inproc://spec_context");
                SenderAction(Sender);
            });

            _receiverThread = new Thread(() =>
            {
                ReceiverInit(Receiver);
                Receiver.SendHighWatermark = 1;
                Receiver.Bind("inproc://spec_context");
                _receiverReady.Set();
                ReceiverAction(Receiver);
            });
        }

        protected void StartThreads()
        {
            _receiverThread.Start();
            _senderThread.Start();

            if (!_receiverThread.Join(5000))
            {
                _receiverThread.Abort();
            }

            if (!_senderThread.Join(5000))
            {
                _senderThread.Abort();
            }
        }

        protected override void Dispose(bool disposing)
        {
            Sender.Dispose();
            Receiver.Dispose();
            Context.Dispose();
        }
    }
}
