namespace ZeroMQ.AcceptanceTests.ZmqSocketSpecs
{
    using System;
    using System.Threading;
    using ZeroMQ.AcceptanceTests;
    using ZeroMQ.AcceptanceTests.Behaviors;

    class WhenSubscribingToASpecificPrefix : SubscriptionMessageFiltered
    {
        public WhenSubscribingToASpecificPrefix()
        {
            var signal = new ManualResetEvent(false);

            ReceiverInit = sub => sub.Subscribe(Messages.PubSubPrefix);

            ReceiverAction = sub =>
            {
                signal.Set();

                Message1 = sub.ReceiveFrame();
                Message2 = sub.ReceiveFrame(TimeSpan.FromMilliseconds(500));
            };

            SenderInit = pub => signal.WaitOne(1000);

            SenderAction = pub =>
            {
                SendResult1 = pub.SendFrame(Messages.PubSubFirst);
                SendResult2 = pub.SendFrame(Messages.PubSubSecond);
            };
        }
    }

    class WhenSubscribingToAllPrefixes : SubscriptionMessagesReceived
    {
        public WhenSubscribingToAllPrefixes()
        {
            var signal = new ManualResetEvent(false);

            ReceiverInit = sub => sub.Subscribe(new byte[0]);

            ReceiverAction = sub =>
            {
                signal.Set();

                Message1 = sub.ReceiveFrame();
                Message2 = sub.ReceiveFrame(TimeSpan.FromMilliseconds(500));
            };

            SenderInit = pub => signal.WaitOne(1000);

            SenderAction = pub =>
            {
                SendResult1 = pub.SendFrame(Messages.PubSubFirst);
                SendResult2 = pub.SendFrame(Messages.PubSubSecond);
            };
        }
    }
}
