using System.Collections.Generic;
using MapDrawer.ManagerSystem;

namespace MapDrawer.EventSystem
{
    public abstract class TimeSpacedEvent : UpdatableEvent
    {
        public TimeSpacedEvent(){ }
        
        public TimeSpacedEvent(ISubscriber subscriber) : base(subscriber){}

        public TimeSpacedEvent(IEnumerable<ISubscriber> subscribers) : base(subscribers){}
        
        public override void Update()
        {
        }

        public abstract void TimedUpdate();
    }
}