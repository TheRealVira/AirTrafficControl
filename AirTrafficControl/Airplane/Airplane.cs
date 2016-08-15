#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Airplane.cs
// Date - created:2016.08.15 - 17:43
// Date - current: 2016.08.15 - 18:26

#endregion

#region Usings

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace AirTrafficControl.Airplane
{
    internal class Airplane
    {
        private readonly float _angle;
        private readonly Vector2 _movement;
        private readonly string _name;
        private Vector2 _positon;
        public Rectangle Boundings;

        public Airplane(string name, Vector2 positon, Vector2 movement)
        {
            _name = name;
            _positon = positon;
            _movement = movement;
            UpdateBoundings();
            _angle = (_positon + _movement).VectorToAngle() + (float) Math.PI/2;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(Game1.Textures["Airplane"], Boundings, null, Color.White, _angle,
                Game1.Textures["Airplane"].Bounds.Center.ToVector2(), SpriteEffects.None, 0);
        }

        public void Update(GameTime gT)
        {
            _positon += _movement*(gT.ElapsedGameTime.Milliseconds/500f);
            UpdateBoundings();
        }

        private void UpdateBoundings()
        {
            Boundings = new Rectangle((int) (_positon.X - Game1.Textures["Airplane"].Width/2),
                (int) (_positon.Y - Game1.Textures["Airplane"].Height/2), Game1.Textures["Airplane"].Width,
                Game1.Textures["Airplane"].Height);
        }


        public override string ToString()
        {
            return $"{_name}";
        }
    }
}