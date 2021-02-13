using System;
using TestGame.Services;

namespace TestGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            using (var game = new Startup(services))
            {
                game.Run();
            }
        }
    }
}
