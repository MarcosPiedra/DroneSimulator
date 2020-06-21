using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSimulator.Domain
{
    public interface ITubeService
    {
        bool IsIntersected(Location location, out Tube tube);
    }
}
