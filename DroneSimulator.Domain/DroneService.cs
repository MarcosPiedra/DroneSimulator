using DroneSimulator.Configuration;
using DroneSimulator.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DroneSimulator.Domain
{
    public class DroneService : IDroneService
    {
        private readonly ITubeService tubeService;
        private readonly ITrafficReport trafficReport;
        private readonly ILogger<DroneService> logger;
        private readonly DroneConfig droneConfig;

        //Data cached
        private static Dictionary<int, Drone> drones = new Dictionary<int, Drone>();

        public DroneService(ITubeService tubeService,
                            ITrafficReport trafficReport,
                            ILogger<DroneService> logger,
                            DroneConfig droneConfig)
        {
            this.tubeService = tubeService;
            this.trafficReport = trafficReport;
            this.logger = logger;
            this.droneConfig = droneConfig;
        }

        public Drone Create(int id)
        {
            var newDrone = new Drone() { Id = id };
            drones.Add(id, newDrone);

            return newDrone;
        }

        public Drone Find(int id)
        {
            if (drones.ContainsKey(id))
                return drones[id];

            return null;
        }

        public void Move(Drone drone, LocationDatetime locationDatetime)
        {
            this.logger.LogInformation($"Move droneId {drone.Id}: {locationDatetime}");
            drone.Moved.Enqueue(locationDatetime);
        }

        public void ProcessDataMoved()
        {
            foreach (var drone in drones.Values)
            {
                if (drone.Moved.Count == 0)
                    continue;

                if (drone.Moved.Count < droneConfig.MovementConsumerCount)
                    continue;

                while (drone.Moved.TryDequeue(out LocationDatetime data))
                {
                    if (drone.LastLocationDatetime == null)
                        drone.LastLocationDatetime = data;

                    if (tubeService.IsIntersected(data.Location, out Tube tube))
                    {
                        var speed = data.CalcSpeed(drone.LastLocationDatetime);
                        var condition = (TrafficCondition)(new Random().Next(0, 2));
                        trafficReport.Report(drone.Id, data.DateTime, speed, condition);
                    }
                }
            }
        }

        public void Shutdown(Drone drone)
        {
            this.logger.LogInformation($"SHUTDOWN droneId {drone.Id}");
        }

        public IEnumerable<Drone> GetDrones()
        {
            foreach (var item in drones)
            {
                yield return item.Value;
            }
        }
    }
}
