using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MapDrawer.ControlSystem;
using MapDrawer.EventSystem;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MapDrawer.ManagerSystem
{
    public class MainUpdateManager : IManager, IUpdatable
    {
        public static MainUpdateManager Instance { get; }

        private List<IUpdatable> _updatables;

        static MainUpdateManager()
        {
            Instance = new MainUpdateManager();
        }

        private MainUpdateManager()
        {
            _updatables = new List<IUpdatable>();
            Init();
        }

        private void Init()
        {
            UpdatableEvent action = new KeyAction(Keys.Enter, ActionType.KeyDown);
            action.AddSubscriber((triggeredBy) => { LoggingManager.Instance.Info("Hello Enter!"); });
            RegisterUpdatable(action);
            RegisterUpdatable(TimeManager.Instance);
        }

        public void RegisterUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Update()
        {
            foreach (var variableUpdatable in _updatables)
            {
                variableUpdatable.Update();
            }
        }
    }
}