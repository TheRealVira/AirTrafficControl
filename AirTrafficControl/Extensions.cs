#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Extensions.cs
// Date - created:2016.08.15 - 15:37
// Date - current: 2016.08.15 - 18:28

#endregion

#region Usings

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#endregion

namespace AirTrafficControl
{
    internal static class Extensions
    {
        public static bool KeyWasPressed(this KeyboardState newK, KeyboardState oldK, Keys key)
            => newK.IsKeyUp(key) && oldK.IsKeyDown(key);

        public static float VectorToAngle(this Vector2 position) => (float) Math.Atan2(position.Y, position.X);
    }
}