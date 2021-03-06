﻿// Copyright (c) the authors of nanoGames. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the project root.

using System;

namespace NanoGames.Games.Bomberguy
{
    internal class BomberMatch : Match<BomberGuy>
    {
        public const int FIELD_MIN_SIZE = 9;
        public const double PLAYER_RATIO = .9;
        public const double BOMB_RATIO = 1;
        public const int BOMB_REACH = 2;
        public const double PLAYER_SPEED = 9;
        public const double BOMBSTACLE_PROBABILITY = 0.5;

        private int _fieldSize;
        private double _playerSpeed;
        private double _pixelsPerUnit;
        private double _widthOffset;
        private BomberThing[,] _field;
        private int _deadPlayers;

        public int DeadPlayers
        {
            get
            {
                return _deadPlayers;
            }

            set
            {
                _deadPlayers = value;
                CheckCompleted();
            }
        }

        public Vector CellSize
        {
            get { return new Vector(_pixelsPerUnit, _pixelsPerUnit); }
        }

        public BomberThing this[Vector v]
        {
            get
            {
                return this[(int)v.X, (int)v.Y];
            }

            set
            {
                this[(int)v.X, (int)v.Y] = value;
            }
        }

        public BomberThing this[int x, int y]
        {
            get
            {
                return _field[x, y];
            }

            set
            {
                _field[x, y] = value;
                if (value != null) value.Position = GetCoordinates(new Vector(x, y));
            }
        }

        public Vector GetCell(BomberThing thing)
        {
            var c = (thing.Center.X - _widthOffset) / _pixelsPerUnit;
            var r = thing.Center.Y / _pixelsPerUnit;
            return new Vector(Math.Floor(r), Math.Floor(c));
        }

        public Vector GetCell(Vector position)
        {
            var c = (position.X - _widthOffset) / _pixelsPerUnit;
            var r = position.Y / _pixelsPerUnit;
            return new Vector(Math.Floor(r), Math.Floor(c));
        }

        public Vector GetCoordinates(Vector cellCoordinates)
        {
            return new Vector(_widthOffset + cellCoordinates.Y * _pixelsPerUnit, cellCoordinates.X * _pixelsPerUnit);
        }

        public void CheckAllDeaths()
        {
            foreach (var p in Players)
            {
                CheckDeath(p);
            }
        }

        protected override void Initialize()
        {
            _fieldSize = FIELD_MIN_SIZE + ((int)(Players.Count / 4)) * 2;

            _field = new BomberThing[_fieldSize, _fieldSize];

            _playerSpeed = PLAYER_SPEED / _fieldSize;

            _pixelsPerUnit = Graphics.Height / _fieldSize;
            _widthOffset = (Graphics.Width - Graphics.Height) / 2d;

            // initialize all obstacles
            InitializeField();

            // initialize players
            InitializePlayers();
        }

        protected override void Update()
        {
            foreach (var p in this.Players)
            {
                DrawField(p);

                DrawPlayers(p);

                /* Skip players that have already finished. */
                if (p.Dead) continue;

                MovePlayer(p);

                DropBomb(p);

                CheckDeath(p);
            }
        }

        private void InitializeField()
        {
            for (int r = 0; r < _fieldSize; r++)
            {
                for (int c = 0; c < _fieldSize; c++)
                {
                    if (r == 0 || r == _fieldSize - 1 || c == 0 || c == _fieldSize - 1 || r % 2 == 0 && c % 2 == 0)
                    {
                        //_field[r, c] = new Unbombable(this, new Vector(_widthOffset + c * _pixelsPerUnit, r * _pixelsPerUnit), new Vector(_pixelsPerUnit, _pixelsPerUnit));
                        this[r, c] = new Unbombable(this, CellSize);
                    }
                    else
                    {
                        if (this.Random.NextDouble() <= BOMBSTACLE_PROBABILITY)
                        {
                            this[r, c] = new Bombstacle(this, CellSize);
                        }
                    }
                }
            }
        }

