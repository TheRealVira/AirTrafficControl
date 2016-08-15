#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Game1.cs
// Date - created:2016.08.15 - 14:25
// Date - current: 2016.08.15 - 18:28

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficControl.Airport;
using AirTrafficControl.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, Effect> Shader;
        private readonly GraphicsDeviceManager _graphics;
        private List<Airplane.Airplane> _airplanes;
        private List<Airport.Airport> _airports;
        private SpriteBatch _spriteBatch;

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

            Constants.DISPLAY_WIDTH = _graphics.PreferredBackBufferWidth;
            Constants.DISPLAY_HEIGHT = _graphics.PreferredBackBufferHeight;

            NewMouseState = new MouseState();
            NewKeyboardState = new KeyboardState(Keys.A);
            OldMouseState = NewMouseState;
            OldKeyboardState = NewKeyboardState;

            CoolPixle2016 = new Texture2D(GraphicsDevice, 1, 1);
            CoolPixle2016.SetData(new[] {Color.White});
            Constants.ClearColor = Color.Black;

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

            Textures = ContentLib.LoadStuff<Texture2D>(Content, "Textures");
            Shader = ContentLib.LoadStuff<Effect>(Content, "Shader");

            _airports = AirportFactory.Factory(new Random(DateTime.Now.Millisecond), Constants.AIRPLANE_COUNT).ToList();
            _airplanes = new List<Airplane.Airplane>
            {
                new Airplane.Airplane("Tester101", _airports[0]._boundings.Center.ToVector2(), new Vector2(50, 50))
            };
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
                _airports.Clear();
                _airports =
                    AirportFactory.Factory(new Random(DateTime.Now.Millisecond), Constants.AIRPLANE_COUNT).ToList();
            }

            _airplanes.ForEach(x => x.Update(gameTime));

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
            GraphicsDevice.Clear(Color.Black);

            // Drawing the grid by my grid shader
            Grid.Draw(_spriteBatch);

            _spriteBatch.Begin();
            {
                // Clear the areas, where the airports will be
                _airports.ForEach(x => x.DrawFilled(_spriteBatch, gameTime, Constants.ClearColor));

                // Draw the airports
                _airports.ForEach(x => x.Draw(_spriteBatch, gameTime, Color.White));

                // Draw the airplanes, which are intersecting any airports
                _airplanes.Where(x => _airports.Any(ap => ap._boundings.Intersects(x.Boundings)))
                    .AsParallel()
                    .ForAll(x => x.Draw(_spriteBatch));
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}