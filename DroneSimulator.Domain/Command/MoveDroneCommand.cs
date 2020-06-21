using DroneSimulator.Domain.Models;
using MediatR;

namespace DroneSimulator.Domain
{
    public class MoveDroneCommand : IRequest
    {
        public Drone Drone { get; set; }
        public LocationDatetime LocationDatetime { get; set; }
    }
}