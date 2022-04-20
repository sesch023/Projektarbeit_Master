using System.Collections.Generic;

namespace MapDrawer.EventSystem
{
    public abstract class UpdatableEvent : Event, IUpdatable
    {
        public UpdatableEvent(){ }
        
        public UpdatableEvent(ISubscriber subscriber) : base(subscriber){}

        public UpdatableEvent(IEnumerable<ISubscriber> subscribers) : base(subscribers){}
        
        public abstract void Update();
    }
}