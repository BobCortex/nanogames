using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoGames.Games.NortRacing
{
    class NortRacingMatch : Match<NortRacingPlayer>
    {
        protected override void Initialize()
        {
            
        }

        protected override void Update()
        {
            foreach (var player in Players)
            {
                if (player.Input.Up)
                {
                    if ((player.moveDirection == NortRacingPlayer.direction.left) || (player.moveDirection == NortRacingPlayer.direction.right))
                    {
                        player.moveDirection = NortRacingPlayer.direction.up;
                    }
                }

                if (player.Input.Down)
                {
                    if ((player.moveDirection == NortRacingPlayer.direction.left) || (player.moveDirection == NortRacingPlayer.direction.right))
                    {
                        player.moveDirection = NortRacingPlayer.direction.down;
                    }
                }
                
                if (player.Input.Left)
                {
                    if ((player.moveDirection == NortRacingPlayer.direction.up) || (player.moveDirection == NortRacingPlayer.direction.down))
                    {
                        player.moveDirection = NortRacingPlayer.direction.left;
                    }
                }
                if (player.Input.Right)
                {
                    if ((player.moveDirection == NortRacingPlayer.direction.up) || (player.moveDirection == NortRacingPlayer.direction.down))
                    {
                        player.moveDirection = NortRacingPlayer.direction.right;
                    }
                }

                if (player.moveDirection == NortRacingPlayer.direction.up)
                {
                    player.y -= 1;
                }
                if (player.moveDirection == NortRacingPlayer.direction.down)
                {
                    player.y += 1;
                }
                if (player.moveDirection == NortRacingPlayer.direction.right)
                {
                    player.x += 1;
                }
                if (player.moveDirection == NortRacingPlayer.direction.left)
                {
                    player.x -= 1;
                }
            }


            foreach (var player in Players)
            {
                this.Graphics.Rectangle(player.Color, new Vector(player.x -5, player.y -5), new Vector(player.x + 5, player.y + 5));
            }
        }
    }
}
