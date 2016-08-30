#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: PseudoRandom.cs
// Date - created:2016.08.16 - 14:48
// Date - current: 2016.08.30 - 16:56

#endregion

#region Usings

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static AirTrafficControl.Constants;

#endregion

namespace AirTrafficControl.Shader
{
    public static class PseudoRandom
    {
        public static RenderTarget2D Generated;

        public static void Initialize(GraphicsDevice graphics)
        {
            Generated = new RenderTarget2D(graphics, DisplayWidth, DisplayHeight);
        }

        public static void Draw(GraphicsDevice graphics, SpriteBatch sp, Random rand)
        {
            graphics.SetRenderTarget(Generated);
            graphics.Clear(ClearColor);

            //// NOTICE: It'll create a black screen for no reason (which won't let anything drawing through), although it should be white and only some background imaging...
            //sp.Begin(SpriteSortMode.Deferred, null, null, null, null, Game1.Shader["PseudoRandom"]); // TODO: That shader somehow destroys the universe... -> Find THE shield against it
            //{
            //    Game1.Shader["PseudoRandom"].CurrentTechnique.Passes[0].Apply();
            //    sp.Draw(Game1.CoolPixle2016, new Rectangle(0, 0, DisplayWidth, DisplayHeight), Color.White); // Teal looks great.
            //}
            //sp.End();

            var texture = new Color[DisplayHeight*DisplayWidth];
            for (var i = 0; i < texture.Length; i++)
            {
                var val = (float) rand.NextDouble();
                texture[i] = new Color(val, val, val, 1f);
            }
            Generated.SetData(texture);

            graphics.SetRenderTarget(DEFAULT_TARGET);
        }
    }
}