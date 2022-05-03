﻿using System.Collections.Generic;
using MapDrawer.ManagerSystem;

namespace MapDrawer.EventSystem
{
    public abstract class UpdatableEvent : Event, IUpdatable
    {
        public UpdatableEvent()
        {
            Init();
        }

        public UpdatableEvent(Subscriber subscriber) : base(subscriber)
        {
            Init();
        }

        public UpdatableEvent(IEnumerable<Subscriber> subscribers) : base(subscribers)
        {
            Init();
        }

        private void Init()
        {
            UpdateManager.Instance.RegisterUpdatable(this);
        }

        public abstract void Update();
    }
}