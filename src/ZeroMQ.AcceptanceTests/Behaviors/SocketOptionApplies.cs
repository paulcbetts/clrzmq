namespace ZeroMQ.AcceptanceTests.Behaviors
{
    using ZeroMQ.AcceptanceTests.Fixtures;

    abstract class SocketOptionApplies : UsingReq
    {
        [Spec]
        public void ItShouldNotFail()
        {
            Assert.Null(ExecuteException);
        }
    }
}
