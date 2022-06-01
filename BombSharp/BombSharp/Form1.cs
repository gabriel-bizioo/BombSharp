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
        CollisionManager Manager = new CollisionManager();
        Bitmap mapbmp = null;
        Graphics g = null;
        Player player = null;
        Bomb bomb = null;
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

                string strLine = string.Empty;
                while ((strLine = strReader.ReadLine()) != null)
                {
                    string[] strLineArray = strLine.Split(' ');
                    foreach(string strBlockChar in strLineArray)
                    {
                        BlockType? blocktype = null;
                        
                        rec.Location = new Point(currentPosX, currentPosY);
                        Block block = null;

                        switch (strBlockChar)
                        {
                            //Destructible
                            case "D":
                                blocktype = BlockType.Destructible;
                                block = new Block(blocktype, rec);
                                block.Draw(g);
                                break;
                            //Blank Space
                            case "B":
                                blocktype = BlockType.Empty;
                                block = new Block(blocktype, rec);
                                block.Draw(g);
                                break;
                            //Indestructible
                            case "C":
                                blocktype = BlockType.NonDestructible;
                                block = new Block(blocktype, rec);
                                block.Draw(g);
                                break;
                            default:
                                throw new Exception("Invalid character.");
                        }
                        if (blocktype.Value != BlockType.Empty) 
                            Manager.Entities.Add(block);
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
            foreach(Block block in Manager.Entities)
            {
                if(block.BlockType == BlockType.Empty)
                    Manager.Entities.Remove(block);
            }
        }

        public void LoadPlayer()
        {
            player = new Player();
            bomb = new Bomb();

            Manager.PlayerList.Add(player);
            
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            Manager.BombList.Add(bomb);
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

                Manager.HandleCollision();
                g.Clear(Color.Transparent);

                g.DrawImage(mapbmp, 0, 0);
                player.Draw(g);
                bomb.Draw(g);
                if(DateTime.Now.Second == bomb.DeployTime.AddSeconds(4).Second)
                    bomb.Explode();

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
            if(e.KeyCode == Keys.E)
            {
                bomb.Deploy(player);
            }
            player.KeyMovement(e.KeyCode);
        }
    }
}