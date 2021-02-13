using OrbitalPhysics.Services;
using System;

namespace OrbitalPhysics
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Startup())
                game.Run();
        }
    }
}
