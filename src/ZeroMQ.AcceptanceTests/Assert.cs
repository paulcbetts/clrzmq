namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerStepThrough]
    static class Assert
    {
        public static void Equal<T>(T expected, T actual)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (typeof(T).IsClass)
            {
                if (expected == null && actual != null)
                {
                    throw new AssertException("null", actual.ToString(), typeof(T), typeof(T));
                }

                if (expected != null && actual == null)
                {
                    throw new AssertException(expected.ToString(), "null", typeof(T), typeof(T));
                }

                if (actual == null)
                {
                    return;
                }
            }

            var equatable = actual as IEquatable<T>;

            if (equatable != null && equatable.Equals(expected))
            {
                return;
            }

            if (!actual.Equals(expected))
            {
                throw new AssertException(expected.ToString(), actual.ToString(), typeof(T), typeof(T));
            }

            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        public static void Equal<T>(T[] expected, T[] actual)
        {
            Equal((IEnumerable<T>)expected, actual);
        }

        public static void Equal<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            IEnumerator<T> expectedEnumerator = expected.GetEnumerator();
            IEnumerator<T> actualEnumerator = actual.GetEnumerator();

            while (expectedEnumerator.MoveNext() && actualEnumerator.MoveNext())
            {
                Equal(expectedEnumerator.Current, actualEnumerator.Current);
            }
        }

        public static void Null<T>(T actual) where T : class
        {
            if (actual != null)
            {
                throw new AssertException("null", actual.ToString(), typeof(T), typeof(T));
            }
        }

        public static void NotNull<T>(T actual) where T : class
        {
            if (actual == null)
            {
                throw new AssertException("not null", "null", typeof(T), typeof(T));
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

        public static void True(bool actual)
        {
            if (!actual)
            {
                throw new AssertException("true", "false", typeof(bool), typeof(bool));
            }
        }

        public static void False(bool actual)
        {
            if (actual)
            {
                throw new AssertException("false", "true", typeof(bool), typeof(bool));
            }
        }

        public static void Empty<T>(T[] array)
        {
            if (array.Length > 0)
            {
                throw new AssertException("empty", "not empty", typeof(T[]), typeof(T[]));
            }
        }

        public static void Same(object expected, object actual)
        {
            if (!ReferenceEquals(expected, actual))
            {
                throw new AssertException("objects are the same", "objects are different", expected.GetType(), actual.GetType());
            }
        }
    }
}
