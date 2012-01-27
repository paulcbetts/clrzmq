namespace ZeroMQ.AcceptanceTests.Behaviors
{
    using ZeroMQ.AcceptanceTests.Fixtures;

    abstract class SubscriptionMessagesReceived : UsingThreadedPubSub
    {
        protected Frame Message1;
        protected Frame Message2;
        protected SendStatus SendResult1;
        protected SendStatus SendResult2;

        [Spec]
        public void ItShouldSendTheFirstMessageSuccessfully()
        {
            Assert.Equal(SendStatus.Sent, SendResult1);
        }

        [Spec]
        public void ItShouldSendTheSecondMessageSuccessfully()
        {
            Assert.Equal(SendStatus.Sent, SendResult2);
        }

        [Spec]
        public void ItShouldReceiveTheFirstMessageSuccessfully()
        {
            Assert.NotNull(Message1);
        }

        [Spec]
        public void ItShouldContainTheCorrectFirstMessageData()
        {
            Assert.Equal(Messages.PubSubFirst, Message1);
        }

        [Spec]
        public void ItShouldNotHaveMorePartsAfterTheFirstMessage()
        {
            Assert.False(Message1.HasMore);
        }

        [Spec]
        public void ItShouldTellReceiveTheSecondMessageSuccessfully()
        {
            Assert.Equal(ReceiveStatus.Received, Message2.ReceiveStatus);
        }

        [Spec]
        public void ItShouldContainTheCorrectSecondMessageData()
        {
            Assert.Equal(Messages.PubSubSecond, Message2);
        }

        [Spec]
        public void ItShouldNotHaveMorePartsAfterTheSecondMessage()
        {
            Assert.False(Message2.HasMore);
        }
    }
}
