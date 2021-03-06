﻿// Copyright (c) the authors of nanoGames. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root.

using System.Collections.Generic;

namespace NanoGames
{
    /// <summary>
    /// Contains glyphs for all supported characters.
    /// </summary>
    public static class Font
    {
        private static readonly Dictionary<int, Glyph> _glyphs = new Dictionary<int, Glyph>()
        {
            {
                '!', new Glyph(6, 6)
                {
                    { 3, 1, 3, 4 },
                    { 3, 5 },
                }
            },
            {
                '.', new Glyph(6, 6)
                {
                    { 3, 5 },
                }
            },
            {
                '0', new Glyph(6, 6)
                {
                    { 1, 5, 5, 5, 5, 1, 1, 1, 1, 5 },
                    { 1, 4, 5, 2 },
                }
            },
            {
                '1', new Glyph(6, 6)
                {
                    { 2, 1, 3, 1, 3, 5 },
                    { 1, 5, 5, 5 },
                }
            },
            {
                '2', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1, 5, 3, 1, 3, 1, 5, 5, 5 },
                }
            },
            {
                '3', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1, 5, 5, 1, 5 },
                    { 2, 3, 5, 3 },
                }
            },
            {
                '4', new Glyph(6, 6)
                {
                    { 1, 1, 1, 3, 5, 3 },
                    { 5, 1, 5, 5 },
                }
            },
            {
                '5', new Glyph(6, 6)
                {
                    { 5, 1, 1, 1, 1, 3, 5, 3, 5, 5, 1, 5 },
                }
            },
            {
                '6', new Glyph(6, 6)
                {
                    { 5, 1, 1, 1, 1, 5, 5, 5, 5, 3, 1, 3 },
                }
            },
            {
                '7', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1, 2, 5 },
                }
            },
            {
                '8', new Glyph(6, 6)
                {
                    { 1, 5, 5, 5, 5, 1, 1, 1, 1, 5 },
                    { 1, 3, 5, 3 },
                }
            },
            {
                '9', new Glyph(6, 6)
                {
                    { 1, 5, 5, 5, 5, 1, 1, 1, 1, 3, 5, 3 },
                }
            },
            {
                ':', new Glyph(6, 6)
                {
                    { 3, 5 },
                    { 3, 2 },
                }
            },
            {
                '<', new Glyph(6, 6)
                {
                    { 5, 1, 1, 3, 5, 5 },
                }
            },
            {
                '>', new Glyph(6, 6)
                {
                    { 1, 1, 5, 3, 1, 5 },
                }
            },
            {
                'A', new Glyph(6, 6)
                {
                    { 1, 5, 1, 1, 5, 1, 5, 5 },
                    { 1, 3, 5, 3 },
                }
            },
            {
                'B', new Glyph(6, 6)
                {
                    { 1, 3, 5, 3, 5, 5, 1, 5, 1, 1, 4, 1, 4, 3 },
                }
            },
            {
                'C', new Glyph(6, 6)
                {
                    { 5, 1, 1, 1, 1, 5, 5, 5 },
                }
            },
            {
                'D', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1, 5, 5, 1, 5 },
                    { 2, 1, 2, 5 },
                }
            },
            {
                'E', new Glyph(6, 6)
                {
                    { 5, 1, 1, 1, 1, 5, 5, 5 },
                    { 1, 3, 4, 3 },
                }
            },
            {
                'F', new Glyph(6, 6)
                {
                    { 5, 1, 1, 1, 1, 5 },
                    { 1, 3, 4, 3 },
                }
            },
            {
                'G', new Glyph(6, 6)
                {
                    { 5, 1, 1, 1, 1, 5, 5, 5, 5, 3 },
                }
            },
            {
                'H', new Glyph(6, 6)
                {
                    { 1, 1, 1, 5 },
                    { 5, 1, 5, 5 },
                    { 1, 3, 5, 3 },
                }
            },
            {
                'I', new Glyph(6, 6)
                {
                    { 3, 1, 3, 5 },
                    { 1, 1, 5, 1 },
                    { 1, 5, 5, 5 },
                }
            },
            {
                'J', new Glyph(6, 6)
                {
                    { 5, 1, 5, 5, 1, 5 },
                }
            },
            {
                'K', new Glyph(6, 6)
                {
                    { 1, 1, 1, 5 },
                    { 5, 1, 1, 3, 5, 5 },
                }
            },
            {
                'L', new Glyph(6, 6)
                {
                    { 1, 1, 1, 5, 5, 5 },
                }
            },
            {
                'M', new Glyph(6, 6)
                {
                    { 1, 5, 1, 1, 3, 5, 5, 1, 5, 5 },
                }
            },
            {
                'N', new Glyph(6, 6)
                {
                    { 1, 5, 1, 1, 5, 5, 5, 1 },
                }
            },
            {
                'O', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1, 5, 5, 1, 5, 1, 1 },
                }
            },
            {
                'P', new Glyph(6, 6)
                {
                    { 1, 5, 1, 1, 5, 1, 5, 3, 1, 3 },
                }
            },
            {
                'Q', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1, 5, 5, 1, 5, 1, 1 },
                    { 3, 3, 5, 5 },
                }
            },
            {
                'R', new Glyph(6, 6)
                {
                    { 1, 5, 1, 1, 5, 1, 5, 3, 1, 3 },
                    { 3, 3, 5, 5 },
                }
            },
            {
                'S', new Glyph(6, 6)
                {
                    { 5, 1, 1, 1, 1, 3, 5, 3, 5, 5, 1, 5 },
                }
            },
            {
                'T', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1 },
                    { 3, 1, 3, 5 },
                }
            },
            {
                'U', new Glyph(6, 6)
                {
                    { 1, 1, 1, 5, 5, 5, 5, 1 },
                }
            },
            {
                'V', new Glyph(6, 6)
                {
                    { 1, 1, 3, 5, 5, 1 },
                }
            },
            {
                'W', new Glyph(6, 6)
                {
                    { 1, 1, 2, 5, 3, 1, 4, 5, 5, 1 },
                }
            },
            {
                'X', new Glyph(6, 6)
                {
                    { 1, 1, 5, 5 },
                    { 1, 5, 5, 1 },
                }
            },
            {
                'Y', new Glyph(6, 6)
                {
                    { 1, 1, 3, 3, 5, 1 },
                    { 3, 3, 3, 5 },
                }
            },
            {
                'Z', new Glyph(6, 6)
                {
                    { 1, 1, 5, 1, 1, 5, 5, 5 },
                }
            },
            {
                '✓', new Glyph(6, 6)
                {
                    { 1, 3, 2, 5, 5, 1 },
                }
            },
        };

        /// <summary>
        /// Gets the glyph for the specified character.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>The glyph, or null if this character has no glyph.</returns>
        public static Glyph GetGlyph(char c)
        {
            Glyph glyph;
            _glyphs.TryGetValue(c, out glyph);
            return glyph;
        }
    }
}
