using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSimulator.Domain
{
    public class TrafficReport : ITrafficReport
    {
        private readonly ILogger<TrafficReport> logger;

        public TrafficReport(ILogger<TrafficReport> logger)
        {
            this.logger = logger;
        }

        public void Report(int id, DateTime dateTime, float speed, TrafficCondition condition)
        {
            this.logger.LogInformation($"Report: DroneId {id}, DateTime {dateTime.ToString("MM/dd/yyyy HH:mm:ss")}, Speed {speed}, TrafficCondition: {condition}");
        }
    }
}
