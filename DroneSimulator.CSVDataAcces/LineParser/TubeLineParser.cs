using DroneSimulator.Domain;
using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DroneSimulator.CSVDataAccess.LineParser
{
    public class TubeLineParser : ILineParser<Tube>
    {
        public TubeLineParser()
        {
        }

        public Tube Parse(string line)
        {
            // Sample: "Acton Town",51.503071,-0.280303
            var data = line.Split(',');

            if (data.Length != 3)
                throw new Exception($"Line to parse it is wrong: {line}");

            // Parse directly
            var tubeNamed = data[0].Trim('"');
            var latitude = float.Parse(data[1], CultureInfo.InvariantCulture);
            var longitud = float.Parse(data[2], CultureInfo.InvariantCulture);

            var tube = new Tube();
            tube.Name = tubeNamed;
            tube.Location = new Location() { Latitude = latitude, Longitud = longitud };

            return tube;
        }
    }
}
