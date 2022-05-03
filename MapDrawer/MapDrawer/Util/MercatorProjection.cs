using System;

namespace MapDrawer.Util
{
    // https://wiki.openstreetmap.org/wiki/Mercator#C#_implementation
    public static class MercatorProjection
    {
        private const double RMajor = 6378137.0;
        private const double RMinor = 6356752.3142;
        private const double Ratio = RMinor / RMajor;
        private static readonly double Eccent = Math.Sqrt(1.0 - Ratio * Ratio);
        private static readonly double Com = 0.5 * Eccent;

        private const double Deg2Rad = Math.PI / 180.0;
        private const double Rad2Deg = 180.0 / Math.PI;
        private const double Pi2 = Math.PI / 2.0;

        public static double[] ToPixel(double lon, double lat)
        {
            return new[] {LonToX(lon), LatToY(lat)};
        }

        public static double[] ToGeoCoord(double x, double y)
        {
            return new[] {XToLon(x), YToLat(y)};
        }

        public static double LonToX(double lon)
        {
            return RMajor * DegToRad(lon);
        }

        public static double LatToY(double lat)
        {
            lat = Math.Min(89.5, Math.Max(lat, -89.5));
            var phi = DegToRad(lat);
            var sinphi = Math.Sin(phi);
            var con = Eccent * sinphi;
            con = Math.Pow((1.0 - con) / (1.0 + con), Com);
            var ts = Math.Tan(0.5 * (Math.PI * 0.5 - phi)) / con;
            return 0 - RMajor * Math.Log(ts);
        }

        public static double XToLon(double x)
        {
            return RadToDeg(x) / RMajor;
        }

        public static double YToLat(double y)
        {
            var ts = Math.Exp(-y / RMajor);
            var phi = Pi2 - 2 * Math.Atan(ts);
            var dphi = 1.0;
            var i = 0;
            while (Math.Abs(dphi) > 0.000000001 && i < 15)
            {
                var con = Eccent * Math.Sin(phi);
                dphi = Pi2 - 2 * Math.Atan(ts * Math.Pow((1.0 - con) / (1.0 + con), Com)) - phi;
                phi += dphi;
                i++;
            }

            return RadToDeg(phi);
        }

        private static double RadToDeg(double rad)
        {
            return rad * Rad2Deg;
        }

        private static double DegToRad(double deg)
        {
            return deg * Deg2Rad;
        }
    }
}