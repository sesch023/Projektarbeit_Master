using MapDrawer.EventSystem;
using MapDrawer.Graphical;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer.MapSystem
{
    public class Globe : IUpdatable, IDrawable
    {
        private float LongitudeTileStep;
        private float LatitudeTileStep;

        private GlobeTile[,] _globe;

        public Globe(float latitudeTileStep = 0.5f, float longitudeTileStep = 0.5f)
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
                    GlobePosition position = new GlobePosition
                    {
                        Elevation = 0.0f,
                        Latitude = x * LatitudeTileStep-180.0f,
                        Longitude = y * LongitudeTileStep-90.0f
                    };
                    _globe[x, y] = new GlobeTile(position, 0, 0.0f, null, LongitudeTileStep);
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