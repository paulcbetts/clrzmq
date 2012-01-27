namespace ZeroMQ.AcceptanceTests.Fixtures
{
    using System;
    using System.Threading;
    using ZeroMQ.Devices;

    abstract class UsingThreadedDevice<TDevice> : AcceptanceTest
        where TDevice : Device
    {
        protected const string FrontendAddr = "inproc://dev_frontend";
        protected const string BackendAddr = "inproc://dev_backend";

        protected static Func<ZmqSocket> CreateSender;
        protected static Func<ZmqSocket> CreateReceiver;

        protected static ZmqSocket Sender;
        protected static ZmqSocket Receiver;
        protected static TDevice Device;
        protected static ZmqContext ZmqContext;

        private static Thread DeviceThread;
        private static Thread ReceiverThread;
        private static Thread SenderThread;

        private static ManualResetEvent DeviceReady;
        private static ManualResetEvent ReceiverSignal;

        public override void Setup()
        {
            ZmqContext = ZmqContext.Create();
            Device = (TDevice)Activator.CreateInstance(typeof(TDevice), ZmqContext, FrontendAddr, BackendAddr);

            Sender = CreateSender();
            Receiver = CreateReceiver();

            DeviceReady = new ManualResetEvent(false);
            ReceiverSignal = new ManualResetEvent(false);

            DeviceThread = new Thread(StartDevice);
            ReceiverThread = new Thread(StartReceiver);
            SenderThread = new Thread(StartSender);
        }

        public override void Execute()
        {
            DeviceThread.Start();
            ReceiverThread.Start();
            SenderThread.Start();

            if (!ReceiverThread.Join(5000))
            {
                ReceiverThread.Abort();
            }

            if (!SenderThread.Join(5000))
            {
                SenderThread.Abort();
            }

            Device.Stop();

            if (!DeviceThread.Join(5000))
            {
                DeviceThread.Abort();
            }
        }

        protected virtual void DeviceInit()
        {
        }

        protected virtual void SenderInit()
        {
        }

        protected virtual void ReceiverInit()
        {
        }

        protected abstract void SenderAction();
        protected abstract void ReceiverAction();

        protected void StartDevice()
        {
            DeviceInit();
            Device.Initialize();

            Device.Start();

            // XXX: This is a hack until a better method of guaranteeing device readiness is discovered
            Thread.Sleep(100);

            DeviceReady.Set();
        }

        protected void StartReceiver()
        {
            DeviceReady.WaitOne();

            ReceiverInit();
            Receiver.ReceiveHighWatermark = 1;
            Receiver.Connect(BackendAddr);

            ReceiverSignal.Set();

            ReceiverAction();
        }

        protected void StartSender()
        {
            ReceiverSignal.WaitOne();

            SenderInit();
            Sender.SendHighWatermark = 1;
            Sender.Connect(FrontendAddr);

            SenderAction();
        }

        protected override void Dispose(bool disposing)
        {
            if (Sender != null)
            {
                Sender.Dispose();
            }

            if (Receiver != null)
            {
                Receiver.Dispose();
            }
            
            if (Device != null)
            {
                Device.Dispose();
            }

            if (ZmqContext != null)
            {
                ZmqContext.Dispose();
            }
        }
    }
}
