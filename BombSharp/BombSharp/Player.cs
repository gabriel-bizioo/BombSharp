using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp
{
    public class animationCharacter
    {

        

        int leftX;
        int leftY;

        int upX;
        int upY;

        int downX;
        int downY;
    


        public void walkRight(Image img, Graphics g, int x, int y)
        {
            int rightX = x;
            int rightY = y;

            for (int i = 0; i < 6; i++)
            {
                g.DrawImage(img, new Rectangle(0, 0, 13, 23), new Rectangle(rightX, rightY, 13, 23), GraphicsUnit.Pixel);
                rightX += 15;
            }
        } 

        public void walkLeft()
        {

        }

        public void walkUp()
        {

        }

        public void walkDown()
        {

        }
    }
}
