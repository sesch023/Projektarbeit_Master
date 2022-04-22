using System;
using System.Windows.Forms.VisualStyles;
using MapDrawer.ManagerSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapDrawer
{
    public class MapDrawer : Game
    {
        private const int sizeX = 100;
        private const int sizeY = 100;

        private const int pixelPerX = 10;
        private const int pixelPerY = 10;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _baseTexture;
        private Color[,] _colormap;

        public MapDrawer()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = sizeX * pixelPerX;
            _graphics.PreferredBackBufferWidth = sizeY * pixelPerY;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void initCamera()
        {
            //var viewport = new Viewport(0, 0, 0, 0, 0, 1);
            //GraphicsDevice.Viewport = viewport;
            //_graphics.ApplyChanges();
        }

        private void initMap()
        {
            _colormap = new Color [sizeX, sizeY];
            Random r = new Random();

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    _colormap[x, y] = new Color(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                }
            }
        }

        protected override void Initialize()
        {
            initMap();
            initCamera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            
            _baseTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
            _baseTexture.SetData(new Color[]{ Color.White });
            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            TimeManager.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            /*
            for(var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    _spriteBatch.Draw(_baseTexture, new Rectangle(x * pixelPerX, y * pixelPerY, pixelPerX, pixelPerY), 
                                      _colormap[x, y]);
                }
            }
            */
            
            _spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}