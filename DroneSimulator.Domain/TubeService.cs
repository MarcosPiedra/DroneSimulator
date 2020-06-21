using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using config = DroneSimulator.Configuration;

namespace DroneSimulator.Domain
{
    public class TubeService : ITubeService
    {
        private readonly config.DroneConfig droneConfig;
        private readonly IRepository<Tube> tubeRepo;

        public TubeService(config.DroneConfig droneConfig,
                           IRepository<Tube> tubeRepo)
        {
            this.droneConfig = droneConfig;
            this.tubeRepo = tubeRepo;
        }

        public bool IsIntersected(Location location, out Tube tube)
        {
            tube = this.tubeRepo.Query.AsParallel()
                                      .FirstOrDefault(t => t.Location.GetMeters(location) < droneConfig.NearByStationValue);

            return tube != null;
        }
    }
}
