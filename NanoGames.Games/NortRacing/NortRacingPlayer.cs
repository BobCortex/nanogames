using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoGames.Games.NortRacing
{
    class NortRacingPlayer:Player<NortRacingMatch>
    {
        public double x = 100;
        public double y = 100;

        public enum direction {left, right, up, down};

        public direction moveDirection = direction.up;





       
    }
}
