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
        public Player(bool player2) : base(null)
        {
            this.Player2 = player2;
            if (Player2)
            {
                this.CoordX = Block.Width * 9;
                this.CoordY = Block.Height * 9;
            }
            else
            {  
                this.CoordX = Block.Width;
                this.CoordY = Block.Height;
            }
            this.Width = Block.Width - 20;
            this.Height = Block.Height - 12;
            this.HitBox = PlayerHitBox.FromPlayer(this);
            playerSS = Properties.sprites.player;
            this.Health = 1;
        }
        
        public int Speed = 6;
        public int Health;

        public int CoordX, CoordY, Width, Height;
        public int SpriteY, SpriteX;

        private Image playerSS = null;
        private bool Player2;

        public FacingDirections PlayerDirection { get; set; } = FacingDirections.Down | FacingDirections.Stop;

        public void KeyMovement(Keys key)
        {
            if (!Player2)
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
            else
            {
                switch (key)
                {
                    case Keys.Up:
                        this.PlayerDirection = FacingDirections.Up | FacingDirections.Moving;
                        break;
                    case Keys.Left:
                        this.PlayerDirection = FacingDirections.Left | FacingDirections.Moving;
                        break;
                    case Keys.Down:
                        this.PlayerDirection = FacingDirections.Down | FacingDirections.Moving;
                        break;
                    case Keys.Right:
                        this.PlayerDirection = FacingDirections.Right | FacingDirections.Moving;
                        break;
                }
            }
            
        }

        public void Stop()
        {
            this.PlayerDirection = (FacingDirections)((int)this.PlayerDirection & 30); // Magic :)
            this.SpriteX = 0;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(playerSS, new Rectangle(this.CoordX, this.CoordY, this.Width, this.Height), new Rectangle(SpriteX, SpriteY, 17, 26), GraphicsUnit.Pixel);
            //HitBox.Draw(g);
        }

        public void WalkAnimation()
        {
            if(this.Health > 0)
            {
                if (this.PlayerDirection == (FacingDirections.Down | FacingDirections.Moving))
                {
                    this.CoordY += this.Speed;
                    this.SpriteY = 0;
                    if (this.SpriteX < 105)
                        this.SpriteX += 21;
                    else
                        this.SpriteX = 0;
                }
                if (this.PlayerDirection == (FacingDirections.Right | FacingDirections.Moving))
                {
                    this.CoordX += this.Speed;
                    this.SpriteY = 27;
                    if (this.SpriteX < 105)
                        this.SpriteX += 21;
                    else
                        this.SpriteX = 0;
                }
                if (this.PlayerDirection == (FacingDirections.Up | FacingDirections.Moving))
                {
                    this.CoordY -= this.Speed;
                    this.SpriteY = 54;
                    if (this.SpriteX < 105)
                        this.SpriteX += 21;
                    else
                        this.SpriteX = 0;
                }
                if (this.PlayerDirection == (FacingDirections.Left | FacingDirections.Moving))
                {
                    this.CoordX -= this.Speed;
                    this.SpriteY = 281;
                    if (this.SpriteX < 105)
                        this.SpriteX += 21;
                    else
                        this.SpriteX = 0;
                }
            }
            
        }

        public void DeathAnimation()
        {
            this.SpriteY = 255;
            if (this.SpriteX >= 111)
                this.SpriteX = 0;
            this.SpriteX += 26;

        }

        public void Die()
        {
            if (this.Health < 0)
            {
                this.Speed = 0;
                DeathAnimation();
            }
        }

        public override void OnCollision(CollisionInfo info)
        {
            if (info.Entity is Bomb)
            {
                this.Health -= 1;
            }
            else
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


                    if (ddy == 0 && ldx == 160 - Speed && rdx == 90 - Speed)
                    {
                        this.CoordX = (int)info.SideB.X - this.Width;
                    }
                    if (udy == 0 && ldx == 84 && rdx == 154)
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

            
            
        }
    }

}