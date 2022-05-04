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
        private Block[,] objLvl = new Classes.Block[11, 11];
        Bitmap bmp = null;
        Graphics g = null;
        RectangleF rec = new RectangleF();
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
            for(int i = 0; i < objLvl.GetLength(0); i++)
            {
                for(int j = 0; j < objLvl.GetLength(i); j++)
                {
                    if (element == objLvl[i, j].BlockObj)
                    {
                        result = objLvl[i, j];
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
                float blockHeight = 16;
                float blockWidth = 16;
                float currentPosY = 0;
                float currentPosX = 0;
                float initialPosX = 0;
                int iRow = 0;
                int iCol = 0;

                string strLine = string.Empty;
                while ((strLine = strReader.ReadLine()) != null)
                {
                    string[] strLineArray = strLine.Split(' ');
                    foreach(string strBlockChar in strLineArray)
                    {
                        Nullable<BlockType> blocktype = null;
                        
                        rec.Size = new SizeF(blockWidth, blockHeight);
                        rec.Location = new PointF(currentPosX, currentPosY);
                        
                        switch (strBlockChar)
                        {
                            //Destructible
                            case "D":
                                g.DrawImage(Properties.blocks.Destructible, rec);
                                blocktype = BlockType.Destructible;
                                break;
                            //Black Space
                            case "B":
                                g.DrawImage(Properties.blocks.Empty, rec);
                                blocktype = BlockType.Empty;
                                break;
                            //Indestructible
                            case "C":
                                g.DrawImage(Properties.blocks.NonDestructible, rec);
                                blocktype = BlockType.NonDestructible;
                                break;
                            default:
                                throw new Exception("Invalid character.");
                        }

                        this.objLvl[iRow, iCol] = new Block(g, blocktype.Value);
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

        }
    }
}
