using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BombSharp.Classes
{
    public abstract class Entity
    {


        public HitBox HitBox { get; set; }
        public PointF Location { get; set; }

        public void CheckCollision()
        {

        }
    }
}
