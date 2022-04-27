using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp
{
    public static class Animator
    {
        public static void WalkRightAnim(Graphics g, Image img, int x)
        {
            int y = 47;
            for(int i = 0; i < 6; i++)
            {
                g.DrawImage(img, new Rectangle(0, 0, 13, 23), new Rectangle(x, y, 13, 23), GraphicsUnit.Pixel);
            }
        }


    }
}
