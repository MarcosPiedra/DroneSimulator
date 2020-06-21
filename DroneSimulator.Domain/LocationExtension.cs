using DroneSimulator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSimulator.Domain
{
    public static class LocationExtension
    {
        public static int GetMeters(this Location locA, Location locB)
        {
            // https://stackoverflow.com/questions/639695/how-to-convert-latitude-or-longitude-to-meters

            var lat1 = locA.Latitude;
            var lon1 = locA.Longitud;
            var lat2 = locB.Latitude;
            var lon2 = locB.Longitud;

            var R = 6378.137; // Radius of earth in KM
            var dLat = lat2 * Math.PI / 180 - lat1 * Math.PI / 180;
            var dLon = lon2 * Math.PI / 180 - lon1 * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;

            return (int)d * 1000; // meters
        }
    }

    public static class LocationDatetimeExtensions
    {
        public static float CalcSpeed(this LocationDatetime locDateTimeA, LocationDatetime locDateTimeB)
        {
            var metersA = locDateTimeA.Location.GetMeters(locDateTimeB.Location);
            var seconds = locDateTimeA.DateTime.Subtract(locDateTimeB.DateTime).TotalSeconds;

            return metersA / (float)seconds;
        }
    }
}
