using BombSharp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombSharp
{
    public partial class Form1 : Form
    {
        private Block[,] blocksLvl = new Classes.Block[11, 11];
        Bitmap bmp = null;
        Graphics g = null;
        Rectangle rec = new Rectangle();
        Player player = null;
        int blockHeight = 100;
        int blockWidth = 100;

        public Form1()
        {
            InitializeComponent();
            Load += delegate
            {
                LoadGame(1);
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
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
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
                                g.DrawImage(blockDestructible, rec);
                                blocktype = BlockType.Destructible;
                                break;
                            //Black Space
                            case "B":
                                g.DrawImage(blockEmpty, rec);
                                blocktype = BlockType.Empty;
                                break;
                            //Indestructible
                            case "C":
                                g.DrawImage(blockNonDestructible, rec);
                                blocktype = BlockType.NonDestructible;
                                break;
                            default:
                                throw new Exception("Invalid character.");
                        }

                        this.blocksLvl[iRow, iCol] = new Block(g, blocktype.Value);
                        iCol++;
                        currentPosX += blockWidth;
                    }
                    iRow++;
                    iCol = 0;
                    currentPosX = initialPosX;
                    currentPosY += blockHeight;
                }
                pictureBox1.Image = bmp;
                strReader.Close();
            }

            //Player
            Rectangle player = new Rectangle(0, 0, blockWidth, blockHeight);
            g.DrawImage(Properties.sprites.player, player);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
