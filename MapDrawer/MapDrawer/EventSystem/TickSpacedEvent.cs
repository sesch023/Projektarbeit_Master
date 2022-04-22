using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using MapDrawer.ManagerSystem;

namespace MapDrawer.EventSystem
{
    public abstract class TickSpacedEvent : UpdatableEvent
    {
        private readonly long _tickSpacing;
        private long _lastTick;
        public TickSpacedEvent(long tickSpacing)
        {
            _tickSpacing = tickSpacing;
            _lastTick = 0;
        }

        public TickSpacedEvent(Subscriber subscriber, long tickSpacing) : base(subscriber)
        {
            _tickSpacing = tickSpacing;
            _lastTick = 0;
        }

        public TickSpacedEvent(IEnumerable<Subscriber> subscribers, long tickSpacing) : base(subscribers)
        {
            _tickSpacing = tickSpacing;
            _lastTick = 0;
        }
        
        public override void Update()
        {
            var passed = TimeManager.Instance.TimePassedSince(_lastTick);
            if (passed >= _tickSpacing)
            {
                TimedUpdate();
                _lastTick += passed;
            }
        }

        public abstract void TimedUpdate();
    }
}