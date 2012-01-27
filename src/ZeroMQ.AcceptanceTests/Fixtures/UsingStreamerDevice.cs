namespace ZeroMQ.AcceptanceTests.Fixtures
{
    using ZeroMQ.Devices;

    abstract class UsingStreamerDevice : UsingThreadedDevice<StreamerDevice>
    {
        protected UsingStreamerDevice()
        {
            CreateSender = () => ZmqContext.CreateSocket(SocketType.PUSH);
            CreateReceiver = () => ZmqContext.CreateSocket(SocketType.PULL);
        }
    }
}
