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
        Player player2 = null;
        Bomb bomb = null;
        Bomb bomb2 = null;
        Rectangle rec = new Rectangle();      
        Timer tm = new Timer();
        int BlockHeight = Block.Height;
        int BlockWidth = Block.Width;

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
                
                int currentPosY = 0;
                int currentPosX = 0;
                int initialPosX = 0;
                int iRow = 0;
                int iCol = 0;
                rec.Size = new Size(BlockWidth, BlockHeight);

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
                                break;
                            //Blank space
                            case "B":
                                blocktype = BlockType.Empty;
                                break;
                            //Indestructible
                            case "C":
                                blocktype = BlockType.NonDestructible;
                                break;
                            //Next level
                            case "N":
                                blocktype = BlockType.Next;
                                break;
                            default:
                                throw new Exception("Invalid character.");
                        }
                        block = new Block(blocktype, rec);
                        block.Draw(g);
                        if (blocktype.Value != BlockType.Empty && blocktype.Value != BlockType.Next) 
                            Manager.Entities.Add(block);
                        if (blocktype.Value == BlockType.Next)
                            Manager.Next = block;
                        iCol++;
                        currentPosX += BlockWidth;
                    }
                    iRow++;
                    iCol = 0;
                    currentPosX = initialPosX;
                    currentPosY += BlockHeight;
                }
                pb.Image = mapbmp;
                strReader.Close();
            }
            //foreach (Block block in Manager.Entities)
            //{
            //    block.HitBox.Draw(g);
            //}
        }

        public void LoadPlayer()
        {
            player = new Player(false);
            player2 = new Player(true);
            bomb = new Bomb();
            bomb2 = new Bomb();

            Manager.PlayerList.Add(player);
            Manager.PlayerList.Add(player2);
            //Manager.Entities.Add(bomb);
            //Manager.Entities.Add(bomb2);
            
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            
            Manager.BombList.Add(bomb);
            Manager.BombList.Add(bomb2);
            
            tm.Interval = 25;
            tm.Tick += delegate
            {
                player.WalkAnimation();
                player2.WalkAnimation();

                Manager.HandleCollision();
                
                g.Clear(Color.Transparent);
                g.DrawImage(mapbmp, 0, 0);

                player.Draw(g);
                player2.Draw(g);

                player.Die();
                player2.Die();
                
                bomb.Draw(g);
                bomb2.Draw(g);

                if(bomb.Explode(DateTime.Now) | bomb2.Explode(DateTime.Now))
                {
                    mapbmp = Block.ReDraw(Manager.Entities, mapbmp);
                }

                pb.Refresh();
            };
            tm.Start();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W | e.KeyCode ==  Keys.A | e.KeyCode == Keys.S | e.KeyCode == Keys.D)
                player.Stop();
            
            if(e.KeyCode == Keys.Up | e.KeyCode == Keys.Left | e.KeyCode == Keys.Down | e.KeyCode == Keys.Right)
                player2.Stop();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {          
            player.KeyMovement(e.KeyCode);
            player2.KeyMovement(e.KeyCode);
            if (e.KeyCode == Keys.E)
            {
                bomb.Deploy(player);
            }
            if (e.KeyCode == Keys.End)
            {
                bomb2.Deploy(player2);
            }
            if (e.KeyCode == Keys.Enter)
                player.Respawn();

            if (e.KeyCode == Keys.PageDown)
                player2.Respawn();
        }
    }
}