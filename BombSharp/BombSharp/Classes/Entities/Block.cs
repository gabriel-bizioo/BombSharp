using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
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
        public Block(BlockType? type, Rectangle size) : base(null)
        {
            //this.BlockObj = obj;
            if(type != null)
                this.BlockType = type.Value;
            this.block_size = size;
            this.HitBox = BlockHitBox.FromBlock(this);
            attributes.SetWrapMode(WrapMode.TileFlipY);

            switch (BlockType)
            {
                case BlockType.NonDestructible:
                    this.BlockSS = Properties.blocks.NonDestructible;
                    break;
                case BlockType.Destructible:
                    this.BlockSS = Properties.blocks.Destructible;
                    break;
                case BlockType.Empty:
                    this.BlockSS = Properties.blocks.Empty;
                    break;
                default:
                    throw new ArgumentException("Block type is null. Error loading level");
            }
        }

        public static int Height { get; set; } = 1000 / 11;
        public static int Width { get; set; } = 1000 / 11;

        //public Graphics BlockObj { get; set; }
        public BlockType BlockType { get; set; }
        public Rectangle block_size;

        private Image BlockSS = null;
        private ImageAttributes attributes = new ImageAttributes();
        public override void Draw(Graphics g)
        {
            g.DrawImage(BlockSS, this.block_size, 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
        }

        public override void OnCollision(CollisionInfo info)
        {
            if (this.BlockType == BlockType.Destructible)
            {
                this.BlockType = BlockType.Empty;
                this.BlockSS = Properties.blocks.Empty;
                Console.WriteLine("Colidiu");
            }
                
        }
    }
}