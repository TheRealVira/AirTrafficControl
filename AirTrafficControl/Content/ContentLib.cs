#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: AirTrafficControl
// Project: AirTrafficControl
// Filename: ContentLib.cs
// Date - created:2016.08.15 - 15:08
// Date - current: 2016.08.16 - 13:12

#endregion

#region Usings

using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;

#endregion

namespace AirTrafficControl.Content
{
    internal static class ContentLib
    {
        public static Dictionary<string, T> LoadStuff<T>(ContentManager manager, string directory)
        {
            var dir = new DirectoryInfo(manager.RootDirectory + "/" + directory);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            var result = new Dictionary<string, T>();

            foreach (var file in dir.GetFiles("*.*"))
            {
                var key = Path.GetFileNameWithoutExtension(file.Name);

                result[key] = manager.Load<T>(directory + "/" + key);
            }

            return result;
        }
    }
}