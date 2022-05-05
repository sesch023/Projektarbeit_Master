using System;
using System.Diagnostics;
using MapDrawer.EventSystem;

namespace MapDrawer.ManagerSystem
{
    public class TimeManager : IManager, IUpdatable
    {
        // Count Tick at this percent of the Delta Milli.
        private const double MinDeltaRatio = 1.0;

        private readonly Stopwatch _timer;

        private uint _tps = 100;

        private long _tpsDeltaMilli;

        static TimeManager()
        {
            Instance = LoggingManager.VerboseTimeManager ? new VerboseTimeManager() : new TimeManager();
        }

        protected TimeManager()
        {
            _timer = new Stopwatch();
            _tpsDeltaMilli = CalculateMilliDelta();
            PassedTicks = 0;
            _timer.Start();
        }

        public static TimeManager Instance { get; }
        public long PassedTime => _timer.ElapsedMilliseconds;
        public long PassedTicks { get; private set; }
        public long LastTickTime { get; private set; }
        public long LastUpdateTime { get; private set; }
        public long LastUpdateDuration { get; private set; }

        public uint Tps
        {
            get => _tps;
            set
            {
                _tps = value;
                _tpsDeltaMilli = CalculateMilliDelta();
            }
        }

        public void Update()
        {
            var time = PassedTime;
            LastUpdateDuration = time - LastUpdateTime;
            LastUpdateTime = time;
            if (time >= LastTickTime + _tpsDeltaMilli)
            {
                PassedTicks++;
                TickPassed(time);
                LastTickTime = time;
            }
        }

        private long CalculateMilliDelta()
        {
            return (long) (1000.0 / _tps * MinDeltaRatio);
        }

        protected virtual void TickPassed(long time)
        {
        }

        public long TicksPassedSince(long since)
        {
            return PassedTicks - since;
        }

        public long TimePassedSince(long since)
        {
            return PassedTime - since;
        }

        public override string ToString()
        {
            return "Tick Number: " + PassedTicks;
        }
    }
}