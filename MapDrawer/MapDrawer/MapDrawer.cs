using System;
using MapDrawer.ManagerSystem;
using MapDrawer.CameraSystem;
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
        private Texture2D _baseTexture;
        private Color[,] _colormap;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public MapDrawer()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = sizeX * pixelPerX;
            _graphics.PreferredBackBufferWidth = sizeY * pixelPerY;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Vsynch + Fixed Update aus
            IsFixedTimeStep = false;
            _graphics.SynchronizeWithVerticalRetrace = false;
        }

        private void initMap()
        {
            _colormap = new Color [sizeX, sizeY];
            var r = new Random();

            for (var x = 0; x < sizeX; x++)
            for (var y = 0; y < sizeY; y++)
                _colormap[x, y] = new Color(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
        }

        protected override void Initialize()
        {
            initMap();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            _baseTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
            _baseTexture.SetData(new[] {Color.White});


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateManager.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, 
                null, null, SimulationManager.Instance.CameraController.Camera2D.GetTransform());
            
            for(var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    _spriteBatch.Draw(_baseTexture, new Rectangle(x * pixelPerX, y * pixelPerY, pixelPerX, pixelPerY), 
                                      _colormap[x, y]);
                }
            }
            

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}