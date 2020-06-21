using DroneSimulator.Domain;
using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DroneSimulator.CSVDataAccess.LineParser
{
    public class LocationDroneLineParser : ILineParser<LocationDroneInTime>
    {
        public LocationDroneLineParser()
        {
        }

        public LocationDroneInTime Parse(string line)
        {
            // Sample: 5937,"51.476105","-0.100224","2011-03-22 07:55:26"
            var data = line.Split(',');

            if (data.Length != 4)
                throw new Exception($"Line to parse it is wrong: {line}");

            // Parse directly
            var droneId = Convert.ToInt32(data[0]);

            var latitude = float.Parse(data[1].Trim('"'), CultureInfo.InvariantCulture);
            var longitud = float.Parse(data[2].Trim('"'), CultureInfo.InvariantCulture);
            var datetime = DateTime.Parse(data[3].Trim('"'));

            var locationDrone = new LocationDroneInTime();
            locationDrone.DroneId = droneId;

            var locationDatetime = new LocationDatetime();
            locationDatetime.Location = new Location() { Latitude = latitude, Longitud = longitud };
            locationDatetime.DateTime = datetime;

            locationDrone.LocationDatetime = locationDatetime;

            return locationDrone;
        }
    }
}
