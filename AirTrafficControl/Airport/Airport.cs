#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Airport.cs
// Date - created:2016.08.15 - 14:32
// Date - current: 2016.08.15 - 18:28

#endregion

#region Usings

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace AirTrafficControl.Airport
{
    internal class Airport
    {
        public readonly Rectangle _boundings;
        private readonly Vector2 _position;
        private readonly string _radarName;
        private readonly float _radius;

        public Airport(string radarName, Vector2 position, float radius)
        {
            _radarName = radarName;
            _position = position;
            _radius = radius;

            var divB2 = _radius/2;
            _boundings = new Rectangle((int) (_position.X - divB2), (int) (_position.Y - divB2),
                (int) _radius, (int) _radius);
        }

        public override string ToString()
        {
            return $"{_radarName}:  {_position} / {_radius} km";
        }

        public void Draw(SpriteBatch drawing, GameTime gameTime, Color color)
        {
            drawing.Draw(Game1.Textures["Radar"], _boundings, color);
        }

        public void DrawFilled(SpriteBatch drawing, GameTime gameTime, Color color)
        {
            drawing.Draw(Game1.Textures["FilledRadar"], _boundings, color);
        }

        public bool Contains(Vector2 point) => _boundings.Contains(point);
        public bool Contains(Rectangle bounds) => _boundings.Contains(bounds);
    }
}