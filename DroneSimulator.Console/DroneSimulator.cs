using DroneSimulator.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DroneSimulator.Console
{
    public class DroneSimulator : IDroneSimulator
    {
        private readonly IDroneService droneService;
        private readonly IDispatcherJob dispatcherJob;
        private readonly ILogger<DroneSimulator> logger;

        public DroneSimulator(IDroneService droneService, 
                              IDispatcherJob disparcherJob,
                              ILogger<DroneSimulator> logger)
        {
            this.droneService = droneService;
            this.dispatcherJob = disparcherJob;
            this.logger = logger;
        }

        public async Task Start()
        {
            logger.LogInformation("Init DroneSimulator");

            this.droneService.Create(5937);
            this.droneService.Create(6043);

            this.dispatcherJob.Init();

            while(this.dispatcherJob.IsRunning)
            {
                await Task.Delay(500);
            }

            logger.LogInformation("DroneSimulator finished!");
        }
    }
}
