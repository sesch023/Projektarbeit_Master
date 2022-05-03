using MapDrawer.EventSystem;
using MapDrawer.Graphical;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer.MapSystem
{
    public class Globe : IUpdatable, IDrawable
    {
        private float LongitudeTileStep;
        private float LatitudeTileStep;

        private GlobeTile[,] globe;

        public Globe(float latitudeTileStep = 0.5f, float longitudeTileStep = 0.5f)
        {
            LongitudeTileStep = longitudeTileStep;
            LatitudeTileStep = latitudeTileStep;

            globe = new GlobeTile[(int)(360/latitudeTileStep),(int)(180/longitudeTileStep)];
        }
        
        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}