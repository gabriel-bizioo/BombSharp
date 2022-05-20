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
        public int Height { get; set; } = 50;
        public int Width { get; set; } = 50;

        public int posX { get; set; }
        public int posY { get; set; }

        public Graphics BlockObj { get; set; }
        public BlockType BlockType { get; set; }
        public Block(Graphics obj, BlockType type, int posX, int posY)
        {
            this.BlockObj = obj;
            this.BlockType = type;
            this.posX = posX;
            this.posY = posY;
        }
    }
}
