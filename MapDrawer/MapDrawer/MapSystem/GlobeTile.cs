using System;
using MapDrawer.EventSystem;
using Microsoft.Xna.Framework;
using IDrawable = MapDrawer.Graphical.IDrawable;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer.MapSystem
{
    public class GlobeTile : IDrawable, IUpdatable
    {
        public GlobePosition GlobePosition { get; set; }
        
        public int Temperature { get; set; }

        private float _humidity;
        public float Humidity
        {
            get => _humidity;
            set => _humidity = Math.Clamp(value, 0, 100);
        }

        public GlobeTile(GlobePosition globePosition, int temperature, float humidity)
        {
            _humidity = humidity;
            GlobePosition = globePosition;
            Temperature = temperature;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Implement Globe Tile Draw By Doing a Mercator Projection
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}