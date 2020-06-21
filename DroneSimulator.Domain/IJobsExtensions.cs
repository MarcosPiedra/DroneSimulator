using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneSimulator.Domain
{
    public static class IJobsExtensions
    {
        public static void RunContinuestly(this IJob job, int delayInSeconds, CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    var sw = Stopwatch.StartNew();
                    await job.ExecuteAsync();
                    var delay = Math.Max(0, (delayInSeconds * 1000) - sw.ElapsedMilliseconds);
                    await Task.Delay((int)delay);
                }
            });
        }
    }
}
