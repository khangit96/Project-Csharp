﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Demo_Snake
{
    class Food

    {   
        //Khai báo thông tin về thực phẩm
        private int x, y, width, height;//x,y là tọa độ của food trên form,width,height là kích thước của food;
        public Rectangle foodRec;
        Image smallFood, bigFood;//hình ảnh thực phẩm lớn/nhỏ

        //Hàm khởi tạo
        public Food(Random randFood,int w,int h)
        {   //random tọa độ của food
            x = randFood.Next(0,29)*10;
            y = randFood.Next(0, 29) * 10;
            //Hình ảnh food
            smallFood = Properties.Resources.apple;
            bigFood = Properties.Resources.bigFood;
            //kích thước của food
            width = w;
            height = h;
            //khởi tạo đối tượng food
            foodRec = new Rectangle(x,y,width,height);
         
        }
        //Hàm lấy tọa độ ngẫu nhiên của food
        public void foodLocation(Random randFood)
        {
            x = randFood.Next(0, 29) * 10;
            y = randFood.Next(0, 29) * 10;
        }

        //Hàm vẽ thức ăn nhỏ
        public void drawFood(Graphics paper)
        {
            foodRec.X = x;
            foodRec.Y = y;
            //  paper.FillEllipse(brush,foodRec);
            paper.DrawImage(smallFood, foodRec);
        }
        //Hàm vẽ thức ăn lớn
        public void drawBigFood(Graphics paper)
        {
            foodRec.X = x;
            foodRec.Y = y;
            //  paper.FillEllipse(brush,foodRec);
            paper.DrawImage(bigFood, foodRec);

        }
      
    }
}
