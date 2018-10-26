using System;
using System.Diagnostics;

namespace Birdy.Util
{
    public class EasyTimer : IDisposable
    {
        private Stopwatch stopwatch;
        private bool stdOut;

        public string Name { get; private set; }

        public EasyTimer(string name, bool stdOut = true, bool autoStart = true)
        {
            this.stdOut = stdOut;
            this.Name = name;
            if(autoStart){
                Start();
            }
        }

        public void Start()
        {
            stopwatch = Stopwatch.StartNew();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public TimeSpan GetElaspedTime()
        {
            return stopwatch.Elapsed;
        }

        public void Dispose()
        {
            if (stopwatch != null && stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }

            if (stdOut)
            {
                Console.WriteLine($"Timer: {Name} ended with {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}