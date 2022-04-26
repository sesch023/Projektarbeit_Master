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
                Position = new Vector2(100, 100)
            };

            _moveSpeed = moveSpeed;

            KeyAction left = new KeyAction(Keys.Left, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(_moveSpeed * SimulationManager.Instance.CalculateUpdateTimeFactor(), 0));
            });
            UpdateManager.Instance.RegisterUpdatable(left);
            
            KeyAction right = new KeyAction(Keys.Right, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(-_moveSpeed * SimulationManager.Instance.CalculateUpdateTimeFactor(), 0));
            });
            UpdateManager.Instance.RegisterUpdatable(right);
            
            KeyAction up = new KeyAction(Keys.Up, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(0, _moveSpeed * SimulationManager.Instance.CalculateUpdateTimeFactor()));
            });
            UpdateManager.Instance.RegisterUpdatable(up);
            
            KeyAction down = new KeyAction(Keys.Down, ActionType.KeyHold, (s) =>
            {
                Camera2D.Move(new Vector2(0, -_moveSpeed * SimulationManager.Instance.CalculateUpdateTimeFactor()));
            });
            UpdateManager.Instance.RegisterUpdatable(down);
            
            KeyAction zoomDown = new KeyAction(Keys.OemPlus, ActionType.KeyDown, (s) =>
            {
                Camera2D.Zoom += 0.1f;
            });
            UpdateManager.Instance.RegisterUpdatable(zoomDown);
            
            KeyAction zoomUp = new KeyAction(Keys.OemMinus, ActionType.KeyDown, (s) =>
            {
                Camera2D.Zoom -= 0.1f;
            });
            UpdateManager.Instance.RegisterUpdatable(zoomUp);
        }
        
        
    }
}