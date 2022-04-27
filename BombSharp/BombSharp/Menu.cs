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
    public partial class Menu : Form
    {
        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
        

        Bitmap bmp = null;
        Graphics g = null;

        Image img = new Bitmap(@"E:\Gabriel\repos\BombSharp\Sprites\spritesheet.png");

        public Menu()
        {
            InitializeComponent();
        }

        int x = 9;
        int y;
        bool front = true;

        private void Menu_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            tm.Interval = 1000;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
        }

        private void tm_Tick(Object sender, EventArgs e)
        {
            g.Clear(Color.FromArgb(255, 233, 127));
            
            if (front)
            {
                y = 22;
                g.DrawImage(img, new Rectangle(0, 0, 13, 23), new Rectangle(x, y, 13, 23), GraphicsUnit.Pixel);
                front = false;
            }
            else
            {
                y = 47;
                g.DrawImage(img, new Rectangle(0, 0, 13, 23), new Rectangle(x, y, 13, 23), GraphicsUnit.Pixel);
                front = true;
            }

            

            pictureBox1.Image = bmp;
        }
        
    }
}
