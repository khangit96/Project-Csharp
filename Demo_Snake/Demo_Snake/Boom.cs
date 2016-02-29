using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Demo_Snake
{
    class Boom
    {
        public int x, y, width, height;
        private Rectangle[] boomRec;
        Image imgBoom;
        public Boom()
        {
            boomRec = new Rectangle[1];
            imgBoom = Properties.Resources.boom;
            x = 200; y = 200; width = 20; height = 20;
            for (int i = 0; i < boomRec.Length; i++)
            {
                boomRec[i] = new Rectangle(x, y, width, height);
                x -= 10;
            }
        }
        public void drawBoom(Graphics paper)
        {
            paper.DrawImage(imgBoom, boomRec[0]);
           
        }
        //Hàm di chuyển xuống
        public void moveDown()
        {
           
            boomRec[0].Y += 10;
        }
        //Hàm di chuyển lên
        public void moveUp()
        {

            boomRec[0].Y -= 10;
        }
        //Hàm di chuyển sang phải
        public void moveRight()
        {
            
            boomRec[0].X += 10;
        }
        //Hàm di chuyển sang trái
        public void moveLeft()
        {
           
            boomRec[0].X -= 10;
        }
    }
}
