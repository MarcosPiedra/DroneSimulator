using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneSimulator.Domain.Handler
{
    public class FinishDroneHandler : IRequestHandler<FinishDroneCommand>
    {
        private readonly IDroneService droneService;

        public FinishDroneHandler(IDroneService droneService)
        {
            this.droneService = droneService;
        }

        public async Task<Unit> Handle(FinishDroneCommand request, CancellationToken cancellationToken)
        {
            this.droneService.Shutdown(request.Drone);
            return await Task.FromResult(Unit.Value);
        }
    }
}
