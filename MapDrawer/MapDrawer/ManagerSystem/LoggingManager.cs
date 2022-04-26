using NLog;

namespace MapDrawer.ManagerSystem
{
    public sealed class LoggingManager : IManager
    {
        public const bool VerboseTimeManager = true;

        private LoggingManager()
        {
        }

        public static Logger Instance { get; } = LogManager.GetCurrentClassLogger();
    }
}