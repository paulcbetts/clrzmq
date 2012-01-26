namespace ZeroMQ.AcceptanceTests
{
    using System;

    static class Assert
    {
        public static void Equal<T>(T expected, T actual)
        {
            if (expected == null && actual != null)
            {
                throw new AssertException("null", actual.ToString(), typeof(T), typeof(T));
            }

            if (expected != null && actual == null)
            {
                throw new AssertException(expected.ToString(), "null", typeof(T), typeof(T));
            }

            if (expected == null && actual == null)
            {
                return;
            }

            if (!actual.Equals(expected))
            {
                throw new AssertException(expected.ToString(), actual.ToString(), typeof(T), typeof(T));
            }
        }

        public static void Null<T>(T actual) where T : class
        {
            if (actual != null)
            {
                throw new AssertException("null", actual.ToString(), typeof(T), typeof(T));
            }
        }

        public static void TypeOf<T>(object actual)
        {
            if (actual.GetType() != typeof(T))
            {
                throw new AssertException("Expected: [" + typeof(T).Name + "]" + Environment.NewLine + "Actual: [" + actual.GetType().Name + "]");
            }
        }

        public static void Contains(string expected, string actual)
        {
            if (!actual.Contains(expected))
            {
                throw new AssertException("Expected to contain: " + expected + Environment.NewLine + "Actual: " + actual);
            }
        }
    }
}
