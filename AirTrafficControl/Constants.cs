#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Constants.cs
// Date - created:2016.08.15 - 14:34
// Date - current: 2016.08.15 - 18:28

#endregion

#region Usings

using Microsoft.Xna.Framework;

#endregion

namespace AirTrafficControl
{
    public static class Constants
    {
        public const string TextureDir = "Textures/";
        public const float MAX_RAD = 500f;
        public const float MIN_RAD = 250f;
        public const int AIRPLANE_COUNT = 1;

        // Will get initlized in Game1.cs
        public static int DISPLAY_WIDTH;
        public static int DISPLAY_HEIGHT;
        public static Color ClearColor;
    }
}