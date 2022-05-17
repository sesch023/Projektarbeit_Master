using System;
using MapDrawer.EventSystem;
using MapDrawer.ManagerSystem;
using Microsoft.Xna.Framework;
using IDrawable = MapDrawer.Graphical.IDrawable;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer.MapSystem
{
    public class GlobeTile : IUpdatable
    {
        public GlobePosition GlobePosition { get; }
        public GlobeTexture GlobeTexture { get; set; }
        
        public int Temperature { get; set; }

        private float _humidity;
        public float Humidity
        {
            get => _humidity;
            set => _humidity = Math.Clamp(value, 0, 100);
        }
        
        public GlobeTile(PlaneGlobe planeGlobe, GlobePosition globePosition, int temperature, float humidity)
        {
            _humidity = humidity;
            GlobePosition = globePosition;
            Temperature = temperature;
            GlobeTexture = new GlobeTexture(planeGlobe, this, null);
            GraphicsManager.Instance.AddLoadRequired(GlobeTexture);
        }

        public void Update()
        {
        }
    }
}