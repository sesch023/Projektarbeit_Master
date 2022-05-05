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
        public GlobeTexture GlobeTexture { get; }
        
        public int Temperature { get; set; }

        private float _humidity;
        public float Humidity
        {
            get => _humidity;
            set => _humidity = Math.Clamp(value, 0, 100);
        }
        
        public GlobeTile(GlobePosition globePosition, int temperature, float humidity, GlobeTexture globeTexture = null, 
            float longitudeTileStep=0.5f)
        {
            _humidity = humidity;
            GlobePosition = globePosition;
            Temperature = temperature;
            if (globeTexture == null)
            {
                globeTexture = new GlobeTexture(this, null, longitudeTileStep);
                GraphicsManager.Instance.AddLoadRequired(globeTexture);
            }
            GlobeTexture = globeTexture;
        }

        public void Update()
        {
        }
    }
}