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
        }

        private Image BombSS = null;

        public int CoordX, CoordY;
        public int Width, Height;
        private bool deployed;
        public DateTime DeployTime { get; set; }

        public override void Draw(Graphics g)
        {
            if(deployed)
                g.DrawImage(BombSS, new Rectangle(CoordX, CoordY, this.Width, this.Height), new Rectangle(0, 0, 14, 14), GraphicsUnit.Pixel);
            HitBox.Draw(g);
        }

        public void Deploy(Player player)
        {
            this.Width = Block.Width;
            this.Height = Block.Height;

            if (!deployed)
            {
                this.CoordX = player.CoordX + Width;
                this.CoordY = player.CoordY;
                deployed = true;
                DeployTime = DateTime.Now;
            } 
        }

        public void Explode()
        {
            deployed = false;
            this.Width += 5;
            this.Height += 5;
        }
    }
}
