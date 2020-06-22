using Autofac;
using DroneSimulator.Configuration;
using DroneSimulator.CSVDataAccess;
using DroneSimulator.CSVDataAccess.LineParser;
using DroneSimulator.Domain;
using DroneSimulator.Domain.Models;
using DroneSimulator.Domain.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DroneSimulator.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var configBuilder = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.Deug.json", optional: true, reloadOnChange: true);

            var configuration = configBuilder.Build();

            var dispatcherJobConfig = configuration.GetSection("DispatcherJob").Get<DispatcherJobConfig>();
            var droneConfig = configuration.GetSection("Drone").Get<DroneConfig>();
            var reportConfig = configuration.GetSection("Report").Get<ReportConfig>();

            var loggerFactory = LoggerFactory.Create(b =>
            {
                b.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug);
                b.AddConsole();
            });

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DroneSimulation());
            builder.RegisterInstance(loggerFactory).As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
            builder.RegisterInstance(dispatcherJobConfig);
            builder.RegisterInstance(droneConfig);
            builder.RegisterInstance(reportConfig);            
            builder.RegisterType<CSVRespository<LocationDroneInTime>>()
                   .As<IRepository<LocationDroneInTime>>()
                   .OnActivated(async r => await r.Instance.Load(basePath, CSVFileType.LocationDrone))
                   .SingleInstance();
            builder.RegisterType<CSVRespository<Tube>>()
                   .As<IRepository<Tube>>()
                   .OnActivated(async r => await r.Instance.Load(basePath, CSVFileType.Tube))
                   .SingleInstance();
            builder.Register<Func<CSVFileType, object>>((c, p) =>
            {
                return (type) =>
                {
                    switch (type)
                    {
                        case CSVFileType.LocationDrone:
                            return new LocationDroneLineParser();
                        case CSVFileType.Tube:
                            return new TubeLineParser();
                        default:
                            throw new ArgumentException("Invalid CSVFileType");
                    }
                };
            });
            builder.RegisterType<DroneSimulator>().As<IDroneSimulator>();
            var container = builder.Build();

            var simulator = container.Resolve<IDroneSimulator>();
            await simulator.Start();
        }
    }
}
