using System;
using MapDrawer.ControlSystem;
using MapDrawer.ManagerSystem;
using Microsoft.Xna.Framework;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MapDrawer.CameraSystem
{
    public class CameraController
    {
        public Camera2D Camera2D { get; }
        private float _moveSpeed;

        public CameraController(float moveSpeed = 1.0f)
        {
            Camera2D = new Camera2D
            {
                Zoom = 4.0f,
                Position = new Vector2(-100, -100)
            };

            _moveSpeed = moveSpeed;

            KeyAction left = new KeyAction(Keys.Left, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(CalculateMoveSpeed(), 0));
            });
            
            KeyAction right = new KeyAction(Keys.Right, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(-CalculateMoveSpeed(), 0));
            });
            
            KeyAction up = new KeyAction(Keys.Up, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(0, CalculateMoveSpeed()));
            });
            
            KeyAction down = new KeyAction(Keys.Down, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(0, -CalculateMoveSpeed()));
            });
            
            KeyAction zoomDown = new KeyAction(Keys.OemPlus, ActionType.KeyHold, (s) =>
            {
                Camera2D.Zoom += CalculateZoomSpeed();
            });
            
            KeyAction zoomUp = new KeyAction(Keys.OemMinus, ActionType.KeyHold, (s) =>
            {
                Camera2D.Zoom -= CalculateZoomSpeed();
            });
        }

        private float CalculateMoveSpeed()
        {
            return (10.0f/Camera2D.Zoom) * _moveSpeed * SimulationManager.Instance.CalculateUpdateTimeFactor();
        }

        private float CalculateZoomSpeed()
        {
            return 0.1f;
        }
    }
}