using BombSharp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BombSharp
{
    public partial class Form1 : Form
    {
        private Block[,] blocksLvl = new Block[11, 11];
        CollisionManager manager = new CollisionManager();
        Bitmap mapbmp = null;
        Graphics g = null;
        Player player = null;
        Rectangle rec = new Rectangle();      
        Timer tm = new Timer();
        int blockHeight = Block.Height;
        int blockWidth = Block.Width;

        //int px = 0, py = 0;


        public Form1()
        {
            InitializeComponent();
            Load += delegate
            {                    
                LoadGame(1);
                LoadPlayer();
            };
        }

        public Block SearchElementInArrays(Graphics element)
        {
            Block result = null;
            for(int i = 0; i < blocksLvl.GetLength(0); i++)
            {
                for(int j = 0; j < blocksLvl.GetLength(i); j++)
                {
                    if (element == blocksLvl[i, j].BlockObj)
                    {
                        result = blocksLvl[i, j];
                    }
                }
            }
            return result;
        }
        public void LoadGame(int lvl)
        {
            mapbmp = new Bitmap(pb.Width, pb.Height);
            Graphics g = Graphics.FromImage(mapbmp);
            g.Clear(Color.White);
            string map = string.Empty;
            switch (lvl)
            {
                default:
                    MessageBox.Show($"Can't find lvl {lvl}");
                    this.Close();
                    break;
                case 1:
                    map = Properties.maps.LVL1;
                    break;
                case 2:
                    map = Properties.maps.LVL2;
                    break;
            }
            using (System.IO.StringReader strReader = new System.IO.StringReader(map))
            {
                //Generate Map with the txt
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetWrapMode(WrapMode.TileFlipY);
                
                int currentPosY = 0;
                int currentPosX = 0;
                int initialPosX = 0;
                int iRow = 0;
                int iCol = 0;

                rec.Size = new Size(blockWidth, blockHeight);

                //Blocks used
                var blockDestructible = Properties.blocks.Destructible;
                var blockEmpty = Properties.blocks.Empty;
                var blockNonDestructible = Properties.blocks.NonDestructible;

                string strLine = string.Empty;
                while ((strLine = strReader.ReadLine()) != null)
                {
                    string[] strLineArray = strLine.Split(' ');
                    foreach(string strBlockChar in strLineArray)
                    {
                        Nullable<BlockType> blocktype = null;
                        
                        rec.Location = new Point(currentPosX, currentPosY);

                        switch (strBlockChar)
                        {
                            //Destructible
                            case "D":
                                g.DrawImage(blockDestructible, rec, 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
                                blocktype = BlockType.Destructible;
                                break;
                            //Blank Space
                            case "B":
                                g.DrawImage(blockEmpty, rec, 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
                                blocktype = BlockType.Empty;
                                break;
                            //Indestructible
                            case "C":
                                g.DrawImage(blockNonDestructible, rec, 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
                                blocktype = BlockType.NonDestructible;
                                break;
                            default:
                                throw new Exception("Invalid character.");
                        }
                        if (blocktype.Value != BlockType.Empty) 
                            manager.Entities.Add(new Block(g, blocktype.Value, rec));
                        this.blocksLvl[iRow, iCol] = new Block(g, blocktype.Value, rec);
                        iCol++;
                        currentPosX += blockWidth;
                    }
                    iRow++;
                    iCol = 0;
                    currentPosX = initialPosX;
                    currentPosY += blockHeight;
                }
                pb.Image = mapbmp;
                strReader.Close();
            }
            //foreach(Block block in manager.Entities)
            //{
            //    block.HitBox.Draw(g);
            //}
        }

        public void LoadPlayer()
        {
            player = new Player(blockWidth -20, blockHeight -12);

            manager.PlayerList.Add(player);
            
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            tm.Interval = 25;
            tm.Tick += delegate
            {
                if (player.PlayerDirection == (FacingDirections.Down | FacingDirections.Moving))
                {
                    player.CoordY += player.speed;
                    player.SpriteY = 0;
                    if (player.SpriteX < 105)
                        player.SpriteX += 21;
                    else
                        player.SpriteX = 0;
                    

                }
                if (player.PlayerDirection == (FacingDirections.Right | FacingDirections.Moving))
                {
                    player.CoordX += player.speed;
                    player.SpriteY = 27;
                    if (player.SpriteX < 105)
                        player.SpriteX += 21;
                    else
                        player.SpriteX = 0;

                }
                if (player.PlayerDirection == (FacingDirections.Up | FacingDirections.Moving))
                {
                    player.CoordY -= player.speed;
                    player.SpriteY = 54;
                    if (player.SpriteX < 105)
                        player.SpriteX += 21;
                    else
                        player.SpriteX = 0;
                }
                if (player.PlayerDirection == (FacingDirections.Left | FacingDirections.Moving))
                {
                    player.CoordX -= player.speed;
                }

                manager.HandleCollision();
                g.Clear(Color.Transparent);

                g.DrawImage(mapbmp, 0, 0);
                player.Draw(g);

                pb.Refresh();
            };
            tm.Start();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            player.Stop();
            player.SpriteX = 0;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player.KeyMovement(e.KeyCode);
            //y = 27 * (int)player.PlayerDirection;
        }
    }
}
