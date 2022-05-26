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
        public Player(int Width, int Height) : base(null)
        {
            this.Width = Width;
            this.Height = Height;
            this.CoordX = Block.Width;
            this.CoordY = Block.Height;
            this.HitBox = HitBox.FromPlayer(this);
            playerSS = Properties.sprites.player;
        }

        public int speed = 6;
        public int CoordX, CoordY, Width, Height;
        public int SpriteY, SpriteX;
        //public Image spritesheet = Properties.sprites.player;
        //public Image[,] sprite_sliced = new Image[10, 7];
        private Image playerSS = null;

        //public PictureBox PlayerPictureBox;
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

        public override void Draw(Graphics g)
        {
            g.DrawImage(playerSS, new Rectangle(this.CoordX, this.CoordY, this.Width, this.Height), new Rectangle(SpriteX, SpriteY, 17, 26), GraphicsUnit.Pixel);
            HitBox.Draw(g);
        }

        public override void OnCollision(CollisionInfo info)
        {
            float ldx = info.SideA.X - this.CoordX;
            float rdx = info.SideA.X - (this.CoordX + this.Width);
            if (ldx < 0)
                ldx = -ldx;
            if (rdx < 0)
                rdx = -rdx;
            float udy = info.SideA.Y - this.CoordY;
            float ddy = info.SideA.Y - (this.CoordY + this.Height);
            if (udy < 0)
                udy = -udy;
            if (ddy < 0)
                ddy = -ddy;

                if (info.SideA.Y == info.SideB.Y)
                {
                    
                    
                    if(ddy == 0 && ldx == 156 && rdx == 86)
                    {
                        this.CoordX = (int)info.SideB.X - this.Width;                       
                    }
                    if(udy == 0 && ldx == 86 && rdx == 156)
                    {
                        this.CoordX = (int)info.SideB.X;
                    }
                                    //  *              
                    if (udy > ddy) //  ---
                    {
                        this.CoordY = (int)info.SideA.Y - this.Height;
                    }
                                    //  ---                
                    if (ddy > udy) //    * 
                    {
                        this.CoordY = (int)info.SideA.Y;
                    }
                }

                if (info.SideA.X == info.SideB.X)
                {
                    

                    if (ldx >= rdx) // *|
                    {
                        this.CoordX = (int)info.SideA.X - this.Width;
                    }
                    if (rdx >= ldx) // |*
                    {
                        this.CoordX = (int)info.SideA.X;
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