namespace ZeroMQ.AcceptanceTests.Behaviors
{
    using ZeroMQ.AcceptanceTests.Fixtures;

    abstract class StreamerMessageNotReceived : UsingStreamerDevice
    {
        protected Frame Message;
        protected SendStatus SendResult;

        [Spec]
        public void ItShouldBeSentSuccessfully()
        {
            Assert.Equal(SendStatus.Sent, SendResult);
        }

        [Spec]
        public void ItShouldNotBeSuccessfullyReceived()
        {
            Assert.NotNull(Message);
            Assert.Equal(ReceiveStatus.TryAgain, Message.ReceiveStatus);
        }

        [Spec]
        public void ItShouldNotContainTheGivenMessage()
        {
            Assert.Equal(0, Message.MessageSize);
        }

        [Spec]
        public void ItShouldNotHaveMoreParts()
        {
            Assert.False(Message.HasMore);
        }
    }
}
