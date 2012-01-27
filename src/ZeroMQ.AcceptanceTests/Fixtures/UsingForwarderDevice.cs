namespace ZeroMQ.AcceptanceTests.Fixtures
{
    using ZeroMQ.Devices;

    abstract class UsingForwarderDevice : UsingThreadedDevice<ForwarderDevice>
    {
        protected UsingForwarderDevice()
        {
            CreateSender = () => ZmqContext.CreateSocket(SocketType.PUB);
            CreateReceiver = () => ZmqContext.CreateSocket(SocketType.SUB);
        }
    }
}
