﻿// Copyright (c) the authors of NanoGames. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root.

using NanoGames.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NanoGames.Application
{
    /// <summary>
    /// A view showing a moving starfield.
    /// </summary>
    internal sealed class StarfieldView : IView
    {
        private const double _velocity = 0.225;

        private readonly Random _random = new Random();
        private readonly List<Star> _stars = new List<Star>();
        private readonly List<int> _freeIndexes = new List<int>();

        private long _lastTimestamp = 0;

        /// <inheritdoc/>
        public void Update(Terminal terminal)
        {
            var currentTimestamp = Stopwatch.GetTimestamp();
            var deltaTimestamp = currentTimestamp - _lastTimestamp;
            _lastTimestamp = currentTimestamp;
            if (currentTimestamp == deltaTimestamp)
            {
                return;
            }

            var deltaTime = deltaTimestamp / (double)Stopwatch.Frequency;

            for (int i = 0; i < _stars.Count; ++i)
            {
                var star = _stars[i];

                if (!star.IsUsed)
                {
                    continue;
                }

                var oldZ = star.Z;
                var z = oldZ - _velocity * deltaTime;

                if (z < 0)
                {
                    _stars[i].IsUsed = false;
                    _freeIndexes.Add(i);
                    z = 0.001;
                }

                star.Z = z;
                var x = star.X;
                var y = star.Y;

                var c = 0.33 * (1 - oldZ);

                terminal.Graphics.Line(
                    new Color(c, c, c),
                    GetScreenVector(x, y, oldZ + _velocity / 60),
                    GetScreenVector(x, y, z));
            }

            while (_random.NextDouble() > 1.0 / 16.0)
            {
                int f = _freeIndexes.Count - 1;
                if (f >= 0)
                {
                    CreateStar(_stars[_freeIndexes[f]]);
                    _freeIndexes.RemoveAt(f);
                }
                else
                {
                    var star = new Star();
                    CreateStar(star);
                    _stars.Add(star);
                }
            }
        }

        private void CreateStar(Star star)
        {
            star.IsUsed = true;
            star.X = _random.NextDouble() * 2 - 1;
            star.Y = _random.NextDouble() * 2 - 1;
            star.Z = 1;
        }

        private Vector GetScreenVector(double x, double y, double z)
        {
            return new Vector((x / z) * 0.5 * Graphics.Width + 0.5 * Graphics.Width, (y / z) * 0.5 * Graphics.Width + 0.5 * Graphics.Height);
        }

        private sealed class Star
        {
            public bool IsUsed;
            public double X;
            public double Y;
            public double Z;
        }
    }
}