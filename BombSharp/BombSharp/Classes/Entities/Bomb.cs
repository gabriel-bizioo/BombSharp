using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp.Classes
{
    public class Bomb : Entity
    {
        public Bomb() : base(null)
        {
            BombSS = Properties.sprites.items;
            this.HitBox = BombHitBox.FromBomb(this);
            this.Width = Block.Width;
            this.Height = Block.Height;
            this.CoordX = 1000;
        }

        private Image BombSS = null;

        public int CoordX, CoordY;
        public int Width, Height;
        public bool Deployed { get; set; }
        public DateTime DeployTime { get; set; }

        public override void Draw(Graphics g)
        {
            if(Deployed)    
                g.DrawImage(BombSS, new Rectangle(CoordX, CoordY, this.Width, this.Height), new Rectangle(0, 0, 14, 14), GraphicsUnit.Pixel);
            HitBox.Draw(g);
        }

        public void Deploy(Player player)
        {

            if (!Deployed)
            {
                this.Width = Block.Width - 20;
                this.Height = Block.Height - 20;

                switch (player.PlayerDirection)
                {
                    case FacingDirections.Left:
                        this.CoordX = player.CoordX - this.Width;
                        this.CoordY = player.CoordY;
                        break;
                    case FacingDirections.Right:
                        this.CoordX = player.CoordX + this.Width;
                        this.CoordY = player.CoordY;
                        break;
                    case FacingDirections.Up:
                        this.CoordX = player.CoordX;
                        this.CoordY = player.CoordY - this.Width;
                        break;
                    case FacingDirections.Down:
                        this.CoordX = player.CoordX;
                        this.CoordY = player.CoordY + player.Height;
                        break;
                }
                Deployed = true;
                DeployTime = DateTime.Now;
            } 
        }

        public bool Explode(DateTime now)
        {
            long dt = this.DeployTime.AddSeconds(4).Ticks - now.Ticks;

            if (dt < 10000000 && dt > 0)
            {
                this.CoordX -= 3;
                this.CoordY -= 3;
                this.Width += 6;
                this.Height += 6;
                Deployed = false;
                return true;
            } 
            return false;
        }
    }
}
