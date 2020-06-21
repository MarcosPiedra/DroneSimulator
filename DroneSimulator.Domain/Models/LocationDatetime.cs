using System;
using System.Globalization;

namespace DroneSimulator.Domain.Models
{
    public class LocationDatetime
    {
        public Location Location { get; set; }
        public DateTime DateTime { get; set; }
        public override string ToString()
        {
            return $"DateTime {DateTime.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}, Location: lat {Location.Latitude} lon {Location.Longitud}";
        }
    }
}