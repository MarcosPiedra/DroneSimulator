using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSimulator.Domain.Models
{
    public class Tube
    {
        public string Name { get; set; } = "";
        public Location Location { get; set; } = new Location();
    }
}
