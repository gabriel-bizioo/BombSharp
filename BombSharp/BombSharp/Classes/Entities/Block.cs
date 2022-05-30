using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombSharp.Classes
{
    public enum BlockType
    {
        NonDestructible,
        Destructible,
        Empty
    }
    public class Block : Entity
    {
        public static int Height { get; set; } = 1000 / 11;
        public static int Width { get; set; } = 1000 / 11;

        public Graphics BlockObj { get; set; }
        public BlockType BlockType { get; set; }
        public Rectangle block_size;

        public Block(Graphics obj, BlockType type, Rectangle size) : base(null)
        {
            this.BlockObj = obj;
            this.BlockType = type;
            this.block_size = size;
            this.HitBox = BlockHitBox.FromBlock(this);
        }

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}