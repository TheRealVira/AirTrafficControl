#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Grid.cs
// Date - created:2016.08.15 - 17:32
// Date - current: 2016.08.15 - 18:28

#endregion

#region Usings

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static AirTrafficControl.Game1;

#endregion

namespace AirTrafficControl
{
    internal static class Grid
    {
        private static readonly Vector4 COLOR1 = new Vector4(0, 1, 0, 1);

        public static void Draw(SpriteBatch sp)
        {
            sp.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, Shader["Grid"]);
            {
                Shader["Grid"].Parameters["ScreenCoords"].SetValue(new Vector2(Constants.DISPLAY_WIDTH,
                    Constants.DISPLAY_HEIGHT));
                Shader["Grid"].Parameters["Color1"].SetValue(COLOR1);
                Shader["Grid"].Parameters["Color2"].SetValue(Constants.ClearColor.ToVector4());
                Shader["Grid"].CurrentTechnique.Passes[0].Apply();
                sp.Draw(CoolPixle2016, new Rectangle(0, 0, Constants.DISPLAY_WIDTH, Constants.DISPLAY_HEIGHT),
                    Color.Transparent);
            }
            sp.End();
        }
    }
}