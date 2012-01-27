namespace ZeroMQ.AcceptanceTests.Behaviors
{
    using ZeroMQ.AcceptanceTests.Fixtures;

    abstract class ForwarderMessageFiltered : UsingForwarderDevice
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
        public void ItShouldTellReceiverToRetryTheSecondMessage()
        {
            Assert.Equal(ReceiveStatus.TryAgain, Message2.ReceiveStatus);
        }

        [Spec]
        public void ItShouldNotHaveMorePartsAfterTheSecondMessage()
        {
            Assert.False(Message2.HasMore);
        }
    }
}
