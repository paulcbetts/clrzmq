namespace ZeroMQ.AcceptanceTests.ZmqSocketSpecs
{
    using System;
    using System.Threading;
    using ZeroMQ.AcceptanceTests;
    using ZeroMQ.AcceptanceTests.Behaviors;
    using SingleMessageNotReceived = ZeroMQ.AcceptanceTests.Behaviors.SingleMessageNotReceived;
    using SingleMessageReceived = ZeroMQ.AcceptanceTests.Behaviors.SingleMessageReceived;

    class WhenTransferringInBlockingMode : SingleMessageReceived
    {
        protected override void SenderAction()
        {
            SendResult = Sender.SendFrame(Messages.SingleMessage);
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveFrame();
        }
    }

    class WhenTransferringWithAnAmpleReceiveTimeout : SingleMessageReceived
    {
        protected override void SenderAction()
        {
            Thread.Sleep(500);
            SendResult = Sender.SendFrame(Messages.SingleMessage);
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(2000));
        }
    }

    class WhenTransferringWithAnInsufficientReceiveTimeout : SingleMessageNotReceived
    {
        protected override void SenderAction()
        {
            SendResult = Sender.SendFrame(Messages.SingleMessage);
            Thread.Sleep(10);
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(1));
        }
    }

    class WhenTransferringWithAnAmpleExternalReceiveBuffer : SingleMessageReceived
    {
        protected Frame Buffer;

        [Spec]
        public void ShouldReturnTheSuppliedBuffer()
        {
            Assert.Same(Buffer, Message);
        }

        protected override void SenderAction()
        {
            SendResult = Sender.SendFrame(Messages.SingleMessage);
        }

        protected override void ReceiverAction()
        {
            Buffer = new Frame(256);
            Message = Receiver.ReceiveFrame(Buffer);
        }
    }

    class WhenTransferringWithAnUndersizedExternalReceiveBuffer : SingleMessageReceived
    {
        protected static Frame Buffer;

        protected override void SenderAction()
        {
            SendResult = Sender.SendFrame(Messages.SingleMessage);
        }

        protected override void ReceiverAction()
        {
            Buffer = new Frame(1);
            Message = Receiver.ReceiveFrame(Buffer);
        }
    }

    class WhenTransferringWithAPreallocatedReceiveBuffer : SingleMessageReceived
    {
        protected static int Size;

        [Spec]
        public override void ItShouldBeSuccessfullyReceived()
        {
            Assert.NotNull(Message);
        }

        protected override void SenderAction()
        {
            SendResult = Sender.SendFrame(Messages.SingleMessage);
        }

        protected override void ReceiverAction()
        {
            Message = new Frame(100);

            Size = Receiver.Receive(Message.Buffer);
            Message.MessageSize = Size;
        }
    }

    class WhenTransferringMultipartMessages : MultiPartMessageReceived
    {
        [Spec]
        public void ItShouldBeACompleteMessage()
        {
            Assert.True(Message.IsComplete);
        }

        [Spec]
        public void ItShouldNotBeAnEmptyMessage()
        {
            Assert.False(Message.IsEmpty);
        }

        [Spec]
        public void ItShouldContainTheCorrectNumberOfFrames()
        {
            Assert.Equal(2, Message.FrameCount);
        }

        [Spec]
        public void ItShouldContainTheCorrectNumberOfBytes()
        {
            Assert.Equal(Messages.MultiFirst.MessageSize + Messages.MultiLast.MessageSize, Message.TotalSize);
        }

        protected override void SenderAction()
        {
            SendResult1 = SendResult2 = Sender.SendMessage(new ZmqMessage(new[] { Messages.MultiFirst, Messages.MultiLast }));
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveMessage();
        }
    }
}
