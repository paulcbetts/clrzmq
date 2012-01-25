namespace ZeroMQ.AcceptanceTests
{
    using System;
    using System.IO;

    class Program
    {
        private const string TeamCitySwitch = "/TeamCity";

        private static OutputFlags OutputFlags;

        public static void Main(string[] args)
        {
            OutputFlags = OutputFlags.Console;

            if (args.Length > 1 && args[1].Equals(TeamCitySwitch, StringComparison.OrdinalIgnoreCase))
            {
                OutputFlags |= OutputFlags.TeamCity;
            }

            var result = new TestResult();

            foreach (AcceptanceTest acceptanceTest in TestLocator.GetTests())
            {
                using (acceptanceTest)
                {
                    var runner = new TestRunner(acceptanceTest, OutputFlags);

                    result.AddResult(runner.Run());
                }
            }

            WriteResults(result.Failed > 0 ? Console.Error : Console.Out, result);
        }

        public static void WriteResults(TextWriter output, TestResult result)
        {
            string ignoredText = result.Ignored > 0 ? result.Ignored + " ignored, " : string.Empty;

            output.WriteLine("Results: {0} passed, {1} failed, {2}{3} total", result.Passed, result.Failed, ignoredText, result.TotalSpecs);
        }
    }
}
