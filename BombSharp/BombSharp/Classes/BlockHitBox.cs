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
                  
        public Block block;

        public override PointF[] Points { get; }

    }
}
