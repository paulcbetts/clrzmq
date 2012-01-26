namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.Linq;
    using System.Reflection;

    static class MethodInfoExtensions
    {
        public static bool HasCustomAttribute<TAttribute>(this MethodInfo methodInfo) where TAttribute : Attribute
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            return methodInfo.GetCustomAttributes(typeof(TAttribute), true).Any();
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this MethodInfo methodInfo) where TAttribute : Attribute
        {
            return (TAttribute)methodInfo.GetCustomAttributes(typeof(TAttribute), true).Single();
        }
    }
}
