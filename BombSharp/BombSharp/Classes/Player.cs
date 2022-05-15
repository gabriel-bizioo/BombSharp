using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombSharp.Classes
{
    public class Player
    {
        public int Speed;
        public Image spritesheet = Properties.sprites.player;
        public Image[,] sprite_sliced = new Image[10, 7];
        public enum facingDirections
        {
            Down,
            Right,
            Up,
            Left           
            
        }

        public facingDirections playerDirection = facingDirections.Down;

        public void keyMovement(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    this.playerDirection = facingDirections.Up;
                    break;
                case Keys.A:
                    this.playerDirection = facingDirections.Left;
                    break;
                case Keys.S:
                    this.playerDirection = facingDirections.Down;
                    break;
                case Keys.D:
                    this.playerDirection = facingDirections.Right;
                    break;
            }
        }

        //public void SliceImage(int x, int y)
        //{
        //    for (int i = 0; i < x; i++)
        //    {
        //        for (int j = 0; j < y; j++)
        //        {
        //            sprite_sliced[i, j] = (spritesheet as Bitmap).Clone(new Rectangle(i * resolucaox, j * resolucaoy, resolucaox, resolucaoy), spritesheet.PixelFormat);
        //        }
        //    }
        //}
    }
}
