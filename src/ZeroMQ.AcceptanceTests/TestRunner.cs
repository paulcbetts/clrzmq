namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    class TestRunner
    {
        private const string Indent = "    ";
        private const string SpecPrefix = " -> ";
        private const string FailedSuffix = " - FAILED";

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

            Exception setupException = CaptureException(() => _test.Setup());

            if (setupException != null)
            {
                WriteException("Setup", setupException);
                return result;
            }

            _test.ExecuteException = CaptureException(() => _test.Execute());

            foreach (MethodInfo methodInfo in specs)
            {
                if (ExecuteSpec(() => methodInfo.Invoke(_test, null), SpecPrefix + Specify(methodInfo.Name)))
                {
                    result.Passed++;
                }
            }

            Console.WriteLine();

            return result;
        }

        private static bool ExecuteSpec(Action action, string description)
        {
            Exception ex = CaptureException(action);

            if (ex == null)
            {
                Console.Out.WriteLine(description);
                return true;
            }

            WriteException(description + FailedSuffix, ex);
            return false;
        }

        private static Exception CaptureException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                return ex.InnerException ?? ex;
            }

            return null;
        }

        private static void WriteException(string description, Exception ex)
        {
            Console.Error.WriteLine(description);
            Console.Error.WriteLine(MultilineIndent(ex.Message));
        }

        private static string Specify(string name)
        {
            return string.Join(" ", SpecNamePattern.Matches(name).Cast<Match>().Select(match => match.Value.Replace("_", string.Empty)));
        }

        private static string MultilineIndent(string text)
        {
            return string.Join(Environment.NewLine, text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(s => Indent + s));
        }

        private IEnumerable<MethodInfo> GetSpecs()
        {
            return _test.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance)
                .Where(m => m.HasCustomAttribute<SpecAttribute>());
        }
    }
}
