using MapDrawer.EventSystem;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer
{
    public class Camera : IUpdatable
    {
        protected Viewport Viewport;

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
        }

        public void Update()
        {
        }
    }
}