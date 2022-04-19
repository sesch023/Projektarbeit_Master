using System;

namespace MapDrawer
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MapDrawer())
                game.Run();
        }
    }
}