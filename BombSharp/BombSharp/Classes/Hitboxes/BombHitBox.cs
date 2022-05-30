using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp.Classes
{
    public class BombHitBox : HitBox
    {
        public BombHitBox(Bomb bomb)
        {
            this.Bomb = bomb;
        }

        private Bomb Bomb;
        public static BombHitBox FromBomb(Bomb bomb)
            => new BombHitBox(bomb);

        public override PointF[] Points => new PointF[]
        {
            new PointF(Bomb.CoordX, Bomb.CoordY),
            new PointF(Bomb.CoordX + Bomb.Width, Bomb.CoordY),
            new PointF(Bomb.CoordX + Bomb.Width, Bomb.CoordY + Bomb.Height),
            new PointF(Bomb.CoordX, Bomb.CoordY + Bomb.Height),
            new PointF(Bomb.CoordX, Bomb.CoordY)
        };
    }
}
