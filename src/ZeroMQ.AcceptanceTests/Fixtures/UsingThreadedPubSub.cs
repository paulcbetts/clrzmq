namespace ZeroMQ.AcceptanceTests.Fixtures
{
    abstract class UsingThreadedPubSub : UsingThreadedSocketPair
    {
        protected UsingThreadedPubSub()
            : base(SocketType.PUB, SocketType.SUB)
        {
        }
    }
}
