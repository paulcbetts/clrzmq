namespace ZeroMQ.AcceptanceTests.Fixtures
{
    abstract class UsingSocketPair : AcceptanceTest
    {
        protected ZmqContext Context;
        protected ZmqSocket Sender;
        protected ZmqSocket Receiver;

        private readonly SocketType _senderType;
        private readonly SocketType _receiverType;

        protected UsingSocketPair(SocketType senderType, SocketType receiverType)
        {
            _senderType = senderType;
            _receiverType = receiverType;
        }

        public override void Setup()
        {
            Context = ZmqContext.Create();
            Sender = Context.CreateSocket(_senderType);
            Receiver = Context.CreateSocket(_receiverType);
        }

        protected override void Dispose(bool disposing)
        {
            if (Sender != null)
            {
                Sender.Dispose();
            }

            if (Receiver != null)
            {
                Receiver.Dispose();
            }

            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
