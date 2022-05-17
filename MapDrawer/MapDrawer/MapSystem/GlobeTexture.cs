using System;
using MapDrawer.Graphical;
using MapDrawer.ManagerSystem;
using MapDrawer.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = MapDrawer.Graphical.IDrawable;

namespace MapDrawer.MapSystem
{
    public class GlobeTexture : IDrawable, ILoadRequired
    {
        private const int UnitsPerLatitude = 10;
        private const int UnitsPerLongitude = 10;
        
        private static Texture2D _globeDefaultTexture;
        
        private static Texture2D GetGlobeDefaultTileTexture()
        {
            if (_globeDefaultTexture == null)
            {
                _globeDefaultTexture = new Texture2D(GraphicsManager.Instance.SpriteBatch.GraphicsDevice, 1, 1);
                _globeDefaultTexture.SetData(new[] {Color.White});
            }
            return _globeDefaultTexture;
        }
        
        private Texture2D GetGlobeTexture(GlobeTile globeTile)
        {
            Texture2D texture2D;
            if (MathUtil.AlmostEquals(globeTile.GlobePosition.Longitude, 0.0f, 0.1f) 
                || MathUtil.AlmostEquals(globeTile.GlobePosition.Latitude, 0.0f, 0.1f))
            {
                Console.WriteLine(globeTile.GlobePosition.Longitude);
                Console.WriteLine(globeTile.GlobePosition.Latitude);
                texture2D = new Texture2D(GraphicsManager.Instance.SpriteBatch.GraphicsDevice, 1, 1);
                texture2D.SetData(new[] {Color.Red});
            }
            else
            {
                texture2D = GetGlobeDefaultTileTexture();
            }

            return texture2D;
        }

        private PlaneGlobe _planeGlobe;
        private GlobeTile _globeTile;
        private Rectangle _drawingRectangle;
        private Texture2D _texture2D;

        public GlobeTexture(PlaneGlobe planeGlobe, GlobeTile globeTile, Texture2D texture2D = null)
        {
            _texture2D = texture2D;
            _planeGlobe = planeGlobe;
            _globeTile = globeTile;
            CalculateDrawingRectangle();
        }

        private void CalculateDrawingRectangle()
        {
            _drawingRectangle = new Rectangle
            {
                X = (int)(_globeTile.GlobePosition.Latitude * UnitsPerLatitude),
                Y = (int)(_globeTile.GlobePosition.Longitude * UnitsPerLongitude),
                Width = UnitsPerLatitude,
                Height = UnitsPerLongitude
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, _drawingRectangle, Color.White);
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            if (_texture2D == null)
                _texture2D = GetGlobeTexture(_globeTile);
        }
    }
}