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

            KeyboardAction left = new KeyboardAction(Keys.Left, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(CalculateMoveSpeed(), 0));
            });
            
            KeyboardAction right = new KeyboardAction(Keys.Right, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(-CalculateMoveSpeed(), 0));
            });
            
            KeyboardAction up = new KeyboardAction(Keys.Up, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(0, CalculateMoveSpeed()));
            });
            
            KeyboardAction down = new KeyboardAction(Keys.Down, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(0, -CalculateMoveSpeed()));
            });
            
            //TODO: Clamp Zoom, Change Zoom Speed on level
            KeyboardAction zoomDown = new KeyboardAction(Keys.OemPlus, ActionType.KeyHold, (s) =>
            {
                Camera2D.Zoom += CalculateZoomSpeed();
            });
            
            KeyboardAction zoomUp = new KeyboardAction(Keys.OemMinus, ActionType.KeyHold, (s) =>
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
            return 0.1f * SimulationManager.Instance.CalculateUpdateTimeFactor();
        }
    }
}