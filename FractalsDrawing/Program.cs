using System;

namespace FractalsDrawing
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
