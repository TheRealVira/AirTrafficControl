﻿#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: IFactory.cs
// Date - created:2016.08.29 - 20:06
// Date - current: 2016.08.30 - 18:59

#endregion

#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace AirTrafficControl
{
    internal interface IFactory<out T>
    {
        IEnumerable<T> Factorize(Random rand, int count);
    }
}