#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: AirportFactory.cs
// Date - created:2016.08.15 - 14:32
// Date - current: 2016.08.30 - 12:58

#endregion

#region Usings

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

#endregion

namespace AirTrafficControl.Airport
{
    public class AirportFactory : IFactory<Airport>
    {
        private static readonly string[] CoolNames =
        {
            "INVAP 2D INKAN",
            "INVAP 3D long range",
            "Jindalee",
            "EDT-FILA",
            "SABER M60",
            "SCP-01 Scipio",
            "SENTIR M20",
            "SABER M200",
            "Erieye radar - AEW",
            "COBRA - Counter Battery Radar",
            "Sindre II - air defence",
            "Ground Master 400",
            "Euroradar CAPTOR"
        };

        public IEnumerable<Airport> Factorize(Random rand, int count)
        {
            if (rand == null)
            {
                yield break; // Fast breakout, before something else breaks...
            }

            for (var i = 0; i < count; i++)
            {
                yield return
                    new Airport(CoolNames.RandomItem(rand),
                        new Vector2(
                            rand.Next((int) Constants.MIN_RAD, (int) (Constants.DisplayWidth - Constants.MAX_RAD)),
                            rand.Next((int) Constants.MIN_RAD, (int) (Constants.DisplayHeight - Constants.MAX_RAD))),
                        rand.Next((int) Constants.MIN_RAD, (int) Constants.MAX_RAD));
            }
        }
    }
}