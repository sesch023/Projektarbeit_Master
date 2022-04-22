using NLog;

namespace MapDrawer.ManagerSystem
{
    public class LoggingManager : IManager
    {
        public static Logger Instance { get; } = NLog.LogManager.GetCurrentClassLogger();
        private LoggingManager()
        {
        }
    }
}