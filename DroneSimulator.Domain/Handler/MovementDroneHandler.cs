using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneSimulator.Domain.Handler
{
    public class MovementDroneHandler : IRequestHandler<MoveDroneCommand>
    {
        private readonly IDroneService droneService;

        public MovementDroneHandler(IDroneService droneService)
        {
            this.droneService = droneService;
        }

        public async Task<Unit> Handle(MoveDroneCommand request, CancellationToken cancellationToken)
        {
            this.droneService.Move(request.Drone, request.LocationDatetime);
            return await Task.FromResult(Unit.Value);
        }
    }
}
