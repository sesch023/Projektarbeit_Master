using System.Collections.Generic;

namespace MapDrawer.EventSystem
{
    public interface IEvent
    {
        public void TriggerSubscribers();

        public void AddSubscriber(ISubscriber subscriber);

        public void AddSubscribers(IEnumerable<ISubscriber> subscribers);

        public void RemoveSubscriber(ISubscriber subscriber);
    }
}