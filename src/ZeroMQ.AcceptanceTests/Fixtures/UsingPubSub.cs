namespace ZeroMQ.AcceptanceTests.Fixtures
{
    abstract class UsingPubSub : UsingSocketPair
    {
        protected UsingPubSub()
            : base(SocketType.PUB, SocketType.SUB)
        {
        }
    }
}
