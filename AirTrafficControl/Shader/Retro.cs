#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Retro.cs
// Date - created:2016.08.16 - 12:55
// Date - current: 2016.08.16 - 13:12

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
        private static float RumbleTime;
        private static float DontRumble;
        private static bool Rumbleing;

        public static void Retrorize(SpriteBatch spriteBatch, RenderTarget2D toRetrorize)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, Game1.Shader["Retro"]);
            {
                for (var i = 0; i < Game1.Shader["Retro"].CurrentTechnique.Passes.Count; i++)
                {
                    Game1.Shader["Retro"].CurrentTechnique.Passes[i].Apply();
                    spriteBatch.Draw(toRetrorize, new Rectangle(0, 0, DisplayWidth, DisplayHeight), Color.White);
                }
            }

            spriteBatch.End();
        }

        public static void Update(GameTime gameTime, Random rand)
        {
            if (DontRumble <= 0)
            {
                if (Rumbleing)
                {
                    RumbleTime -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    Rumbleing = true;
                    RumbleTime = rand.Next(MIN_TIME_RUMBELING, MAX_TIME_RUMBELING);

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

                if (RumbleTime <= 0)
                {
                    Rumbleing = false;
                    DontRumble = rand.Next(MIN_TIME_NOT_RUMBELING, MAX_TIME_NOT_RUMBELING);
                    Game1.Shader["Retro"].Parameters["RumbleVectorR"].SetValue(Vector2.Zero);
                    Game1.Shader["Retro"].Parameters["RumbleVectorG"].SetValue(Vector2.Zero);
                    Game1.Shader["Retro"].Parameters["RumbleVectorB"].SetValue(Vector2.Zero);
                }
            }
            else
            {
                DontRumble -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}