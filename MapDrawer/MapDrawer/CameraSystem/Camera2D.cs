using Microsoft.Xna.Framework;

namespace MapDrawer.CameraSystem
{
    // https://stackoverflow.com/questions/17452808/moving-a-camera-in-xna-c-sharp
    public class Camera2D
    {
        public Camera2D() : this(Vector2.Zero)
        {
        }
        
        public Camera2D(Vector2 position)
        {
            Zoom = 1;
            Rotation = 0;
            Origin = Vector2.Zero;
            Position = position;
        }

        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        public void Move(Vector2 direction)
        {
            Position += direction;
        }

        public Matrix GetTransform()
        {
            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            var originMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            return translationMatrix * rotationMatrix * scaleMatrix * originMatrix;
        }
    }
}