using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace DroneSimulator.Domain.Module
{
    public class DroneSimulation : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DroneService>().As<IDroneService>();
            builder.RegisterType<TubeService>().As<ITubeService>();
            builder.RegisterType<DispatcherService>().As<IDisparcherService>();
            builder.RegisterType<DispatcherJob>().As<IDispatcherJob>();
            builder.RegisterType<TrafficReport>().As<ITrafficReport>();

            builder.AddMediatR(typeof(DroneSimulation).Assembly);
        }
    }
}
