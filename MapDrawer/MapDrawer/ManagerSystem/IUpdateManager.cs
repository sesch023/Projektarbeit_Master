using MapDrawer.EventSystem;

namespace MapDrawer.ManagerSystem
{
    public interface IUpdateManager : IManager, IUpdatable
    {
        public void RegisterUpdatable(IUpdatable updatable);

        public void RemoveUpdatable(IUpdatable updatable);
        
        public void MarkUpdatableForRemoval(IUpdatable updatable);

        public void MarkRemovableForRemoval(IManagedRemoval managedRemoval);
    }
}