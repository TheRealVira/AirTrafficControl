#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Extensions.cs
// Date - created:2016.08.15 - 15:37
// Date - current: 2016.08.30 - 18:59

#endregion

#region Usings

using System;
using System.Collections.Generic;
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

        /// <summary>
        ///     Rotates the vector with the given angle (rotation).
        /// </summary>
        /// <param name="vector">Your vector, which should be rotated.</param>
        /// <param name="angle">The angle has to be in radians. (NOT DEGREES!!!!)</param>
        /// <param name="origin">The point, where the value should rotate arround.</param>
        /// <returns>Returns your rotated vector.</returns>
        public static Vector2 Rotate(this Vector2 vector, float angle, Vector2 origin)
            => Vector2.Transform(vector - origin, Matrix.CreateRotationZ(angle)) + origin;

        //public static float ToRadians(this float value) => (float) (value/1*(Math.PI/180f));
    }
}