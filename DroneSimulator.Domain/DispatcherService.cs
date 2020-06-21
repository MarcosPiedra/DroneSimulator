using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneSimulator.Domain
{
    public class DispatcherService : IDisparcherService
    {
        private readonly IRepository<LocationDroneInTime> repo;

        public DispatcherService(IRepository<LocationDroneInTime> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<LocationDroneInTime>> GetAsync(DateTime dateTime)
        {
            return await Task.FromResult(repo.Query.Where(d => d.LocationDatetime.DateTime == dateTime));
        }
    }
}
