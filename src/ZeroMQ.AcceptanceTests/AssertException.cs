namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AssertException : Exception
    {
        public AssertException()
        {
        }

        public AssertException(string expected, string actual, Type expectedType, Type actualType)
            : this(FormatMessage(expected, actual, expectedType, actualType))
        {
        }

        public AssertException(string message)
            : base(message)
        {
        }

        public AssertException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AssertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        static string FormatMessage(string expected, string actual, Type expectedType, Type actualType)
        {
            return string.Format(
                "Expected: {0} [{1}]{2}Actual: {3} [{4}]",
                expected,
                expectedType.Name,
                Environment.NewLine,
                actual,
                actualType.Name);
        }
    }
}
