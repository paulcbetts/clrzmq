namespace ZeroMQ.AcceptanceTests.Fixtures
{
    abstract class UsingThreadedReqRep : UsingThreadedSocketPair
    {
        protected UsingThreadedReqRep()
            : base(SocketType.REQ, SocketType.REP)
        {
        }
    }
}
