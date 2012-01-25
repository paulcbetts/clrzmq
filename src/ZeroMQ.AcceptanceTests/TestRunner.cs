namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    class TestRunner
    {
        private const string Indent = "  ";
        private static readonly Regex SpecNamePattern = new Regex(@"[A-Z_][a-z]*", RegexOptions.Compiled);

        private readonly AcceptanceTest _test;
        private readonly string _testName;
        private readonly OutputFlags _flags;

        public TestRunner(AcceptanceTest test, OutputFlags outputFlags)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            _test = test;
            _testName = Specify(test.GetType().Name);
            _flags = outputFlags;
        }

        public TestResult Run()
        {
            IEnumerable<MemberInfo> specs = GetSpecs();

            var result = new TestResult { TotalSpecs = specs.Count() };

            Console.WriteLine(_testName);

            if (!Execute(() => _test.Setup(), "(Setup)"))
            {
                return result;
            }

            _test.ExecuteException = CaptureException(() => _test.Execute(), "(Execute)");

            foreach (MethodInfo methodInfo in specs)
            {
                if (Execute(() => methodInfo.Invoke(_test, null), Indent + Specify(methodInfo.Name)))
                {
                    result.Passed++;
                }
            }

            Console.WriteLine();

            return result;
        }

        private static bool Execute(Action action, string description)
        {
            try
            {
                action();

                Console.Out.WriteLine(description);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(description);
                Console.Error.WriteLine(MultilineIndent(Indent + Indent, ex.Message));

                return false;
            }

            return true;
        }

        private static Exception CaptureException(Action action, string description)
        {
            try
            {
                Console.Out.WriteLine(description);

                action();
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }

        private static string Specify(string name)
        {
            return string.Join(" ", SpecNamePattern.Matches(name).Cast<Match>().Select(match => match.Value.Replace("_", string.Empty)));
        }

        private static string MultilineIndent(string indent, string text)
        {
            return string.Join(Environment.NewLine, text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(s => indent + s));
        }

        private IEnumerable<MethodInfo> GetSpecs()
        {
            return _test.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance)
                .Where(m => m.HasCustomAttribute<SpecAttribute>());
        }
    }
}
