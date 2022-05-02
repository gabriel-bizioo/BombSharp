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
        public Form1()
        {
            InitializeComponent();
            LoadGame(1);
        }

        public Block SearchElementInArrays(Control element)
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

            using(System.IO.StringReader strReader = new System.IO.StringReader(map))
            {
                int blockHeight = 50;
                int blockWidth = 50;
                int currentPosY = 0;
                int currentPosX = 0;
                int initialPosX = 0;
                int iRow = 0;
                int iCol = 0;

                string strLine = string.Empty;
                while ((strLine = strReader.ReadLine()) != null)
                {
                    string[] strLineArray = strLine.Split(' ');
                    foreach(string strBlockChar in strLineArray)
                    {
                        Button btn = new Button();
                        btn.Size = new Size(blockWidth, blockHeight);
                        Nullable<BlockType> blocktype = null;

                        switch (strBlockChar)
                        {
                            //Destructible
                            case "D":
                                btn.BackColor = Color.LightGray;
                                blocktype = BlockType.Destructible;
                                break;
                            //Black Space
                            case "B":
                                btn.BackColor = Color.Green;
                                blocktype = BlockType.Empty;
                                break;
                            //Indestructible
                            case "C":
                                btn.BackColor = Color.DarkGray;
                                blocktype = BlockType.NonDestructible;
                                break;
                            default:
                                MessageBox.Show($"{strBlockChar} is not a valid character");
                                break;
                        }

                        btn.Location = new Point(currentPosX, currentPosY);
                        this.Controls.Add(btn);
                        this.objLvl[iRow, iCol] = new Block(btn, blocktype.Value);

                        iCol++;
                        currentPosX += (blockWidth + 1);
                    }
                    iRow++;
                    iCol = 0;
                    currentPosX = initialPosX;
                    currentPosY += blockHeight;
                }
                strReader.Close();
            }

        }
    }
}
