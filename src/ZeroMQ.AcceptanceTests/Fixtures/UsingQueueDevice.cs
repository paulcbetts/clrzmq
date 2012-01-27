namespace ZeroMQ.AcceptanceTests.Fixtures
{
    using ZeroMQ.Devices;

    abstract class UsingQueueDevice : UsingThreadedDevice<QueueDevice>
    {
        protected UsingQueueDevice()
        {
            CreateSender = () => ZmqContext.CreateSocket(SocketType.REQ);
            CreateReceiver = () => ZmqContext.CreateSocket(SocketType.REP);
        }
    }
}
