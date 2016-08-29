#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Airplane.cs
// Date - created:2016.08.15 - 17:43
// Date - current: 2016.08.16 - 13:12

#endregion

#region Usings

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace AirTrafficControl.Airplane
{
    public class Airplane
    {
        private readonly float _angle;
        private Vector2 _movement;
        private readonly string _name;
        private Vector2 _positon;
        public Rectangle Boundings;
        private readonly Vector2 _targetVector;
        private readonly Airport.Airport _goal;
        public Circle MiddleCircle;
        private float _speed;

        public Airplane(string name, Vector2 positon, Vector2 movement, Airport.Airport goal)
        {
            _name = name;
            _positon = positon;
            _movement = movement;
            UpdateBoundings();

            // Calculating the angle:
            var deltaY = goal._boundings.Y - positon.Y;
            var deltaX = goal._boundings.X - positon.X;
            _angle = (float) Math.Atan2(deltaY, deltaX);

            var goalVector = goal._boundings.ToVector2();
            _targetVector = goalVector - positon; // Getting raw vector
            _targetVector.Normalize();

            _goal = goal;

            _speed = Constants.DEFAULT_SPEED;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(Game1.Textures["Airplane"], Boundings, null, Color.White,0 /*_angle*/,
                Game1.Textures["Airplane"].Bounds.Center.ToVector2(), SpriteEffects.None, 0);
        }

        public void Update(GameTime gT)
        {
            _positon += _movement * (gT.ElapsedGameTime.Milliseconds / 500f);
            UpdateBoundings();

            _movement = _targetVector * _speed;

            _speed = _speed <= Constants.MIN_SPEED ? Constants.MIN_SPEED : _speed* Constants.SPEED_LOSS;

            if (_goal._innerBoundings.Intersects(Boundings)) // We got to the goal!
            {
                Game1.Airplanes.Remove(this);
            }
        }

        private void UpdateBoundings()
        {
            Boundings = new Rectangle((int) (_positon.X - 25),
                (int) (_positon.Y - 50), 50, 100);
            MiddleCircle=new Circle(_positon.X,_positon.Y,10);
        }


        public override string ToString()
        {
            return $"{_name}";
        }
    }
}