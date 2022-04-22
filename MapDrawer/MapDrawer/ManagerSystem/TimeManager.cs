using System;
using System.Diagnostics;
using MapDrawer.EventSystem;

namespace MapDrawer.ManagerSystem
{
    public class TimeManager : IManager, IUpdatable
    {
        public static TimeManager Instance { get; }

        private readonly Stopwatch _timer;
        public long PassedTime => _timer.ElapsedMilliseconds;

        public long PassedTicks { get; private set; } = 0;

        private uint _tps = 1;
        public uint Tps
        {
            get => _tps;
            set
            {
                _tps = value;
                _tpsDeltaMilli = CalculateMilliDelta();
            }
        }
        
        private long _tpsDeltaMilli;
        private long _lastTickTime;

        static TimeManager()
        {
            Instance = new TimeManager();
        }

        private TimeManager()
        {
            _timer = new Stopwatch();
            _tpsDeltaMilli = CalculateMilliDelta();
            _lastTickTime = 0;
            PassedTicks = 0;
            _timer.Start();
        }

        private long CalculateMilliDelta()
        {
            return 60000 / _tps;
        }

        public void Update()
        {
            long time = PassedTime;
            if (_lastTickTime + _tpsDeltaMilli >= time)
            {
                PassedTicks++;
                _lastTickTime = time;
                LoggingManager.Instance.Info(PassedTicks);
            }
        }
    }
}