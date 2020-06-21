using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DroneSimulator.Domain
{
    public interface IDroneService
    {
        Drone Create(int id);
        Drone Find(int id);
        void Move(Drone drone, LocationDatetime locationDatetime);
        void Shutdown(Drone drone);
        void ProcessDataMoved();
        IEnumerable<Drone> GetDrones();
    }
}
