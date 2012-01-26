namespace ZeroMQ.AcceptanceTests.Fixtures
{
    abstract class UsingReqRep : UsingSocketPair
    {
        protected UsingReqRep()
            : base(SocketType.REQ, SocketType.REP)
        {
        }
    }
}
