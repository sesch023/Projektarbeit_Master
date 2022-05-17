using System;
using MapDrawer.EventSystem;
using MapDrawer.Graphical;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer.MapSystem
{
    public class PlaneGlobe : IUpdatable, IDrawable
    {
        public float LongitudeTileStep { get; }
        public float LatitudeTileStep { get; }

        private GlobeTile[,] _globe;

        public PlaneGlobe(float latitudeTileStep = 0.5f, float longitudeTileStep = 0.5f)
        {
            LongitudeTileStep = longitudeTileStep;
            LatitudeTileStep = latitudeTileStep;

            InitGlobe();
        }

        private void InitGlobe()
        {
            _globe = new GlobeTile[(int)(360/LatitudeTileStep),(int)(180/LongitudeTileStep)];
            for (var x = 0; x < _globe.GetLength(0); x++)
            {
                for (var y = 0; y < _globe.GetLength(1); y++)
                {
                    var lat = x * LatitudeTileStep - 180.0f;
                    var longi = y * LongitudeTileStep - 90.0f;
                    GlobePosition position = new GlobePosition
                    {
                        Elevation = 0.0f,
                        Latitude = lat,
                        Longitude = longi
                    };

                    _globe[x, y] = new GlobeTile(this, position, 0, 0.0f);
                }
            }
        }
        
        public void Update()
        {
            foreach(var tile in _globe)
            {
                tile.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var tile in _globe)
            {
                tile.GlobeTexture.Draw(spriteBatch);
            }
        }
    }
}