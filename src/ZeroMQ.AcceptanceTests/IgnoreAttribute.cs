namespace ZeroMQ.AcceptanceTests
{
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    class IgnoreAttribute : Attribute
    {
        public IgnoreAttribute(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentNullException("reason");
            }

            Reason = reason;
        }

        public string Reason { get; private set; }
    }
}
