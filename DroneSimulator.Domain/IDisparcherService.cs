using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DroneSimulator.Domain
{
    public interface IDisparcherService
    {
        Task<IEnumerable<LocationDroneInTime>> GetAsync(DateTime dateTime);
    }
}