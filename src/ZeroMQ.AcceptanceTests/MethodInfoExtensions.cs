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
    }
}
