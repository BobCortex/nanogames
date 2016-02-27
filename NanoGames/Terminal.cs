﻿// Copyright (c) the authors of NanoGames. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root.

namespace NanoGames
{
    /// <summary>
    /// Represents an input/output connection to a player.
    /// </summary>
    internal sealed class Terminal : IRenderer
    {
        /// <summary>
        /// The virtual screen width.
        /// </summary>
        public const double Width = 640;

        /// <summary>
        /// The virtual screen height.
        /// </summary>
        public const double Height = 360;

        private readonly IRenderer _renderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Terminal"/> class.
        /// </summary>
        /// <param name="renderer">The renderer to draw onto.</param>
        public Terminal(IRenderer renderer)
        {
            _renderer = renderer;
        }

        /// <inheritdoc/>
        public void Line(Color color, Vector vectorA, Vector vectorB)
        {
            _renderer?.Line(color, vectorA, vectorB);
        }

        /// <summary>
        /// Draws a glyph.
        /// </summary>
        /// <param name="color">The color of the glyph.</param>
        /// <param name="glyph">The glyph to draw.</param>
        /// <param name="lowerLeft">The lower-left corner of the glyph.</param>
        /// <param name="x">The x vector of the glyph to draw.</param>
        /// <param name="y">The y vector of the glyph to draw.</param>
        public void Glyph(Color color, Glyph glyph, Vector lowerLeft, Vector x, Vector y)
        {
            if (glyph == null)
            {
                return;
            }

            foreach (var stroke in glyph)
            {
                Line(color, lowerLeft + stroke.A.X * x + stroke.A.Y * y, lowerLeft + stroke.B.X * x + stroke.B.Y * y);
            }
        }

        /// <summary>
        /// Writes text.
        /// </summary>
        /// <param name="color">The text color.</param>
        /// <param name="size">The font size. Glyphs are square shaped, so this is both the width and height of each character.</param>
        /// <param name="position">The position of the lower-left corner of the text.</param>
        /// <param name="text">The text to write.</param>
        public void Text(Color color, double size, Vector position, string text)
        {
            if (text == null)
            {
                return;
            }

            var x = new Vector(size, 0);
            var y = new Vector(0, size);

            foreach (var c in text)
            {
                var glyph = Font.GetGlyph(c);
                Glyph(color, Font.GetGlyph(c), position, x, y);
                position.X += size;
            }
        }
    }
}