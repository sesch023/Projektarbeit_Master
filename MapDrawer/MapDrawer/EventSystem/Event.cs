using System.Collections.Generic;

namespace MapDrawer.EventSystem
{
    public abstract class Event : IEvent
    {
        private readonly List<ISubscriber> _subscribers;

        public Event()
        {
            _subscribers = new List<ISubscriber>();
        }
        
        public Event(ISubscriber subscriber) : base()
        {
            AddSubscriber(subscriber);
        }

        public Event(IEnumerable<ISubscriber> subscribers) : base()
        {
            AddSubscribers(subscribers);
        }

        public void TriggerSubscribers()
        {
            foreach(ISubscriber subscriber in _subscribers){
                subscriber.Trigger(this);
            }
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void AddSubscribers(IEnumerable<ISubscriber> subscribers)
        {
            _subscribers.AddRange(subscribers);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }
    }
}