using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace MapDrawer
{
    public class Camera : Updatable
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