namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    static class TestLocator
    {
        public static IEnumerable<AcceptanceTest> GetTests()
        {
            return GetTests(Assembly.GetExecutingAssembly());
        }

        public static IEnumerable<AcceptanceTest> GetTests(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => typeof(AcceptanceTest).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract)
                .Select(ConstructTest);
        }

        private static AcceptanceTest ConstructTest(Type testType)
        {
            return (AcceptanceTest)Activator.CreateInstance(testType, true);
        }
    }
}
