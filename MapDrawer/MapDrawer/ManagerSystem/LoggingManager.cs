using NLog;

namespace MapDrawer.ManagerSystem
{
    public class LoggingManager : IManager
    {
        public const bool EnableDebugMode = true;
        public static Logger Instance { get; } = NLog.LogManager.GetCurrentClassLogger();
        private LoggingManager()
        {
        }
    }
}