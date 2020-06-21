using DroneSimulator.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using config = DroneSimulator.Configuration;

namespace DroneSimulator.Domain
{
    public class DispatcherJob : IDispatcherJob
    {
        private readonly config.DispatcherJobConfig config;
        private readonly IDisparcherService disparcherService;
        private readonly IDroneService droneService;
        private readonly IMediator mediator;
        private readonly ILogger<DispatcherJob> logger;
        private readonly CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private DateTime dateTime;
        private int nextSeconds;

        public bool IsRunning => !cancelTokenSource.IsCancellationRequested;

        public DispatcherJob(config.DispatcherJobConfig config,
                             IDisparcherService disparcherService,
                             IDroneService droneService,
                             IMediator mediator,
                             ILogger<DispatcherJob> logger)
        {
            this.config = config;
            this.disparcherService = disparcherService;
            this.droneService = droneService;
            this.mediator = mediator;
            this.logger = logger;
            dateTime = this.config.InitialDateTime;
            nextSeconds = this.config.DelayInSecond;
        }

        public void Init()
        {
            this.RunContinuestly(this.config.DelayInSecond, cancelTokenSource.Token);
        }

        public async Task ExecuteAsync()
        {
            var count = config.SimulationFactor;

            while (count != 0)
            {
                logger.LogInformation($"Process {this.dateTime.ToString("MM/dd/yyyy HH:mm:ss")}");

                if (this.dateTime.Equals(this.config.FinishDateTime))
                {
                    this.Cancel();
                    droneService.ProcessDataMoved();
                    var drones = droneService.GetDrones();

                    foreach (var drone in drones)
                    {
                        await mediator.Send(new FinishDroneCommand() { Drone = drone });
                    }

                    return;
                }

                var orders = await disparcherService.GetAsync(this.dateTime);
                foreach (var order in orders)
                {
                    var drone = droneService.Find(order.DroneId);
                    if (drone != null)
                    {
                        await mediator.Send(new MoveDroneCommand() { Drone = drone, LocationDatetime = order.LocationDatetime });
                    }
                }

                droneService.ProcessDataMoved();
                this.dateTime = this.dateTime.AddSeconds(this.nextSeconds);
                count--;
            }
        }

        public void Cancel()
        {
            this.cancelTokenSource.Cancel();
        }
    }
}
