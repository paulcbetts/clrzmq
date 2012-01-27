namespace ZeroMQ.AcceptanceTests.DeviceSpecs
{
    using System;
    using ZeroMQ.AcceptanceTests.Behaviors;

    class WhenUsingQueueDeviceToSendASingleMessageInBlockingMode : QueueMessageReceived
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

    class WhenUsingQueueDeviceToSendASingleMessageWithAnAmpleTimeout : QueueMessageReceived
    {
        protected override void SenderAction()
        {
            SendResult = Sender.SendFrame(Messages.SingleMessage, TimeSpan.FromMilliseconds(2000));
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(2000));
        }
    }

    class WhenUsingQueueDeviceToReceiveASingleMessageWithInsufficientTimeout : QueueMessageNotReceived
    {
        protected override void SenderAction()
        {
            SendResult = Sender.SendFrame(Messages.SingleMessage);
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveFrame(TimeSpan.FromMilliseconds(0));
        }
    }

    class WhenUsingQueueDeviceToSendAMultipartMessageInBlockingMode : QueueMultiPartMessageReceived
    {
        protected override void SenderAction()
        {
            SendResult1 = Sender.SendFrame(Messages.MultiFirst);
            SendResult2 = Sender.SendFrame(Messages.MultiLast);
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveMessage();
        }
    }

    class WhenUsingQueueDeviceToSendAMultipartMessageWithAnAmpleTimeout : QueueMultiPartMessageReceived
    {
        protected override void SenderAction()
        {
            SendResult1 = Sender.SendFrame(Messages.MultiFirst, TimeSpan.FromMilliseconds(2000));
            SendResult2 = Sender.SendFrame(Messages.MultiLast, TimeSpan.FromMilliseconds(2000));
        }

        protected override void ReceiverAction()
        {
            Message = Receiver.ReceiveMessage(TimeSpan.FromMilliseconds(2000));
        }
    }
}
