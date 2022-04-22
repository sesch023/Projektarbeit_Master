using System;
using System.Diagnostics;
using System.Drawing.Printing;
using MapDrawer.EventSystem;
using MapDrawer.Util;

namespace MapDrawer.ManagerSystem
{
    public class TimeManager : IManager, IUpdatable
    {
        public static TimeManager Instance { get; }
        
        private readonly Stopwatch _timer;
        public long PassedTime => _timer.ElapsedMilliseconds;
        public long PassedTicks { get; private set; } = 0;
        public long LastTickTime { get; private set; } = 0;

        private uint _tps = 1;
        // Count Tick at this percent of the Delta Milli.
        private const double MinDeltaRatio = 0.9;

        protected long SecondLastTickTime = 0;
        private long _tpsDeltaMilli;

        public uint Tps
        {
            get => _tps;
            set
            {
                _tps = value;
                _tpsDeltaMilli = CalculateMilliDelta();
            }
        }

        static TimeManager()
        {
            Instance = LoggingManager.EnableDebugMode ? new VerboseTimeManager() : new TimeManager();
        }

        protected TimeManager()
        {
            _timer = new Stopwatch();
            _tpsDeltaMilli = CalculateMilliDelta();
            PassedTicks = 0;
            _timer.Start();
        }

        private long CalculateMilliDelta()
        {
            return (long)((1000.0 / _tps) * MinDeltaRatio);
        }

        public void Update()
        {
            var time = PassedTime;
            if (time >= LastTickTime + _tpsDeltaMilli)
            {
                PassedTicks++;
                TickPassed();
                SecondLastTickTime = LastTickTime;
                LastTickTime = time;
            }
        }

        protected virtual void TickPassed()
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