#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Airplane.cs
// Date - created:2016.08.30 - 17:04
// Date - current: 2016.08.30 - 18:59

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
        private readonly Airport.Airport _goal;
        private readonly string _name;
        private readonly Vector2 _targetVector;
        private float _speed;

        public float Alpha = 1f;
        public bool FadeOut;

        public Airplane(string name, Vector2 position, Airport.Airport goal)
        {
            _name = name;

            _targetVector = goal._boundings.ToVector2() -
                            new Vector2(position.X + Constants.AIRPLANE_WIDTH/2f,
                                position.Y + Constants.AIRPLANE_HEIGHT/2f); // Getting raw vector
            _targetVector.Normalize();
                // Normalizing the raw vector (notice here, that we'll get some loss of precision)

            Boundings = new AdvancedRectangle(position.X, position.Y, Constants.AIRPLANE_WIDTH,
                Constants.AIRPLANE_HEIGHT, (float) (Math.Atan2(_targetVector.Y, _targetVector.X) + Math.PI/2));

            _goal = goal;

            _speed = Constants.DEFAULT_SPEED;
            Landed = false;
        }

        public bool BeenSeen { get; private set; }

        public bool Landed { get; private set; }

        public AdvancedRectangle Boundings { get; private set; }

        public void Seen()
        {
            BeenSeen = true;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(Game1.Textures["Airplane"],
                new Rectangle((int) (Boundings.Position.X + Boundings.Width/2),
                    (int) (Boundings.Position.Y + Boundings.Height/2), (int) Boundings.Width,
                    (int) Boundings.Height), null, Color.White*Alpha, Boundings.Angle,
                Game1.Textures["Airplane"].Bounds.Center.ToVector2(), SpriteEffects.None, 0f);

            var offset = Game1.Fonts["Airplane"].MeasureString(ToString());
            sp.Draw(Game1.CoolPixle2016,
                new Rectangle((int) (Boundings.Position.X - offset.X/2) - 3,
                    (int) (Boundings.Position.Y + Boundings.Height - 3),
                    (int) (offset.X + 6), (int) (offset.Y + 3)), Color.Black*Alpha);
            sp.DrawString(Game1.Fonts["Airplane"], ToString(),
                new Vector2(Boundings.Position.X - offset.X/2, Boundings.Position.Y + Boundings.Height), Color.Red*Alpha);

#if(DEBUG)
            var corners = Boundings.Corners;
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
                Alpha -= (float) gT.ElapsedGameTime.TotalMilliseconds*
                         Constants.DISSOLVING_SPEED_OF_THE_AIRPLANE_WHEN_IN_GOAL;

                Boundings =
                    new AdvancedRectangle(
                        Boundings.Position + _targetVector*_speed*(gT.ElapsedGameTime.Milliseconds/500f)*Alpha,
                        Boundings.Width,
                        Boundings.Height, Boundings.Angle);

                if (Alpha < 0)
                {
                    Game1.Airplanes.Remove(this);
                }

                return;
            }

            #endregion

            #region FadeOut

            if (FadeOut)
            {
                Alpha -= (float) gT.ElapsedGameTime.TotalMilliseconds*
                         Constants.DISSOLVING_SPEED_OF_THE_AIRPLANE_WHEN_RADAR;

                if (Alpha < 0)
                {
                    FadeOut = false;
                    Alpha = 1f;
                }
            }

            #endregion

            Boundings =
                new AdvancedRectangle(
                    Boundings.Position + _targetVector*(gT.ElapsedGameTime.Milliseconds/500f)*_speed, Boundings.Width,
                    Boundings.Height, Boundings.Angle);

            _speed = _speed <= Constants.MIN_SPEED ? Constants.MIN_SPEED : _speed*Constants.SPEED_LOSS;

            if (Boundings.ContainsPoint(_goal._innerBoundings.ToVector2()))
            {
                Landed = true;
            }

            //var corners = Corners;
            //for (var i = 0; i < corners.Length; i++)
            //{
            //    if (_goal._innerBoundings.ContainsPoint(corners[i].ToPoint())) // We got to the goal!
            //    {
            //        Landed = true;
            //    }
            //}

            if (Boundings.Position.X < 0 || Boundings.Position.X > Constants.DisplayWidth || Boundings.Position.Y < 0 ||
                Boundings.Position.Y > Constants.DisplayHeight)
            {
                Game1.Airplanes.Remove(this);
            }
        }


        public override string ToString()
        {
            return $"{_name}";
        }
    }
}