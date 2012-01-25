namespace ZeroMQ.AcceptanceTests
{
    using System;

    abstract class AcceptanceTest : IDisposable
    {
        ~AcceptanceTest()
        {
            Dispose(false);
        }

        public Exception ExecuteException { get; set; }

        public abstract void Setup();

        public abstract void Execute();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected abstract void Dispose(bool disposing);
    }
}
