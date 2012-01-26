namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.Linq;

    static class TypeExtensions
    {
        public static bool HasCustomAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.GetCustomAttributes(typeof(TAttribute), true).Any();
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return (TAttribute)type.GetCustomAttributes(typeof(TAttribute), true).Single();
        }
    }
}
