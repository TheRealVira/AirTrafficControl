#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: AdvancedRectangle.cs
// Date - created:2016.08.30 - 17:48
// Date - current: 2016.08.30 - 19:00

#endregion

#region Usings

using Microsoft.Xna.Framework;

#endregion

namespace AirTrafficControl
{
    public struct AdvancedRectangle
    {
        public AdvancedRectangle(float x, float y, float width, float height, float angle)
            : this()
        {
            Initialise(new Vector2(x, y), width, height, angle);
        }

        public AdvancedRectangle(Vector2 position, float width, float height, float angle)
            : this()
        {
            Initialise(position, width, height, angle);
        }

        private void Initialise(Vector2 position, float width, float height, float angle)
        {
            Position = position;
            Width = width;
            Height = height;
            Angle = angle;
        }

        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Angle { get; set; }
        public Vector2 Center => new Vector2(Position.X + Width/2, Position.Y + Height/2);

        // TopLeft
        // TopRight
        // BottomRight
        // BottomLeft
        public Vector2[] Corners
            =>
                new[]
                {
                    new Vector2(Position.X, Position.Y).Rotate(Angle, Center),
                    new Vector2(Position.X + Width, Position.Y).Rotate(Angle,
                        Center),
                    new Vector2(Position.X + Width, Position.Y + Height).Rotate(Angle,
                        Center),
                    new Vector2(Position.X, Position.Y + Height).Rotate(Angle,
                        Center)
                };

        public bool ContainsPoint(Vector2 position)
        {
            return (position.X >= Position.X) && (position.X <= Position.X + Width) && (position.Y >= Position.Y) &&
                   (position.Y < Position.Y + Height);
        }
    }
}