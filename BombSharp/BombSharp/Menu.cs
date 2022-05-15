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
        Image img = new Bitmap(Properties.sprites.walk_animations);

                
        public Menu()
        {
            InitializeComponent();
            player = new SoundPlayer(Properties.audio.heya);
        }

        //incremento do y = 25
        //incremento do x = 17
        int x = 1;
        int y = 0;


        private void Menu_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            tm.Interval = 200;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
            player.Play();
        }

        private void tm_Tick(Object sender, EventArgs e)
        {
            g.Clear(Color.White);

            if(x <= 88)
            {
                g.DrawImage(img, new Rectangle(20, 20, (pictureBox1.Width/12), (pictureBox1.Height/12)), new Rectangle(x, y, 15, 25), GraphicsUnit.Pixel);

                x += 17;
            }
            else
            {
                x = 1;
                
                g.DrawImage(img, new Rectangle(20, 20, (pictureBox1.Width/12), (pictureBox1.Height/12)), new Rectangle(x, y, 15, 25), GraphicsUnit.Pixel);

                x += 17;
            }

            pictureBox1.Image = bmp;
        }

    }
}
