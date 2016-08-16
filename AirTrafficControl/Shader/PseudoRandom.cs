using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static AirTrafficControl.Constants;

namespace AirTrafficControl.Shader
{
    public static class PseudoRandom
    {
        public static void Initialize(GraphicsDevice graphics)
        {
            Generated = new RenderTarget2D(graphics, DisplayWidth, DisplayHeight);
        }

        public static void Draw(GraphicsDevice graphics, SpriteBatch sp)
        {
            graphics.SetRenderTarget(Generated);
            graphics.Clear(ClearColor);

            // NOTICE: It'll create a black screen for no reason (which won't let anything drawing through), although it should be white and only some background imaging...
            sp.Begin(SpriteSortMode.Deferred, null, null, null, null, Game1.Shader["PseudoRandom"]); // TODO: That shader somehow destroys the universe... -> Find THE shield against it
            {
                sp.Draw(Game1.CoolPixle2016, new Rectangle(0, 0, DisplayWidth, DisplayHeight), Color.White); // Teal looks great.
            }
            sp.End();

            graphics.SetRenderTarget(DEFAULT_TARGET);
        }

        public static RenderTarget2D Generated;
    }
}
