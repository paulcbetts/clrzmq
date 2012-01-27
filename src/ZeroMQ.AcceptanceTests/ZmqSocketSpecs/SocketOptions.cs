namespace ZeroMQ.AcceptanceTests.ZmqSocketSpecs
{
    using System;
    using ZeroMQ.AcceptanceTests.Behaviors;

    class WhenSettingTheAffinitySocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.Affinity = 0x03ul;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(0x03ul, Socket.Affinity);
        }
    }

    class WhenSettingTheBacklogSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.Backlog = 6;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(6, Socket.Backlog);
        }
    }

    class WhenSettingTheIdentitySocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.Identity = Messages.Identity;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(Messages.Identity, Socket.Identity);
        }
    }

    class WhenSettingTheLingerSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.Linger = TimeSpan.FromMilliseconds(333);
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(TimeSpan.FromMilliseconds(333), Socket.Linger);
        }
    }

    class WhenSettingTheMaxMessageSizeSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Socket.MaxMessageSize = 60000L;
            }
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Assert.Equal(60000L, Socket.MaxMessageSize);
            }
        }
    }

    class WhenSettingTheMulticastHopsSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Socket.MulticastHops = 6;
            }
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Assert.Equal(6, Socket.MulticastHops);
            }
        }
    }

    class WhenSettingTheMulticastRateSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.MulticastRate = 60;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(60, Socket.MulticastRate);
        }
    }

    class WhenSettingTheMulticastRecoveryIntervalSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.MulticastRecoveryInterval = TimeSpan.FromMilliseconds(333);
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(TimeSpan.FromMilliseconds(333), Socket.MulticastRecoveryInterval);
        }
    }

    class WhenSettingTheReceiveBufferSizeSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.ReceiveBufferSize = 10000;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(10000, Socket.ReceiveBufferSize);
        }
    }

    class WhenSettingTheReceiveHighWatermarkSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.ReceiveHighWatermark = 100;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(100, Socket.ReceiveHighWatermark);
        }
    }

    class WhenSettingTheReceiveTimeoutSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Socket.ReceiveTimeout = TimeSpan.FromMilliseconds(333);
            }
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Assert.Equal(TimeSpan.FromMilliseconds(333), Socket.ReceiveTimeout);
            }
        }
    }

    class WhenSettingTheReconnectIntervalSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.ReconnectInterval = TimeSpan.FromMilliseconds(333);
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(TimeSpan.FromMilliseconds(333), Socket.ReconnectInterval);
        }
    }

    class WhenSettingTheReconnectIntervalMaxSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.ReconnectIntervalMax = TimeSpan.FromMilliseconds(333);
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(TimeSpan.FromMilliseconds(333), Socket.ReconnectIntervalMax);
        }
    }

    class WhenSettingTheSendBufferSizeSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.SendBufferSize = 10000;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(10000, Socket.SendBufferSize);
        }
    }

    class WhenSettingTheSendHighWatermarkSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            Socket.SendHighWatermark = 100;
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            Assert.Equal(100, Socket.SendHighWatermark);
        }
    }

    class WhenSettingTheSendTimeoutSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Socket.SendTimeout = TimeSpan.FromMilliseconds(333);
            }
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Assert.Equal(TimeSpan.FromMilliseconds(333), Socket.SendTimeout);
            }
        }
    }

    class WhenSettingTheSupportedProtocolSocketOption : SocketOptionApplies
    {
        public override void Execute()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Socket.SupportedProtocol = ProtocolType.Both;
            }
        }

        [Spec]
        public void ItShouldReturnTheGivenValue()
        {
            if (ZmqVersion.Current.IsAtLeast(3))
            {
                Assert.Equal(ProtocolType.Both, Socket.SupportedProtocol);
            }
        }
    }
}
