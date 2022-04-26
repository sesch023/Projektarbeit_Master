using MapDrawer.Util;

namespace MapDrawer.ManagerSystem
{
    public sealed class VerboseTimeManager : TimeManager
    {
        private const int MavgSize = 50;
        private readonly MovingAverageLong _mavg;

        public VerboseTimeManager()
        {
            _mavg = new MovingAverageLong(MavgSize);
        }

        public decimal AverageTickMs => _mavg.Average;

        protected override void TickPassed(long time)
        {
            _mavg.ComputeAverage(time - LastTickTime);
            LoggingManager.Instance.Info(this);
        }

        public override string ToString()
        {
            return base.ToString() + ", Average MS per Tick: " + _mavg.Average + ", Target Tick Time: " + 1000.0 / Tps;
        }
    }
}