namespace ZeroMQ.AcceptanceTests.ZmqSocketSpecs
{
    using System;
    using System.Threading;
    using ZeroMQ.AcceptanceTests;
    using ZeroMQ.AcceptanceTests.Behaviors;

    class WhenSubscribingToASpecificPrefix : SubscriptionMessageFiltered
    {
        readonly ManualResetEvent _signal = new ManualResetEvent(false);

        protected override void InitSender()
        {
            _signal.WaitOne(1000);
        }

        protected override void InitReceiver()
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
            _signal.Set();

            Message1 = Receiver.ReceiveFrame();
            Message2 = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(500));
        }
    }

    class WhenSubscribingToAllPrefixes : SubscriptionMessagesReceived
    {
        readonly ManualResetEvent _signal = new ManualResetEvent(false);

        protected override void InitSender()
        {
            _signal.WaitOne(1000);
        }

        protected override void InitReceiver()
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
            _signal.Set();

            Message1 = Receiver.ReceiveFrame();
            Message2 = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(500));
        }
    }
}
