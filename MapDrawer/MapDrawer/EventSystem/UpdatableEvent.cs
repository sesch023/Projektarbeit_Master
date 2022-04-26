using System.Collections.Generic;

namespace MapDrawer.EventSystem
{
    public abstract class UpdatableEvent : Event, IUpdatable
    {
        public UpdatableEvent()
        {
        }

        public UpdatableEvent(Subscriber subscriber) : base(subscriber)
        {
        }

        public UpdatableEvent(IEnumerable<Subscriber> subscribers) : base(subscribers)
        {
        }

        public abstract void Update();
    }
}