﻿// Copyright (c) the authors of nanoGames. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root.

namespace NanoGames.Games.Bomberguy
{
    internal class Explosion : AbstractBomberThing
    {
        private Type type;

        public Explosion(Type type, BomberMatch match, Vector size) : this(type, match, new Vector(), size)
        {
        }

        public Explosion(Type type, BomberMatch match, Vector position, Vector size) : base(match, true, true, true, position, size)
        {
            this.type = type;

            match.TimeOnce(1500, () => Destroy());
        }

        public enum Type
        {
            CENTER,
            ROW,
            COLUMN,
            TOPEND,
            RIGHTEND,
            BOTTOMEND,
            LEFTEND
        }

        public override void Draw(Graphics g)
        {
            g.Circle(new Color(1, 0, 0), Center, Size.X / 10);
        }
    }
}
