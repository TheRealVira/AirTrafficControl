using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AirTrafficControl.Airplane
{
    public class AirplaneFactory:IFactory<Airplane>
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
            if (rand == null||Game1.Airports==null||Game1.Airports.Count==0)
            {
                yield break; // Fast breakout, before something else breaks...
            }

            for (int i = 0; i < count; i++)
            {
                var position = new Vector2(rand.Next(0, Constants.DisplayWidth), rand.Next(0, Constants.DisplayHeight));
                var randomAirport = Game1.Airports.RandomItem(rand);

                var targetVector = randomAirport._boundings.ToVector2() - position; // Getting raw vector
                targetVector.Normalize(); // Normalizing the raw vector (notice here, that we'll get some loss of precision)
                targetVector *= Constants.DEFAULT_SPEED; // Multiplying the normalized vector by my speedy constant.

                yield return new Airplane(CoolNames[rand.Next(0,CoolNames.Length)],position, targetVector, randomAirport);
            }
        }
    }
}
