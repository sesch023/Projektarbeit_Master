using System;
using MapDrawer.ManagerSystem;
using NLog;

namespace MapDrawer
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        [STAThread]
        static void Main()
        {
            LoggingManager.Instance.Info("Hello World");
            using (var game = new MapDrawer())
                game.Run();
        }
    }
}