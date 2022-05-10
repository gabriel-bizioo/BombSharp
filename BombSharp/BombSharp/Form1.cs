﻿using BombSharp.Classes;
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
        Bitmap bmp = null;
        Graphics g = null;
        Rectangle rec = new Rectangle();
        Player player = new Player();
        Timer tm = new Timer();
        int blockHeight = 100;
        int blockWidth = 100;
        int y = 0;
        int x = 1;


        public Form1()
        {
            InitializeComponent();
            Load += delegate
            {
                LoadGame(1);
                //tm.Interval = 200;
                //tm.Tick += delegate
                //{
                //    Rectangle player = new Rectangle(blockWidth + 7, blockHeight + 10, blockWidth - 15, blockHeight - 15);
                //    if (x <= 100)
                //    {
                //        g.DrawImage(Properties.sprites.player, player, new Rectangle(0, y, 14, 24), GraphicsUnit.Pixel);

                //        x += 20;
                //    }
                //    else
                //    {
                //        x = 1;

                //        g.DrawImage(Properties.sprites.player, player, new Rectangle(0, y, 14, 24), GraphicsUnit.Pixel);

                //        x += 20;
                //    }
                //};
                //tm.Start();
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
                                g.DrawImage(Properties.blocks.Destructible, new Rectangle((int)currentPosX, (int)currentPosY, (int)blockWidth, (int)blockHeight), 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
                                blocktype = BlockType.Destructible;
                                break;
                            //Black Space
                            case "B":
                                g.DrawImage(Properties.blocks.Empty, new Rectangle((int)currentPosX, (int)currentPosY, (int)blockWidth, (int)blockHeight), 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
                                blocktype = BlockType.Empty;
                                break;
                            //Indestructible
                            case "C":
                                g.DrawImage(Properties.blocks.NonDestructible, new Rectangle((int)currentPosX, (int)currentPosY, (int)blockWidth, (int)blockHeight), 0, 0, 16, 16, GraphicsUnit.Pixel, attributes);
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
            Rectangle player = new Rectangle(blockWidth + 7, blockHeight + 10, blockWidth - 15, blockHeight - 15);
            g.DrawImage(Properties.sprites.player, player, new Rectangle(y, 0, 17, 24), GraphicsUnit.Pixel);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player.keyMovement(e.KeyCode);

            y = 26 * (int)player.playerDirection; 
        }
    }
}
