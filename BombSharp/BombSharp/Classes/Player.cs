using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombSharp.Classes
{
    public enum FacingDirections
    {
        Stop = 0,
        Moving = 1,
        Down = 2,
        Right = 4,
        Up = 8,
        Left = 16,
    }

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

        public FacingDirections PlayerDirection { get; set; } = FacingDirections.Down | FacingDirections.Stop;

        public void KeyMovement(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    this.PlayerDirection = FacingDirections.Up | FacingDirections.Moving;
                    break;
                case Keys.A:
                    this.PlayerDirection = FacingDirections.Left | FacingDirections.Moving;
                    break;
                case Keys.S:
                    this.PlayerDirection = FacingDirections.Down | FacingDirections.Moving;
                    break;
                case Keys.D:
                    this.PlayerDirection = FacingDirections.Right | FacingDirections.Moving;
                    break;
            }
        }

        public void Stop()
        {
            this.PlayerDirection = (FacingDirections)((int)this.PlayerDirection & 30); // Magic :)
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

                if (info.SideA.Y < this.PlayerPictureBox.Location.Y + this.PlayerPictureBox.Height)
                {
                    this.PlayerPictureBox.Location = new Point(
                        this.PlayerPictureBox.Location.X,
                        (int)info.SideA.Y + 1);
                }
            }

            if (info.SideA.X == info.SideB.X)
            {
                if (info.SideA.X > this.PlayerPictureBox.Location.X)
                {
                    this.PlayerPictureBox.Location = new Point(
                        (int)info.SideA.X - PlayerPictureBox.Width,
                        this.PlayerPictureBox.Location.Y);
                }
                if (info.SideA.X < this.PlayerPictureBox.Location.X - this.PlayerPictureBox.Width)
                {
                    this.PlayerPictureBox.Location = new Point(
                        (int)info.SideA.X + 1,
                        this.PlayerPictureBox.Location.Y);
                }
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