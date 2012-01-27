namespace ZeroMQ.AcceptanceTests.Fixtures
{
    using System.Threading;

    abstract class UsingThreadedSocketPair : AcceptanceTest
    {
        protected ZmqContext Context;
        protected ZmqSocket Sender;
        protected ZmqSocket Receiver;

        private readonly ManualResetEvent _receiverReady = new ManualResetEvent(false);

        private readonly SocketType _senderType;
        private readonly SocketType _receiverType;

        private Thread _senderThread;
        private Thread _receiverThread;

        protected UsingThreadedSocketPair(SocketType senderType, SocketType receiverType)
        {
            _senderType = senderType;
            _receiverType = receiverType;
        }

        public override void Setup()
        {
            Context = ZmqContext.Create();
            Sender = Context.CreateSocket(_senderType);
            Receiver = Context.CreateSocket(_receiverType);

            _senderThread = new Thread(() =>
            {
                InitSender();
                Sender.SendHighWatermark = 1;
                _receiverReady.WaitOne();
                Sender.Connect("inproc://spec_context");
                SenderAction();
            });

            _receiverThread = new Thread(() =>
            {
                InitReceiver();
                Receiver.SendHighWatermark = 1;
                Receiver.Bind("inproc://spec_context");
                _receiverReady.Set();
                ReceiverAction();
            });
        }

        public override void Execute()
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

        protected virtual void InitSender()
        {
        }

        protected virtual void InitReceiver()
        {
        }

        protected abstract void SenderAction();
        protected abstract void ReceiverAction();

        protected override void Dispose(bool disposing)
        {
            Sender.Dispose();
            Receiver.Dispose();
            Context.Dispose();
        }
    }
}
