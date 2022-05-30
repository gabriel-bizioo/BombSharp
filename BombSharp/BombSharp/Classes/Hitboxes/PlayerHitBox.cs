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

        private Player player;

        public static PlayerHitBox FromPlayer(Player player)
            => new PlayerHitBox(player);

        //Hitbox width = 26px
        //hitbox height = 5px
        public override PointF[] Points => new PointF[]
        {
            new PointF(player.CoordX, player.CoordY),
            new PointF(player.CoordX + player.Width, player.CoordY),
            new PointF(player.CoordX + player.Width, player.CoordY + player.Height),
            new PointF(player.CoordX, player.CoordY + player.Height),
            new PointF(player.CoordX, player.CoordY)
        };
    }
}