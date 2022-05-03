using System.Collections.Generic;
using System.Linq;
using MapDrawer.ControlSystem;
using MapDrawer.EventSystem;
using Microsoft.Xna.Framework.Input;

namespace MapDrawer.ManagerSystem
{
    public sealed class UpdateManager : IUpdateManager
    {
        private readonly List<IUpdatable> _updatables;
        private readonly Queue<IUpdatable> _markedForRemoval;

        static UpdateManager()
        {
            Instance = new UpdateManager();
            Instance.Init();
        }

        private UpdateManager()
        {
            _updatables = new List<IUpdatable>();
            _markedForRemoval = new Queue<IUpdatable>();
        }

        public static UpdateManager Instance { get; }

        public void Update()
        {
            foreach (var variableUpdatable in _updatables.ToList()) variableUpdatable.Update();
            RemovalRun();
        }

        private void RemovalRun()
        {
            foreach(var updatable in _markedForRemoval)
            {
                LoggingManager.Instance.Info("Clearing Updatable: " + updatable);
                RemoveUpdatable(updatable);
            }
            
            _markedForRemoval.Clear();
        }

        private void Init()
        {
            RegisterUpdatable(TimeManager.Instance);
            InitUpdatables();
        }
        
        private void InitUpdatables()
        {
            UpdatableEvent action = new KeyAction(Keys.Enter, ActionType.KeyDown, 
                triggeredBy => { LoggingManager.Instance.Info("Hello Enter!"); });

            var spacedEvent = new TimeSpacedEvent(5000,
                triggeredBy =>
                    LoggingManager.Instance.Info("This Message should be shown every 5000ms!"));

            var timeEvent = new TimeEvent(5000,
                triggeredBy =>
                    LoggingManager.Instance.Info("This Message should be shown once at 5s!"),
                instance =>
                {
                    MarkRemovableForRemoval(instance);
                    LoggingManager.Instance.Info("Bye Time Event!");
                });
            
            var tickEvent = new TickEvent(10,
                triggeredBy =>
                    LoggingManager.Instance.Info("This Message should be shown once after 10 ticks!"),
                instance =>
                {
                    MarkRemovableForRemoval(instance);
                    LoggingManager.Instance.Info("Bye Tick Event!");
                });
        }

        public void RegisterUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }

        public void MarkUpdatableForRemoval(IUpdatable updatable)
        {
            _markedForRemoval.Enqueue(updatable);
        }

        public void MarkRemovableForRemoval(IManagedRemoval managedRemoval)
        {
            if(managedRemoval is IUpdatable removal)
                MarkUpdatableForRemoval(removal);
        }
    }
}