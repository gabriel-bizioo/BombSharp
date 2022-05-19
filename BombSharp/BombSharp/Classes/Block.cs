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
    public class Block
    {
        public static int Height { get; set; } = 1000 / 11;
        public static int Width { get; set; } = 1000 / 11;

        public Graphics BlockObj { get; set; }
        public BlockType BlockType { get; set; }
        public Block(Graphics obj, BlockType type)
        {
            this.BlockObj = obj;
            this.BlockType = type;
        }
    }
}
