#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Airplane.cs
// Date - created:2016.08.15 - 17:43
// Date - current: 2016.08.30 - 12:57

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
        private readonly Airport.Airport _goal;
        private readonly string _name;
        private readonly Vector2 _targetVector;

        public float _alpha = 1f;
        private Rectangle _boundings;
        private Vector2 _movement;
        private Vector2 _positon;
        private float _speed;
        public bool FadeOut;

        public Airplane(string name, Vector2 position, Airport.Airport goal)
        {
            _name = name;
            _positon = position;

            var targetVector = goal._boundings.ToVector2() - position; // Getting raw vector

            // Calculating the angle:
            _angle = (float) ((float) Math.Atan2(targetVector.Y, targetVector.X) + Math.PI/2f);

            targetVector.Normalize(); // Normalizing the raw vector (notice here, that we'll get some loss of precision)
            targetVector *= Constants.DEFAULT_SPEED; // Multiplying the normalized vector by my speedy constant.

            _movement = targetVector;
            UpdateBoundings();

            var goalVector = goal._boundings.ToVector2();
            _targetVector = goalVector - position; // Getting raw vector
            _targetVector.Normalize();

            _goal = goal;

            _speed = Constants.DEFAULT_SPEED;
        }

        public bool BeenSeen { get; private set; }

        public bool Landed { get; private set; }

        // TopLeft
        // TopRight
        // BottomRight
        // BottomLeft
        public Vector2[] Corners
            =>
                new[]
                {
                    new Vector2(_boundings.X, _boundings.Y).Rotate(_angle, _boundings.Center.ToVector2()),
                    new Vector2(_boundings.X + _boundings.Width, _boundings.Y).Rotate(_angle,
                        _boundings.Center.ToVector2()),
                    new Vector2(_boundings.X + _boundings.Width, _boundings.Y + _boundings.Height).Rotate(_angle,
                        _boundings.Center.ToVector2()),
                    new Vector2(_boundings.X, _boundings.Y + _boundings.Height).Rotate(_angle,
                        _boundings.Center.ToVector2())
                };

        public void Seen()
        {
            BeenSeen = true;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(Game1.Textures["Airplane"], _boundings, null, Color.White*_alpha, _angle,
                Game1.Textures["Airplane"].Bounds.Center.ToVector2(), SpriteEffects.None, 0);

            var offset = Game1.Fonts["Airplane"].MeasureString(ToString());
            sp.Draw(Game1.CoolPixle2016,
                new Rectangle((int) (_boundings.X - offset.X/2) - 3, _boundings.Y + _boundings.Height - 3,
                    (int) (offset.X + 6), (int) (offset.Y + 3)), Color.Black*_alpha);
            sp.DrawString(Game1.Fonts["Airplane"], ToString(),
                new Vector2(_boundings.X - offset.X/2, _boundings.Y + _boundings.Height), Color.Red*_alpha);

#if(DEBUG)
            var corners = Corners;
            for (int i = 0; i < corners.Length; i++)
            {
                sp.Draw(Game1.CoolPixle2016, new Rectangle((int)(corners[i].X - 1), (int)(corners[i].Y - 1), 3, 3), Color.Red);
            }
#endif
        }

        public void Update(GameTime gT)
        {
            #region DissolvingWhenLanded

            if (Landed)
            {
                _alpha -= (float) gT.ElapsedGameTime.TotalMilliseconds*
                          Constants.DISSOLVING_SPEED_OF_THE_AIRPLANE_WHEN_IN_GOAL;

                if (_alpha < 0)
                {
                    Game1.Airplanes.Remove(this);
                }

                return;
            }

            #endregion

            #region FadeOut

            if (FadeOut)
            {
                _alpha -= (float) gT.ElapsedGameTime.TotalMilliseconds*
                          Constants.DISSOLVING_SPEED_OF_THE_AIRPLANE_WHEN_RADAR;

                if (_alpha < 0)
                {
                    FadeOut = false;
                    _alpha = 1f;
                }
            }

            #endregion

            _positon += _movement*(gT.ElapsedGameTime.Milliseconds/500f);
            UpdateBoundings();

            _movement = _targetVector*_speed;

            _speed = _speed <= Constants.MIN_SPEED ? Constants.MIN_SPEED : _speed*Constants.SPEED_LOSS;

            var corners = Corners;
            for (var i = 0; i < corners.Length; i++)
            {
                if (_goal._innerBoundings.ContainsPoint(corners[i].ToPoint())) // We got to the goal!
                {
                    Landed = true;
                }
            }

            if (_positon.X < 0 || _positon.X > Constants.DisplayWidth || _positon.Y < 0 ||
                _positon.Y > Constants.DisplayHeight)
            {
                Game1.Airplanes.Remove(this);
            }
        }

        private void UpdateBoundings()
        {
            _boundings = new Rectangle((int) (_positon.X - 25),
                (int) (_positon.Y - 50), 50, 100);
        }


        public override string ToString()
        {
            return $"{_name}";
        }
    }
}