namespace ZeroMQ.AcceptanceTests.Behaviors
{
    using System.Linq;
    using ZeroMQ.AcceptanceTests.Fixtures;

    abstract class SingleMessageReceived : UsingThreadedReqRep
    {
        protected Frame Message;
        protected SendStatus SendResult;

        [Spec]
        public void ItShouldBeSentSuccessfully()
        {
            Assert.Equal(SendStatus.Sent, SendResult);
        }

        [Spec]
        public virtual void ItShouldBeSuccessfullyReceived()
        {
            Assert.NotNull(Message);
            Assert.Equal(ReceiveStatus.Received, Message.ReceiveStatus);
        }

        [Spec]
        public void ItShouldContainTheGivenMessage()
        {
            Assert.Equal(Messages.SingleMessage.Buffer, Message.Buffer.Take(Message.MessageSize));
        }

        [Spec]
        public void ItShouldNotHaveMoreParts()
        {
            Assert.False(Message.HasMore);
        }

        [Spec]
        public void ItShouldSetTheActualMessageSize()
        {
            Assert.Equal(Messages.SingleMessage.MessageSize, Message.MessageSize);
        }
    }
}
