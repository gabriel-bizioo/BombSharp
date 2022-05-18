using System;
using System.Drawing;

namespace BombSharp.Classes
{
    public class PlayerHitBox : HitBox
    {


        public PlayerHitBox(Player player)
        {
            this.player = player;
        }

        Player player;

        public override PointF[] Points
        {
            //Colocar pontos do player
            get => new PointF[]
            {
                new PointF (0, 0)
            };
        }
    }
}


