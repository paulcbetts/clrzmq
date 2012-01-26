namespace ZeroMQ.AcceptanceTests.ZmqSocketSpecs
{
    using System;
    using System.Threading;
    using ZeroMQ.AcceptanceTests;
    using SingleMessageNotReceived = ZeroMQ.AcceptanceTests.Behaviors.SingleMessageNotReceived;
    using SingleMessageReceived = ZeroMQ.AcceptanceTests.Behaviors.SingleMessageReceived;

    class WhenTransferringInBlockingMode : SingleMessageReceived
    {
        public WhenTransferringInBlockingMode()
        {
            SenderAction = req => SendResult = req.SendFrame(Messages.SingleMessage);
            ReceiverAction = rep => Message = rep.ReceiveFrame();
        }
    }

    class WhenTransferringWithAnAmpleReceiveTimeout : SingleMessageReceived
    {
        public WhenTransferringWithAnAmpleReceiveTimeout()
        {
            SenderAction = req =>
            {
                Thread.Sleep(500);
                SendResult = req.SendFrame(Messages.SingleMessage);
            };

            ReceiverAction = rep => Message = rep.ReceiveFrame(TimeSpan.FromMilliseconds(2000));
        }
    }

    class WhenTransferringWithAnInsufficientReceiveTimeout : SingleMessageNotReceived
    {
        public WhenTransferringWithAnInsufficientReceiveTimeout()
        {
            SenderAction = req =>
            {
                SendResult = req.SendFrame(Messages.SingleMessage);
                Thread.Sleep(10);
            };

            ReceiverAction = rep => Message = rep.ReceiveFrame(TimeSpan.FromMilliseconds(1));
        }
    }

    class WhenTransferringWithAnAmpleExternalReceiveBuffer : SingleMessageReceived
    {
        protected Frame Buffer;

        public WhenTransferringWithAnAmpleExternalReceiveBuffer()
        {
            SenderAction = req => SendResult = req.SendFrame(Messages.SingleMessage);

            Buffer = new Frame(256);
            ReceiverAction = rep => Message = rep.ReceiveFrame(Buffer);
        }

        [Spec]
        public void ShouldReturnTheSuppliedBuffer()
        {
            Assert.Same(Buffer, Message);
        }
    }

    class WhenTransferringWithAnUndersizedExternalReceiveBuffer : SingleMessageReceived
    {
        protected static Frame Buffer;

        public WhenTransferringWithAnUndersizedExternalReceiveBuffer()
        {
            SenderAction = req => SendResult = req.SendFrame(Messages.SingleMessage);

            Buffer = new Frame(1);
            ReceiverAction = rep => Message = rep.ReceiveFrame(Buffer);
        }
    }

    class WhenTransferringWithAPreallocatedReceiveBuffer : SingleMessageReceived
    {
        protected static int Size;

        public WhenTransferringWithAPreallocatedReceiveBuffer()
        {
            SenderAction = req => SendResult = req.SendFrame(Messages.SingleMessage);

            Message = new Frame(100);
            ReceiverAction = rep =>
            {
                Size = rep.Receive(Message.Buffer);
                Message.MessageSize = Size;
            };
        }

        [Spec]
        public override void ItShouldBeSuccessfullyReceived()
        {
            Assert.NotNull(Message);
        }
    }
}
