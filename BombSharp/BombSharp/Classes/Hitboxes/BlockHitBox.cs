using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp.Classes
{
    public class BlockHitBox: HitBox
    {
        public BlockHitBox(Block block)
        {
            this.block = block;

            this.Points = new PointF[]
            {
                new PointF(block.block_size.X, block.block_size.Y),
                new PointF(block.block_size.Right, block.block_size.Y),
                new PointF(block.block_size.Right, block.block_size.Bottom),
                new PointF(block.block_size.X, block.block_size.Bottom),
                new PointF(block.block_size.X, block.block_size.Y)
            };

        }

        public BlockHitBox(Bomb bomb)
        {
            this.bomb = bomb;

            this.Points = new PointF[]
            {
                new PointF(bomb.CoordX, bomb.CoordY),
                new PointF(bomb.CoordX + bomb.Width, bomb.CoordY),
                new PointF(bomb.CoordX + bomb.Width, bomb.CoordY + bomb.Height),
                new PointF(bomb.CoordX, bomb.CoordY + bomb.Height),
                new PointF(bomb.CoordX, bomb.CoordY)
            };
        }

        public static BlockHitBox FromBlock(Block block)
            => new BlockHitBox(block);
        public static BlockHitBox FromBomb(Bomb bomb)
            => new BlockHitBox(bomb);

        public Block block;
        public Bomb bomb;

        public override PointF[] Points { get; }

    }
}
