using System;
using System.Drawing;

namespace BombSharp.Classes
{
    public class PlayerHitBox : HitBox
    {
        public PlayerHitBox(Player player)
        {

            this.player = player;
            this.Points = new PointF[]
            {
                new PointF(player.player_size.X, player.player_size.Y),
                new PointF(player.player_size.Right, player.player_size.Y),
                new PointF(player.player_size.Right, player.player_size.Bottom),
                new PointF(player.player_size.X, player.player_size.Bottom),
                new PointF(player.player_size.X, player.player_size.Y)
            };
        }

        Player player;

        public override PointF[] Points {get;}
    }
}


