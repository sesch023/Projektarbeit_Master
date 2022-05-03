using System.Collections.Generic;
using System.Linq;
using MapDrawer.Graphical;
using MapDrawer.MapSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = MapDrawer.Graphical.IDrawable;

namespace MapDrawer.ManagerSystem
{
    public class GraphicsManager : IManager, IDrawable, ILoadRequired
    {
        private readonly List<IDrawable> _drawables;
        private readonly List<ILoadRequired> _loadRequireds;

        private Texture2D _globeTexture;

        public static GraphicsManager Instance { get; }

        static GraphicsManager()
        {
            Instance = new GraphicsManager();
        }

        private GraphicsManager()
        {
            _drawables = new List<IDrawable>();
            _loadRequireds = new List<ILoadRequired>();
            Init();
        }

        private void Init()
        {
            TestMap testmap = new TestMap();
            AddLoadRequired(testmap);
            AddDrawable(testmap);
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            _globeTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _globeTexture.SetData(new[] {Color.White});
            
            foreach (var variableUpdatable in _loadRequireds.ToList()) variableUpdatable.LoadContent(spriteBatch);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, 
                null, null, SimulationManager.Instance.CameraController.Camera2D.GetTransform());
            
            foreach (var variableUpdatable in _drawables.ToList()) variableUpdatable.Draw(spriteBatch);

            spriteBatch.End();
            
        }

        public void AddDrawable(IDrawable drawable)
        {
            _drawables.Add(drawable);
        }

        public void RemoveDrawable(IDrawable drawable)
        {
            _drawables.Remove(drawable);
        }
        
        public void AddLoadRequired(ILoadRequired load)
        {
            _loadRequireds.Add(load);
        }

        public void RemoveLoadRequired(ILoadRequired load)
        {  
            _loadRequireds.Remove(load);
        }

        public Texture2D GetGlobeTileTexture(GlobePosition globePosition)
        {
            return _globeTexture;
        }
    }
}