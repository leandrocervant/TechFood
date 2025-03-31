using System;
using TechFood.Domain.Shared;

namespace TechFood.Domain.ValueObjects
{
    public class Coordinates : ValueObject
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double DistanceFrom(Coordinates coordinates)
        {
            double rlat1 = Math.PI * Latitude / 180;
            double rlat2 = Math.PI * coordinates.Latitude / 180;
            double theta = Longitude - coordinates.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                          Math.Cos(rlat2) * Math.Cos(rtheta);

            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            //Kilometers
            return Math.Round(dist * 1.609344, 2, MidpointRounding.AwayFromZero);
        }
    }
}
