using System;
using MapDrawer.Graphical;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = MapDrawer.Graphical.IDrawable;

namespace MapDrawer.MapSystem
{
    public class TestMap : IDrawable, ILoadRequired
    {
        private const int sizeX = 100;
        private const int sizeY = 100;

        private const int pixelPerX = 10;
        private const int pixelPerY = 10;
        
        private Texture2D _baseTexture;
        private Color[,] _colormap;

        public void LoadContent(SpriteBatch spriteBatch)
        {
            _colormap = new Color [sizeX, sizeY];
            var r = new Random();

            for (var x = 0; x < sizeX; x++)
            for (var y = 0; y < sizeY; y++)
                _colormap[x, y] = new Color(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            
            _baseTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _baseTexture.SetData(new[] {Color.White});
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for(var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    spriteBatch.Draw(_baseTexture, new Rectangle(x * pixelPerX, y * pixelPerY, pixelPerX, 
                                pixelPerY), _colormap[x, y]);
                }
            }
        }
    }
}