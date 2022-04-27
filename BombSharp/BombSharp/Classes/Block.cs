using System;
using System.Collections.Generic;
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
        int Height { get; set; }
        int Width { get; set; }

        public Control BlockObj { get; set; }
        public BlockType BlockType { get; set; }
        public Block(Control obj, BlockType type)
        {
            this.BlockObj = obj;
            this.BlockType = type;
        }
    }
}
