using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Linq;

namespace Demo_Snake
{
    class Snake
    {
        private SolidBrush brush;
        public int x, y, width, height;
        private Rectangle[] snakeRec;
        Image image;
        Image rightHead;
        public Rectangle[] SnakeRec
        {
            get
            {
                return snakeRec;
            }
        }
        
        //Hàm khởi tạo rắn
        public Snake()
        {
            snakeRec = new Rectangle[3];
          //  brush = new SolidBrush(Color.Red);
            image = Properties.Resources._52;
            rightHead = Properties.Resources.dauNgangPhai;
            x = 350; y = 200; width = 20; height = 20;
            for (int i = 0; i < snakeRec.Length; i++)
            {
                snakeRec[i] = new Rectangle(x, y, width, height);
                x -= 10;
            }
        }
       
        //vẽ rắn
        public void drawSnake(Graphics paper)
        {
            int count =1;//biến này dùng để tạo đầu cho con rắn
            foreach (Rectangle r in snakeRec)
            {
                // paper.FillEllipse(brush,r);
          
                if (count == 1)
                {
                    paper.DrawImage(rightHead, r);
                    count = 0;
                }
                else
                {
                    paper.DrawImage(image, r);
                }
               
            }
        }

        //Vẽ rắn trong lúc di chuyển
        public void drawSnakeRun()
        {
            for (int i = snakeRec.Length - 1; i > 0; i--)
            {
                snakeRec[i] = snakeRec[i - 1];
            }
            
            
        }

        //Hàm di chuyển xuống
        public void moveDown()
        {
            drawSnakeRun();
            snakeRec[0].Y += 10;
        }
        //Hàm di chuyển lên
        public void moveUp()
        {
            drawSnakeRun();
            snakeRec[0].Y -= 10;
        }
        //Hàm di chuyển sang phải
        public void moveRight()
        {
            drawSnakeRun();
            snakeRec[0].X += 10;
        }
        //Hàm di chuyển sang trái
        public void moveLeft()
        {
            drawSnakeRun();
            snakeRec[0].X -= 10;
        }

        //Hàm làm cho rắn lớn lên khi ăn mồi
        public void growSnake()
        {
            List<Rectangle> rec = snakeRec.ToList();
            rec.Add(new Rectangle(snakeRec[snakeRec.Length - 1].X, snakeRec[snakeRec.Length - 1].Y, width, height));
            snakeRec = rec.ToArray();
        }
    }
}
