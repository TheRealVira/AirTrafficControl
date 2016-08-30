#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Circle.cs
// Date - created:2016.08.29 - 21:57
// Date - current: 2016.08.30 - 12:58

#endregion

#region Usings

using Microsoft.Xna.Framework;

#endregion

namespace AirTrafficControl
{
    public struct Circle
    {
        public Circle(float x, float y, float radius)
            : this()
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public float Radius { get; }
        public float X { get; }
        public float Y { get; }

        public Vector2 ToVector2() => new Vector2(X, Y);

        public bool Intersects(Rectangle rectangle)
        {
            // the first thing we want to know is if any of the corners intersect
            var corners = new[]
            {
                new Point(rectangle.Top, rectangle.Left),
                new Point(rectangle.Top, rectangle.Right),
                new Point(rectangle.Bottom, rectangle.Right),
                new Point(rectangle.Bottom, rectangle.Left)
            };

            for (var i = 0; i < corners.Length; i++)
            {
                if (ContainsPoint(corners[i]))
                {
                    return true;
                }
            }

            // next we want to know if the left, top, right or bottom edges overlap
            if (X - Radius > rectangle.Right || X + Radius < rectangle.Left)
                return false;

            return Y - Radius <= rectangle.Bottom && Y + Radius >= rectangle.Top;
        }

        public bool ContainsPoint(Point point)
        {
            return new Vector2(point.X - X, point.Y - Y).Length() <= Radius;
        }
    }
}