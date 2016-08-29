#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Extensions.cs
// Date - created:2016.08.15 - 15:37
// Date - current: 2016.08.16 - 13:12

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
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

        public static T RandomItem<T>(this IList<T> items, Random rand)
            => items == null || rand == null ? default(T) : items[rand.Next(0, items.Count)];
    }
}