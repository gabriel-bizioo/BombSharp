using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp.Classes
{
    public class Player
    {
        public int Speed;
        public Image spritesheet = Properties.sprites.player;
        public Image[,] sprite_walk = new Image[3, 7];
        public Image[,] sprite_lift = new Image[3, 12];
        public Image[] sprite_stun = new Image[5];
        public Image[] sprite_win = new Image[4];
        public Image[] sprite_defeat = new Image[5];
        public Image[] sprite_idle = new Image[7];
    }
}
