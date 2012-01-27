namespace ZeroMQ.AcceptanceTests.Behaviors
{
    using System.Linq;
    using ZeroMQ.AcceptanceTests.Fixtures;

    abstract class MultiPartMessageReceived : UsingThreadedReqRep
    {
        protected ZmqMessage Message;
        protected SendStatus SendResult1;
        protected SendStatus SendResult2;

        [Spec]
        public void ItShouldSendTheFirstFrameSuccessfully()
        {
            Assert.Equal(SendStatus.Sent, SendResult1);
        }

        [Spec]
        public void ItShouldSendTheSecondFrameSuccessfully()
        {
            Assert.Equal(SendStatus.Sent, SendResult2);
        }

        [Spec]
        public void ItShouldReceiveAllMessageParts()
        {
            Assert.Equal(2, Message.FrameCount);
        }

        [Spec]
        public void ItShouldContainTheCorrectFirstFrameData()
        {
            Assert.Equal(Messages.MultiFirst, Message.First());
        }

        [Spec]
        public void ItShouldHaveMorePartsAfterTheFirstFrame()
        {
            Assert.True(Message.First().HasMore);
        }

        [Spec]
        public void ItShouldContainTheCorrectSecondFrameData()
        {
            Assert.Equal(Messages.MultiLast, Message.Last());
        }

        [Spec]
        public void ItShouldNotHaveMorePartsAfterTheSecondFrame()
        {
            Assert.False(Message.Last().HasMore);
        }
    }
}
