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

        public override PointF[] Points => new PointF[]
        {
            new PointF(player.PlayerPictureBox.Location.X, player.PlayerPictureBox.Location.Y),
            new PointF(player.PlayerPictureBox.Right, player.PlayerPictureBox.Location.Y),
            new PointF(player.PlayerPictureBox.Right, player.PlayerPictureBox.Bottom),
            new PointF(player.PlayerPictureBox.Location.X, player.PlayerPictureBox.Bottom),
            new PointF(player.PlayerPictureBox.Location.X, player.PlayerPictureBox.Location.Y)
        };
    }
}