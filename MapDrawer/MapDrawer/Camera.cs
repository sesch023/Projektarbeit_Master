using System.Windows.Forms;
using MapDrawer.EventSystem;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer
{
    public class Camera : IUpdatable
    {
        protected Viewport _viewport;
        
        public Camera(Viewport viewport)
        {
            _viewport = viewport;
        }
        
        public void Update()
        {
            
        }
    }
}