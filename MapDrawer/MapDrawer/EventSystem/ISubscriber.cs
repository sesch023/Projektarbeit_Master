namespace MapDrawer.EventSystem
{
    public interface ISubscriber
    {
        public void Trigger(Event triggeredBy);
    }
}