
using System.Linq;
using Microsoft.Xna.Framework;

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

        public float Radius { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }

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

            Circle tmpThis = this;
            if (corners.Any(corner => tmpThis.ContainsPoint(corner)))
            {
                return true;
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
