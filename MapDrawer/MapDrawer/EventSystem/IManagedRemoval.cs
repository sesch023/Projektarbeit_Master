namespace MapDrawer.EventSystem
{
    public delegate void OnRemoval(IManagedRemoval triggeredBy);

    public interface IManagedRemoval
    {
        public void ManageRemoval();
    }
}