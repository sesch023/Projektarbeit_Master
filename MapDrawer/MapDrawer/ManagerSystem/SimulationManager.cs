using System;
using MapDrawer.CameraSystem;

namespace MapDrawer.ManagerSystem
{
    public class SimulationManager : IManager
    {
        private const float UpdateTimeFactor = 50.0f;
        public static SimulationManager Instance { get; }
        public CameraController CameraController { get;  }

        static SimulationManager()
        {
            Instance = new SimulationManager();
        }
        
        private SimulationManager()
        {
            CameraController = new CameraController();
        }

        public float CalculateUpdateTimeFactor()
        {
            return TimeManager.Instance.LastUpdateDuration / UpdateTimeFactor;
        }
    }
}