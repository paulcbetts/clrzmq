namespace ZeroMQ.AcceptanceTests.DeviceSpecs
{
    using System;
    using ZeroMQ.AcceptanceTests.Behaviors;

    class WhenUsingForwarderDeviceWithFullSubscription : ForwarderMessagesReceived
    {
        protected override void DeviceInit()
        {
            Device.FrontendSetup.SubscribeAll();
        }

        protected override void ReceiverInit()
        {
            Receiver.SubscribeAll();
        }

        protected override void SenderAction()
        {
            SendResult1 = Sender.SendFrame(Messages.PubSubFirst);
            SendResult2 = Sender.SendFrame(Messages.PubSubSecond);
        }

        protected override void ReceiverAction()
        {
            Message1 = Receiver.ReceiveFrame();
            Message2 = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(50));
        }
    }

    class WhenUsingForwarderDeviceWithAReceiverSubscription : ForwarderMessageFiltered
    {
        protected override void DeviceInit()
        {
            Device.FrontendSetup.SubscribeAll();
        }

        protected override void ReceiverInit()
        {
            Receiver.Subscribe(Messages.PubSubPrefix);
        }

        protected override void SenderAction()
        {
            SendResult1 = Sender.SendFrame(Messages.PubSubFirst);
            SendResult2 = Sender.SendFrame(Messages.PubSubSecond);
        }

        protected override void ReceiverAction()
        {
            Message1 = Receiver.ReceiveFrame();
            Message2 = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(50));
        }
    }

    class WhenUsingForwarderDeviceWithADeviceSubscription : ForwarderMessageFiltered
    {
        protected override void DeviceInit()
        {
            Device.FrontendSetup.Subscribe(Messages.PubSubPrefix);
        }

        protected override void ReceiverInit()
        {
            Receiver.SubscribeAll();
        }

        protected override void SenderAction()
        {
            SendResult1 = Sender.SendFrame(Messages.PubSubFirst);
            SendResult2 = Sender.SendFrame(Messages.PubSubSecond);
        }

        protected override void ReceiverAction()
        {
            Message1 = Receiver.ReceiveFrame();
            Message2 = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(50));
        }
    }
}
