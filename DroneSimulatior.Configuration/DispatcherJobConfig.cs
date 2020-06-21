using System;

namespace DroneSimulator.Configuration
{
    public class DispatcherJobConfig
    {
        public int DelayInSecond { get; set; } = 0;
        public DateTime InitialDateTime { get; set; }
        public DateTime FinishDateTime { get; set; }
        public int SimulationFactor { get; set; } = 0;
    }
}
