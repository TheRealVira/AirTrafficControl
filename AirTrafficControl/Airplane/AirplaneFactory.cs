#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: AirplaneFactory.cs
// Date - created:2016.08.29 - 20:05
// Date - current: 2016.08.30 - 16:56

#endregion

#region Usings

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

#endregion

namespace AirTrafficControl.Airplane
{
    public class AirplaneFactory : IFactory<Airplane>
    {
        private static readonly string[] CoolNames =
        {
            "Adamoli-Cattani fighter",
            "AD Scout",
            "Aerfer Ariete",
            "Aero Ae 02",
            "Albatros D.XI",
            "Albatros Dr.I",
            "Albatros L 84",
            "Alcock Scout",
            "Ansaldo A.1 Balilla",
            "Arado Ar 440",
            "Arsenal VB 10",
            "Arsenal VG 90",
            "Avia BH-23"
        };

        public IEnumerable<Airplane> Factorize(Random rand, int count)
        {
            if (rand == null || Game1.Airports == null || Game1.Airports.Count == 0)
            {
                yield break; // Fast breakout, before something else breaks...
            }

            for (var i = 0; i < count; i++)
            {
                var position = new Vector2(rand.Next(0, Constants.DisplayWidth), rand.Next(0, Constants.DisplayHeight));
                var randomAirport = Game1.Airports.RandomItem(rand);

                yield return new Airplane(CoolNames[rand.Next(0, CoolNames.Length)], position, randomAirport);
            }
        }
    }
}