        private void InitializePlayers()
        {
            int playersPerSide = (int)Math.Ceiling(Players.Count / 4d);
            BomberGuy[,] playerArray = new BomberGuy[4, playersPerSide];
            int side = 0;
            int playerCount = 0;
            foreach (BomberGuy p in Players)
            {
                playerArray[side, playerCount] = p;
                if (side++ > 3)
                {
                    side = 0;
                    playerCount++;
                }
            }

            Vector direction = new Vector(1, 0);
            int distance = (int)Math.Floor((double)(_fieldSize - 2) / playersPerSide);
            Vector currPos = new Vector(1, 1);
            for (int i = 0; i < 4; i++)
            {
                if (i == 1) currPos = new Vector(_fieldSize - 2, 1);
                if (i == 2) currPos = new Vector(_fieldSize - 2, _fieldSize - 2);
                if (i == 3) currPos = new Vector(1, _fieldSize - 2);

                for (int j = 0; j < playersPerSide; j++)
                {
                    var p = playerArray[i, j];
                    if (p == null) continue;
                    p.Size = new Vector(_pixelsPerUnit * PLAYER_RATIO, _pixelsPerUnit * PLAYER_RATIO);

                    var cellCoordinates = new Vector(currPos.Y, currPos.X);

                    MakeSpaceAroundPlayer(cellCoordinates);

                    p.Position = GetCoordinates(cellCoordinates);
                    currPos += direction * distance;
                }

                direction = direction.RotatedRight;
            }
        }

        private void MakeSpaceAroundPlayer(Vector cellCoordinates)
        {
            var cellContent = this[cellCoordinates];
            if (cellContent != null && cellContent.Destroyable) cellContent.Destroy();

            var directionVector = new Vector(1, 0);

            for (int i = 0; i < 4; i++)
            {
                cellContent = this[cellCoordinates + directionVector];
                if (cellContent != null && cellContent.Destroyable) cellContent.Destroy();
                directionVector = directionVector.RotatedRight;
            }
        }

        private void DrawField(BomberGuy p)
        {
            for (int r = 0; r < _fieldSize; r++)
            {
                for (int c = 0; c < _fieldSize; c++)
                {
                    BomberThing thing = _field[r, c];

                    if (thing != null)
                        thing.Draw(p.Graphics);
                }
            }
        }

