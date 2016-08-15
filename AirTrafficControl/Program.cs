#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Program.cs
// Date - created:2016.08.15 - 14:25
// Date - current: 2016.08.15 - 18:28

#endregion

#region Usings

using System;

#endregion

namespace AirTrafficControl
{
    /// <summary>
    ///     The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}