namespace ZeroMQ.AcceptanceTests
{
    struct TestResult
    {
        public int TotalSpecs;
        public int Passed;
        public int Ignored;

        public int Failed
        {
            get { return TotalSpecs - Passed - Ignored; }
        }

        public void AddResult(TestResult result)
        {
            TotalSpecs += result.TotalSpecs;
            Passed += result.Passed;
            Ignored += result.Ignored;
        }
    }
}
