#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Game1.cs
// Date - created:2016.08.15 - 14:25
// Date - current: 2016.08.30 - 12:58

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficControl.Content;
using AirTrafficControl.Shader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static AirTrafficControl.Constants;

#endregion

namespace AirTrafficControl
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static Texture2D CoolPixle2016;

        public static KeyboardState NewKeyboardState, OldKeyboardState;
        public static MouseState OldMouseState, NewMouseState;
        private static Random _rand;

        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, Effect> Shader;
        public static Dictionary<string, SpriteFont> Fonts;
        public static List<Airplane.Airplane> Airplanes;
        public static List<Airport.Airport> Airports;

        private readonly GraphicsDeviceManager _graphics;
        private RenderTarget2D _shadRenderTarget;
        private SpriteBatch _spriteBatch;

        private float RadarLikeLine_X;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
#if (DEBUG)
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
#else
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
#endif

            DisplayWidth = _graphics.PreferredBackBufferWidth;
            DisplayHeight = _graphics.PreferredBackBufferHeight;

            NewMouseState = new MouseState();
            NewKeyboardState = new KeyboardState(Keys.A);
            OldMouseState = NewMouseState;
            OldKeyboardState = NewKeyboardState;

            CoolPixle2016 = new Texture2D(GraphicsDevice, 1, 1);
            CoolPixle2016.SetData(new[] {Color.White});
            ClearColor = Color.Black;
            _rand = new Random(DateTime.Now.Millisecond);

            _shadRenderTarget = new RenderTarget2D(_graphics.GraphicsDevice, DisplayWidth, DisplayHeight);

            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures = ContentLib.LoadStuff<Texture2D>(Content, TEXTURE_DIR);
            Shader = ContentLib.LoadStuff<Effect>(Content, SHADER_DIR);
            Fonts = ContentLib.LoadStuff<SpriteFont>(Content, FONTS_DIR);

            Grid.Initialize();
            Retro.Initialize(_rand);
            PseudoRandom.Initialize(_graphics.GraphicsDevice);
            PseudoRandom.Draw(_graphics.GraphicsDevice, _spriteBatch, _rand); // Generate the random map

            Airports = TheAirportFactory.Factorize(_rand, AIRPORT_COUNT).ToList();
            Airplanes = TheAirplaneFactory.Factorize(_rand, 1).ToList();
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            NewKeyboardState = Keyboard.GetState();
            NewMouseState = Mouse.GetState();

            if (NewKeyboardState.KeyWasPressed(OldKeyboardState, Keys.Escape))
            {
                Exit();
                base.Update(gameTime);
                return;
            }

            if (NewKeyboardState.KeyWasPressed(OldKeyboardState, Keys.R))
            {
                Airports.Clear();
                Airports =
                    TheAirportFactory.Factorize(_rand, AIRPORT_COUNT).ToList();

                Airplanes.Clear();
            }

            RadarLikeLine_X += (float) gameTime.ElapsedGameTime.TotalMilliseconds*0.5f;
            if (RadarLikeLine_X > DisplayWidth + 100)
            {
                RadarLikeLine_X = -100;
            }

            Retro.Update(gameTime, _rand);

            for (var i = Airplanes.Count - 1; i > -1; i--)
            {
                Airplanes[i].Update(gameTime);
            }

            if (Airplanes.Count < AIRPLANE_COUNT)
            {
                Airplanes.AddRange(TheAirplaneFactory.Factorize(_rand, 1));
            }

            OldMouseState = NewMouseState;
            OldKeyboardState = NewKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Draw all stuff into a texture
            GraphicsDevice.Clear(ClearColor);
            DrawScene(gameTime);

            Retro.Retrorize(_spriteBatch, _shadRenderTarget);

            // Drawing the grid by my grid shader
            Grid.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        private void DrawScene(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_shadRenderTarget);
            GraphicsDevice.Clear(ClearColor);

            _spriteBatch.Begin();
            {
                var radarLikeRect = new Rectangle((int) RadarLikeLine_X, 0, 100, DisplayHeight);

                _spriteBatch.Draw(PseudoRandom.Generated, new Rectangle(0, 0, DisplayWidth, DisplayHeight),
                    Color.DarkOliveGreen);

                //_spriteBatch.Draw(PseudoRandom.Generated, new Rectangle(0, 0, DisplayWidth, DisplayHeight), Color.White);
                _spriteBatch.Draw(Textures["Scannline"], radarLikeRect, Color.White);

                // Clear the areas, where the airports will be
                Airports.ForEach(x => x.DrawFilled(_spriteBatch, gameTime, ClearColor));

                // Draw the airports
                Airports.ForEach(x => x.Draw(_spriteBatch, gameTime, Color.White));

                // Draw the string of the airports
                for (var i = 0; i < Airports.Count; i++)
                {
                    Airports[i].DrawString(_spriteBatch);
                }

                // Draw the airplanes, which are intersecting any airports
                for (var i = 0; i < Airplanes.Count; i++)
                {
                    if (Airplanes[i].FadeOut || Airplanes[i].Landed)
                    {
                        Airplanes[i].Draw(_spriteBatch);
                        continue;
                    }

                    var corners = Airplanes[i].Corners;
                    for (var j = 0; j < corners.Length; j++)
                    {
                        var shouldContinue = false;
                        for (var k = 0; k < Airports.Count; k++)
                        {
                            if (Airports[k]._boundings.ContainsPoint(corners[j].ToPoint()))
                            {
                                Airplanes[i].FadeOut = false;
                                Airplanes[i]._alpha = 1f;
                                Airplanes[i].Draw(_spriteBatch);
                                shouldContinue = true;
                                Airplanes[i].Seen();
                                break;
                            }
                        }

                        if (shouldContinue)
                        {
                            continue;
                        }

                        if (radarLikeRect.Contains(corners[j]))
                        {
                            Airplanes[i].FadeOut = true;
                            Airplanes[i].Draw(_spriteBatch);
                            Airplanes[i].Seen();
                            break;
                        }
                    }
                }
                //Airplanes.Where(x => x.Landed || Airports.Any(ap => x.Corners.Any(corner => radarLikeRect.Contains(corner) ||ap._boundings.ContainsPoint(corner.ToPoint()))))
                //    .AsParallel()
                //    .ForAll(x => x.Draw(_spriteBatch));

                var seenAirplanes = new List<string>();
                var landedAirplanes = new List<string>();
                for (var i = 0; i < Airplanes.Count; i++)
                {
                    if (Airplanes[i].Landed)
                    {
                        landedAirplanes.Add(Airplanes[i].ToString());
                    }

                    if (Airplanes[i].BeenSeen)
                    {
                        seenAirplanes.Add(Airplanes[i].ToString());
                    }
                }

                var height = Fonts["Airplane"].MeasureString("W").Y;
                _spriteBatch.DrawString(Fonts["Airplane"], "Seen:", new Vector2(0, height), Color.White);
                for (var i = 0; i < seenAirplanes.Count; i++)
                {
                    _spriteBatch.DrawString(Fonts["Airplane"], seenAirplanes[i], new Vector2(0, i*height + 3*height),
                        Color.White);
                }

                _spriteBatch.DrawString(Fonts["Airplane"], "Landed:", new Vector2(DisplayWidth - 100, height),
                    Color.White);
                for (var i = 0; i < landedAirplanes.Count; i++)
                {
                    _spriteBatch.DrawString(Fonts["Airplane"], landedAirplanes[i],
                        new Vector2(DisplayWidth - Fonts["Airplane"].MeasureString(landedAirplanes[i]).X - 3,
                            i*height + 3*height), Color.White);
                }

#if (DEBUG)
                Airports.ForEach(x=>x.DebugDraw(_spriteBatch));
#endif
            }
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(DEFAULT_TARGET);
        }
    }
}