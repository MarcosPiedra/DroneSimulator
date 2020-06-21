using DroneSimulator.Domain.Models;
using MediatR;

namespace DroneSimulator.Domain
{
    public class FinishDroneCommand : IRequest
    {
        public Drone Drone { get; set; }
    }
}