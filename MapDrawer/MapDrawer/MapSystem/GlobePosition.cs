using System;
using MapDrawer.Util;
using Microsoft.Xna.Framework;

namespace MapDrawer.MapSystem
{
    public struct GlobePosition
    {
        private float _latitude;
        public float Latitude
        {
            get => _latitude;
            set => _latitude = Math.Clamp(value, -180, 180);
        }

        private float _longitude;
        public float Longitude
        {
            get => _longitude;
            set => _longitude = Math.Clamp(value, -90, 90);
        }

        public float Elevation { get; set; }
    }
}