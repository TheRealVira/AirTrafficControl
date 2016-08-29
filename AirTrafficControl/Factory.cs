using System;
using System.Collections.Generic;

namespace AirTrafficControl
{
    interface IFactory<out T>
    {
        IEnumerable<T> Factorize(Random rand, int count);
    }
}
