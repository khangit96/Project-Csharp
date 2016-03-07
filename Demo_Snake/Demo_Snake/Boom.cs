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
        public Rectangle[] boomRec;
        Image imgBoom;
        public int[]xArray=new int[]{50,100,150,200,250,300,350,400,450,500,550,600,650,700,750,800};
        public int[] yArray = new int[] {5};
        public Boom(Random randBoom)
        {
         
            boomRec = new Rectangle[1];
            imgBoom = Properties.Resources.boom;
            int xPosition = randBoom.Next(0,15);
            x =xArray[xPosition];
            y = yArray[0];
            width = 20; height = 20;
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
        //Hàm lấy tọa độ ngẫu nhiên của boom
        public void boomLocation(Random randBoom)
        {
            x = randBoom.Next(0, 29) * 10;
            y = randBoom.Next(0, 29) * 10;
        }
        //Hàm di chuyển xuống
        public void moveDown()
        {

            boomRec[0].Y +=20;
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
