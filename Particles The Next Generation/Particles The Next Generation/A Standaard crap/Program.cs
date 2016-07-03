using System;

namespace Particles_The_Next_Generation
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (TheNextGeneration game = new TheNextGeneration())
            {
                game.Run();
            }
        }
    }
#endif
}

