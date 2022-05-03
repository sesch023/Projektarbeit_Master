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
            set => _latitude = Math.Clamp(value, -90, 90);
        }

        private float _longitude;
        public float Longitude
        {
            get => _longitude;
            set => _longitude = Math.Clamp(value, -180, 180);
        }

        public float Elevation { get; set; }
        
        public static Vector2 ProjectGlobePositionToScreenCoordinate(GlobePosition position)
        {
            return new Vector2((float)MercatorProjection.LonToX(position.Longitude),
                (float)MercatorProjection.LatToY(position.Latitude));
        }
    }
}