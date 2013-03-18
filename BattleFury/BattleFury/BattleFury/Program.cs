using System;

namespace BattleFury
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BattleFuryGame game = new BattleFuryGame())
            {
                game.Run();
            }
        }
    }
#endif
}

