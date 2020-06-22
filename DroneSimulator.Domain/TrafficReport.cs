using DroneSimulator.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DroneSimulator.Domain
{
    public class TrafficReport : ITrafficReport
    {
        private readonly ILogger<TrafficReport> logger;
        private readonly ReportConfig reportConfig;

        public TrafficReport(ILogger<TrafficReport> logger, ReportConfig reportConfig)
        {
            this.logger = logger;
            this.reportConfig = reportConfig;
            if (!string.IsNullOrEmpty(reportConfig.OutoutFilename))
            {
                if (File.Exists(reportConfig.OutoutFilename))
                    File.Delete(reportConfig.OutoutFilename);
                File.WriteAllText(reportConfig.OutoutFilename, "");
            }
        }

        public void Report(int id, DateTime dateTime, float speed, TrafficCondition condition)
        {
            var logInfo = $"Report: DroneId {id}, DateTime {dateTime.ToString("MM/dd/yyyy HH:mm:ss")}, Speed {speed}, TrafficCondition: {condition}";

            this.logger.LogInformation(logInfo);
            if (!string.IsNullOrEmpty(reportConfig.OutoutFilename))
            {
                File.AppendAllText(reportConfig.OutoutFilename, logInfo + Environment.NewLine);
            }
        }
    }
}
