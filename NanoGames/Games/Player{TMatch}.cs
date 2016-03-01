﻿// Copyright (c) the authors of NanoGames. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root.

namespace NanoGames.Games
{
    /// <summary>
    /// Represents a player in a match.
    /// </summary>
    /// <typeparam name="TMatch">The match type this player type belongs to.</typeparam>
    internal abstract class Player<TMatch> : Player
        where TMatch : Match
    {
        /// <summary>
        /// Gets or sets the match this player belongs to.
        /// </summary>
        public TMatch Match { get; set; }

        /// <summary>
        /// Initializes the player. This is called after <see cref="Match.Initialize"/>.
        /// </summary>
        public abstract void Initialize();

        /// <inheritdoc/>
        public abstract override void Update();
    }
}
