using System.Data;
using MapDrawer.Util;

namespace MapDrawer.ManagerSystem
{
    public class VerboseTimeManager : TimeManager
    {
        private const int MavgSize = 50;
        private readonly MovingAverageLong _mavg;

        public decimal AverageTickMs => _mavg.Average;

        public VerboseTimeManager()
        {
            _mavg = new MovingAverageLong(MavgSize);
        }

        public override void Update()
        {
            base.Update();
            _mavg.ComputeAverage(LastTickTime - SecondLastTickTime);
            LoggingManager.Instance.Info(this);
        }

        public override string ToString()
        {
            return base.ToString() + ", Average MS per Tick: " + _mavg.Average + ", Target Tick Time: " + 1000.0/Tps;
        }
    }
    
    
}