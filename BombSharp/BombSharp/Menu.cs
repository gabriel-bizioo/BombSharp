using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace BombSharp
{
    public partial class Menu : Form
    {
        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
        
        SoundPlayer player = null;

        Bitmap bmp = null;
        Graphics g = null;

        Image img = new Bitmap(@"C:\repos\BombSharp\Sprites\spritesheet.png");

        
        
        public Menu()
        {
            InitializeComponent();
            player = new SoundPlayer(@"C:\repos\BombSharp\Audio\Hey Ya but it's super compressed - YouTube.MP4.wav");
        }


        int x = 59;
        int y = 47;
        bool front = true;

        private void Menu_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            tm.Interval = 300;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
            player.Play();
        }

        private void tm_Tick(Object sender, EventArgs e)
        {
            g.Clear(Color.FromArgb(255, 233, 127));

            if(x <= 127)
            {
                g.DrawImage(img, new Rectangle(0, 0, (pictureBox1.Width+200), pictureBox1.Height), new Rectangle(x, y, 15, 23), GraphicsUnit.Pixel);

                x += 17;
            }
            else
            {
                x = 59;
                
                g.DrawImage(img, new Rectangle(0, 0, (pictureBox1.Width+200), pictureBox1.Height), new Rectangle(x, y, 15, 23), GraphicsUnit.Pixel);

                x += 17;
            }

            pictureBox1.Image = bmp;
        }

    }
}
