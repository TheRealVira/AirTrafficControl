#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Retro.cs
// Date - created:2016.08.16 - 12:55
// Date - current: 2016.08.30 - 18:59

#endregion

#region Usings

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static AirTrafficControl.Constants;

#endregion

namespace AirTrafficControl.Shader
{
    internal static class Retro
    {
        private static float _rumbleTime;
        private static float _dontRumble;
        private static bool _rumbleing;

        public static void Initialize(Random rand)
        {
            _dontRumble = rand.Next(MIN_TIME_NOT_RUMBELING, MAX_TIME_NOT_RUMBELING);
        }

        public static void Retrorize(SpriteBatch spriteBatch, RenderTarget2D toRetrorize)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, Game1.Shader["Retro"]);
            {
                Game1.Shader["Retro"].CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(toRetrorize, new Rectangle(0, 0, DisplayWidth, DisplayHeight), Color.White);
            }

            spriteBatch.End();
        }

        public static void Update(GameTime gameTime, Random rand)
        {
            if (_dontRumble <= 0)
            {
                if (_rumbleing)
                {
                    _rumbleTime -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    _rumbleing = true;
                    _rumbleTime = rand.Next(MIN_TIME_RUMBELING, MAX_TIME_RUMBELING);

                    var rX = (rand.Next(0, 5) - 2)*
                             (gameTime.ElapsedGameTime.Milliseconds/(float) gameTime.ElapsedGameTime.TotalMilliseconds);
                    var rY = (rand.Next(0, 5) - 2)*
                             (gameTime.ElapsedGameTime.Milliseconds/(float) gameTime.ElapsedGameTime.TotalMilliseconds);
                    var r1X = (rand.Next(0, 5) - 2)*
                              (gameTime.ElapsedGameTime.Milliseconds/(float) gameTime.ElapsedGameTime.TotalMilliseconds);
                    var r1Y = (rand.Next(0, 5) - 2)*
                              (gameTime.ElapsedGameTime.Milliseconds/(float) gameTime.ElapsedGameTime.TotalMilliseconds);
                    var r2X = (rand.Next(0, 5) - 2)*
                              (gameTime.ElapsedGameTime.Milliseconds/(float) gameTime.ElapsedGameTime.TotalMilliseconds);
                    var r2Y = (rand.Next(0, 5) - 2)*
                              (gameTime.ElapsedGameTime.Milliseconds/(float) gameTime.ElapsedGameTime.TotalMilliseconds);

                    Game1.Shader["Retro"].Parameters["RumbleVectorR"].SetValue(
                        new Vector2(rX/DisplayWidth,
                            rY/DisplayHeight));
                    Game1.Shader["Retro"].Parameters["RumbleVectorG"].SetValue(
                        new Vector2(r1X/DisplayWidth,
                            r1Y/DisplayHeight));
                    Game1.Shader["Retro"].Parameters["RumbleVectorB"].SetValue(
                        new Vector2(r2X/DisplayWidth,
                            r2Y/DisplayHeight));
                }

                if (_rumbleTime <= 0)
                {
                    _rumbleing = false;
                    _dontRumble = rand.Next(MIN_TIME_NOT_RUMBELING, MAX_TIME_NOT_RUMBELING);
                    Game1.Shader["Retro"].Parameters["RumbleVectorR"].SetValue(Vector2.Zero);
                    Game1.Shader["Retro"].Parameters["RumbleVectorG"].SetValue(Vector2.Zero);
                    Game1.Shader["Retro"].Parameters["RumbleVectorB"].SetValue(Vector2.Zero);
                }
            }
            else
            {
                _dontRumble -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}