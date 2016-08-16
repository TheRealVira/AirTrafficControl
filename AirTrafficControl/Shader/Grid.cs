#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Grid.cs
// Date - created:2016.08.16 - 12:55
// Date - current: 2016.08.16 - 13:12

#endregion

#region Usings

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace AirTrafficControl.Shader
{
    internal static class Grid
    {
        public static void Initialize()
        {
            Game1.Shader["Grid"].Parameters["ScreenCoords"].SetValue(new Vector2(Constants.DisplayWidth,
                Constants.DisplayHeight));
            Game1.Shader["Grid"].Parameters["Div"].SetValue(5f);
            Game1.Shader["Grid"].Parameters["Color1"].SetValue(COLOR1);
            Game1.Shader["Grid"].Parameters["Color2"].SetValue(Color.Transparent.ToVector4());
            Game1.Shader["Grid"].CurrentTechnique.Passes[0].Apply();
        }

        private static readonly Vector4 COLOR1 = new Vector4(.01f, .1f, .01f, 1);

        public static void Draw(SpriteBatch sp)
        {
            sp.Begin(SpriteSortMode.Deferred, null, null, null, null, Game1.Shader["Grid"]);
            {
                sp.Draw(Game1.CoolPixle2016, new Rectangle(0, 0, Constants.DisplayWidth, Constants.DisplayHeight),
                    Color.Transparent);
            }
            sp.End();
        }
    }
}