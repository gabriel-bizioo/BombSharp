using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombSharp.Classes
{
    public class Player : Entity
    {
        public Player(PictureBox player_size) : base(null)
        {
            this.PlayerPictureBox = player_size;
            this.HitBox = HitBox.FromPlayer(this);
        }

        public int speed = 4;

        public Image spritesheet = Properties.sprites.player;
        public Image[,] sprite_sliced = new Image[10, 7];

        public PictureBox PlayerPictureBox;

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

        public override void Draw(Graphics g) { }

        public override void OnCollision(CollisionInfo info)
        {
            if (info.SideA.Y == info.SideB.Y)
            {
                if (info.SideA.Y > this.PlayerPictureBox.Location.Y)
                {
                    this.PlayerPictureBox.Location = new Point(
                        this.PlayerPictureBox.Location.X,
                        (int)info.SideA.Y - this.PlayerPictureBox.Height);
                }

                //if (info.SideA.Y < this.PlayerPictureBox.Location.Y + this.PlayerPictureBox.Height)
                //{
                //    this.PlayerPictureBox.Location = new Point(
                //        this.PlayerPictureBox.Location.X,
                //        (int)info.SideA.Y);                     
                //}
            }

            if (info.SideA.X == info.SideB.X)
            {
                if (info.SideA.X > this.PlayerPictureBox.Location.X + this.PlayerPictureBox.Width)
                {
                    this.PlayerPictureBox.Location = new Point(
                        (int)info.SideA.X,
                        this.PlayerPictureBox.Location.Y);
                }
                //if (info.SideA.X < this.PlayerPictureBox.Location.X + this.PlayerPictureBox.Width)
                //{
                //    this.PlayerPictureBox.Location = new Point(
                //        (int)info.SideA.X - this.PlayerPictureBox.Width,
                //        this.PlayerPictureBox.Location.Y);
                //}
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