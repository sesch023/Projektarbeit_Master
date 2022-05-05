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
        private const int PixelPerLatitude = 20;
        private const int PixelPerLongitude = 10;
        
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
            return GetGlobeDefaultTileTexture();
        }
        
        private GlobeTile _globeTile;
        private Rectangle _drawingRectangle;
        private Texture2D _texture2D;
        private Color _random;

        public GlobeTexture(GlobeTile globeTile, Texture2D texture2D = null, float longitudeTileStep=0.5f)
        {
            _texture2D = texture2D;
            _globeTile = globeTile;
            var r = new Random();
            _random = new Color(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            CalculateDrawingRectangle(longitudeTileStep);
        }

        private void CalculateDrawingRectangle(float longitudeTileStep=0.5f)
        {
            double y = MercatorProjection.LatToY(_globeTile.GlobePosition.Latitude);
            _drawingRectangle = new Rectangle
            {
                X = (int)(MercatorProjection.LonToX(_globeTile.GlobePosition.Longitude) * PixelPerLatitude),
                Y = (int)y * PixelPerLongitude,
                Width = PixelPerLatitude,
                Height = (int)(PixelPerLongitude*(MercatorProjection.LatToY(_globeTile.GlobePosition.Latitude+longitudeTileStep)-y+longitudeTileStep))
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, _drawingRectangle, _random);
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            if (_texture2D == null)
                _texture2D = GetGlobeTexture(_globeTile);
        }
    }
}