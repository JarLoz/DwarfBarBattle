using System;

namespace DBB
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DBBGame game = new DBBGame())
            {
                game.Run();
            }
        }
    }
#endif
}

