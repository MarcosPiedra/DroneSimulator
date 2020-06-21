using System;
using System.Collections.Concurrent;

namespace DroneSimulator.Domain.Models
{
    public class Drone
    {
        public int Id { get; set; } = 0;
        public ConcurrentQueue<LocationDatetime> Moved { get; set; } = new ConcurrentQueue<LocationDatetime>();
        public LocationDatetime LastLocationDatetime { get; set; } = null;
    }
}