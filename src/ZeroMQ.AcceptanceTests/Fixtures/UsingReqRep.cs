namespace ZeroMQ.AcceptanceTests.Fixtures
{
    abstract class UsingReqRep : AcceptanceTest
    {
        protected ZmqSocket Req;
        protected ZmqSocket Rep;
        protected ZmqContext ZmqContext;

        public override void Setup()
        {
            ZmqContext = ZmqContext.Create();
            Req = ZmqContext.CreateSocket(SocketType.REQ);
            Rep = ZmqContext.CreateSocket(SocketType.REP);
        }

        protected override void Dispose(bool disposing)
        {
            Req.Dispose();
            Rep.Dispose();
            ZmqContext.Dispose();
        }
    }
}
