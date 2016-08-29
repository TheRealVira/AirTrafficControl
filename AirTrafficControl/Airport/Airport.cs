#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Airport.cs
// Date - created:2016.08.15 - 14:32
// Date - current: 2016.08.16 - 13:12

#endregion

#region Usings

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace AirTrafficControl.Airport
{
    public class Airport
    {
        public readonly Circle _boundings;
        public readonly Circle _innerBoundings;
        private readonly Rectangle DrawRectangle;
        private readonly Vector2 _position;
        private readonly string _radarName;
        private readonly float _radius;

        public Airport(string radarName, Vector2 position, float radius)
        {
            _radarName = radarName;
            _position = position;
            _radius = radius;

            var divB2 = radius/2;
            DrawRectangle = new Rectangle((int)(_position.X - divB2), (int)(_position.Y - divB2),
                (int)_radius, (int)_radius);

            _boundings = new Circle(position.X,position.Y,radius*.3f);

            _innerBoundings= new Circle(position.X, position.Y, radius/10);
        }

        public override string ToString()
        {
            return $"{_radarName}:  {_position} / {_radius} km";
        }

        public void Draw(SpriteBatch drawing, GameTime gameTime, Color color)
        {
            drawing.Draw(Game1.Textures["Radar"], DrawRectangle, color);
        }

        public void DrawFilled(SpriteBatch drawing, GameTime gameTime, Color color)
        {
            drawing.Draw(Game1.Textures["FilledRadar"], DrawRectangle, color);
        }
        
        public bool Contains(Rectangle bounds) => _boundings.Intersects(bounds);
    }
}