        private void MovePlayer(BomberGuy p)
        {
            double x = 0, y = 0;

            if (p.Input.Up && !p.Input.Down)
            {
                var neighborLeft = this[GetCell(p.Position + new Vector(0, -_playerSpeed))];
                if (neighborLeft != null)
                {
                    var xDistance = p.Center.X - (neighborLeft.Position + neighborLeft.Size).X;
                    var yDistance = p.Center.Y - (neighborLeft.Position + neighborLeft.Size).Y;

                    if (yDistance < 0 || (yDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborLeft = null;
                    }
                }

                var neighborRight = this[GetCell(p.Position + new Vector(p.Size.X, -_playerSpeed))];
                if (neighborRight != null)
                {
                    var xDistance = (neighborRight.Position).X - p.Center.X;
                    var yDistance = p.Center.Y - (neighborRight.Position + neighborRight.Size).Y;

                    if (yDistance < 0 || (yDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborRight = null;
                    }
                }
                if ((neighborLeft == null || neighborLeft.Passable) && (neighborRight == null || neighborRight.Passable))
                {
                    y = -1;
                }
            }
            if (p.Input.Down && !p.Input.Up)
            {
                var neighborLeft = this[GetCell(p.Position + p.Size + new Vector(-p.Size.X, _playerSpeed))];
                if (neighborLeft != null)
                {
                    var xDistance = p.Center.X - (neighborLeft.Position + neighborLeft.Size).X;
                    var yDistance = (neighborLeft.Position).Y - p.Center.Y;

                    if (yDistance < 0 || (yDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborLeft = null;
                    }
                }

                var neighborRight = this[GetCell(p.Position + p.Size + new Vector(0, _playerSpeed))];
                if (neighborRight != null)
                {
                    var xDistance = (neighborRight.Position).X - p.Center.X;
                    var yDistance = (neighborRight.Position).Y - p.Center.Y;

                    if (yDistance < 0 || (yDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborRight = null;
                    }
                }
                if ((neighborLeft == null || neighborLeft.Passable) && (neighborRight == null || neighborRight.Passable))
                {
                    y = 1;
                }
            }
            if (p.Input.Left && !p.Input.Right)
            {
                var neighborAbove = this[GetCell(p.Position + new Vector(-_playerSpeed, 0))];

                if (neighborAbove != null)
                {
                    var xDistance = p.Center.X - (neighborAbove.Position + neighborAbove.Size).X;
                    var yDistance = p.Center.Y - (neighborAbove.Position + neighborAbove.Size).Y;

                    if (xDistance < 0 || (xDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborAbove = null;
                    }
                }

                var neighborBelow = this[GetCell(p.Position + new Vector(-_playerSpeed, p.Size.Y))];

                if (neighborBelow != null)
                {
                    var xDistance = p.Center.X - (neighborBelow.Position + neighborBelow.Size).X;
                    var yDistance = (neighborBelow.Position).Y - p.Center.Y;

                    if (xDistance < 0 || (xDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborBelow = null;
                    }
                }

                if ((neighborAbove == null || neighborAbove.Passable) && (neighborBelow == null || neighborBelow.Passable))
                {
                    x = -1;
                }
            }
            if (p.Input.Right && !p.Input.Left)
            {
                var neighborAbove = this[GetCell(p.Position + new Vector(p.Size.X + _playerSpeed, 0))];

                if (neighborAbove != null)
                {
                    var xDistance = (neighborAbove.Position).X - p.Center.X;
                    var yDistance = p.Center.Y - (neighborAbove.Position + neighborAbove.Size).Y;

                    if (xDistance < 0 || (xDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborAbove = null;
                    }
                }

                var neighborBelow = this[GetCell(p.Position + new Vector(p.Size.X + _playerSpeed, p.Size.Y))];

                if (neighborBelow != null)
                {
                    var xDistance = (neighborBelow.Position).X - p.Center.X;
                    var yDistance = (neighborBelow.Position).Y - p.Center.Y;

                    if (xDistance < 0 || (xDistance > 0 && (yDistance / p.Size.Y + xDistance / p.Size.X) > 0.55))
                    {
                        neighborBelow = null;
                    }
                }
                if ((neighborAbove == null || neighborAbove.Passable) && (neighborBelow == null || neighborBelow.Passable))
                {
                    x = 1;
                }
            }

            if (x == 0 && y == 0) return;

            var direction = new Vector(x, y).Normalized;

            p.Position += direction * _playerSpeed;
        }

        private void DropBomb(BomberGuy p)
        {
            if (!p.Input.Fire) return;

            var cell = GetCell(p);

            var bomb = new Bomb(BOMB_REACH, this, GetCoordinates(cell), new Vector(_pixelsPerUnit * BOMB_RATIO, _pixelsPerUnit * BOMB_RATIO));

            this[cell] = bomb;
        }

        private void DrawPlayers(BomberGuy p)
        {
            /* Draw each player. */
            foreach (var player in Players)
            {
                /* Skip players that have already finished. */
                if (player.Dead) continue;

                if (player == p)
                {
                    /* Always show the current player in white. */
                    Color playerColor = player.Color;
                    player.Color = new Color(1, 1, 1);
                    player.Draw(p.Graphics);
                    player.Color = playerColor;
                }
                else
                {
                    player.Draw(p.Graphics);
                }
            }
        }

        private void CheckDeath(BomberGuy p)
        {
            var cell = this[GetCell(p)];

            if (cell != null && cell.Deadly) p.Destroy();
        }

        private void CheckCompleted()
        {
            this.IsCompleted = DeadPlayers >= Players.Count - 1;

            if (IsCompleted)
            {
                foreach (var p in Players)
                {
                    if (!p.Dead) p.Score = DeadPlayers + 1;
                }
            }
        }
    }
}
