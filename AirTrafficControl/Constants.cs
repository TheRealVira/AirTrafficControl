#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: Constants.cs
// Date - created:2016.08.15 - 14:34
// Date - current: 2016.08.30 - 16:56

#endregion

#region Usings

using AirTrafficControl.Airplane;
using AirTrafficControl.Airport;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace AirTrafficControl
{
    public static class Constants
    {
        public const string TEXTURE_DIR = "Textures";
        public const string SHADER_DIR = "Shader";
        public const string FONTS_DIR = "Fonts";

        public const float MAX_RAD = 500f;
        public const float MIN_RAD = 250f;
        public const int AIRPLANE_COUNT = 10;
        public const int AIRPORT_COUNT = 3;
        public const float DEFAULT_SPEED = 50;
        public const float SPEED_LOSS = .999f;
        public const float MIN_SPEED = 20;
        public const RenderTarget2D DEFAULT_TARGET = null; // A bit cheaty, but who the hell cares :P
        public const float DISSOLVING_SPEED_OF_THE_AIRPLANE_WHEN_IN_GOAL = .001f;
        public const float DISSOLVING_SPEED_OF_THE_AIRPLANE_WHEN_RADAR = .002f;

        // Constants for the Retro-Shader
        public const int MAX_TIME_RUMBELING = 1000;
        public const int MIN_TIME_RUMBELING = 500;
        public const int MAX_TIME_NOT_RUMBELING = 5000;
        public const int MIN_TIME_NOT_RUMBELING = 1000;

        // Will get initlized in Game1.cs
        public static int DisplayWidth;
        public static int DisplayHeight;
        public static Color ClearColor;
        //public static readonly Vector2 DefaultMovementSpeed = new Vector2(50, 50);

        // Factories:
        public static readonly AirplaneFactory TheAirplaneFactory = new AirplaneFactory();
        public static readonly AirportFactory TheAirportFactory = new AirportFactory();
    }
}