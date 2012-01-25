namespace ZeroMQ.AcceptanceTests
{
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
    }
}
