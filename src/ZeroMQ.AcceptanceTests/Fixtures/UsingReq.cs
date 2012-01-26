namespace ZeroMQ.AcceptanceTests.Fixtures
{
    abstract class UsingReq : AcceptanceTest
    {
        protected ZmqSocket Socket;
        protected ZmqContext Context;

        public override void Setup()
        {
            Context = ZmqContext.Create();
            Socket = Context.CreateSocket(SocketType.REQ);
        }

        protected override void Dispose(bool disposing)
        {
            if (Socket != null)
            {
                Socket.Dispose();
            }

            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
