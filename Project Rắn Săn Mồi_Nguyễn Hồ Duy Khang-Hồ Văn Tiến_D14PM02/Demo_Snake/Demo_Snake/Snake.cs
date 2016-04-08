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
        public int x, y, width, height;
        private Rectangle[] snakeRec;
        Image image, left, down, rightHead, leftHead, up, downHead, upHead;
        public Rectangle[] SnakeRec
        {
            get
            {
                return snakeRec;
            }
        }
        
        //Hàm khởi tạo rắn
        public Snake(int snakeLength, int xCoordinate,int yCoordinate)
        {
            snakeRec = new Rectangle[snakeLength];
            image = Properties.Resources._52;
            left = Properties.Resources.left;
            rightHead = Properties.Resources.rightHead;
            leftHead = Properties.Resources.leftHead;
            down = Properties.Resources.down;
            downHead = Properties.Resources.downHead;
            up = Properties.Resources.upHead;
            upHead = Properties.Resources.up;
            x = xCoordinate; y = yCoordinate; width = 20; height = 20;
            for (int i = 0; i < snakeRec.Length; i++)
            {
                snakeRec[i] = new Rectangle(x, y, width, height);
                x -= 10;
            }
        }

        //vẽ lại con rắn bên phải
        public void drawSnake(Graphics paper)
        {
            paper.DrawImage(rightHead, snakeRec[0]);//vẽ đầu phải
            for (int i = 1; i < snakeRec.Length; i++)
            {
                paper.DrawImage(image, snakeRec[i]);//vẽ thân phải
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
        //vẽ lại hình ảnh con rắn khi di chuyển qua trái
        public void drawLeft(Graphics paper)
        {
            paper.DrawImage(leftHead, snakeRec[0]);//vẽ hình ảnh đầu trái
            for (int i = 1; i < snakeRec.Length; i++)
            {
                paper.DrawImage(left, snakeRec[i]);//vẽ hình ảnh thân trái
            }
        }
        //vẽ lại hình ảnh con rắn khi di chuyển xuống
        public void drawDown(Graphics paper)
        {
            paper.DrawImage(downHead, snakeRec[0]);//vẽ hình ảnh đầu dưới
            for (int i = 1; i < snakeRec.Length; i++)
            {
                paper.DrawImage(down, snakeRec[i]);//vẽ hình ảnh thân dưới
            }
        }
        //vẽ lại hình ảnh con rắn khi di chuyển lên
        public void drawUp(Graphics paper)
        {
            paper.DrawImage(up, snakeRec[0]);//vẽ hình ảnh đầu trên
            for (int i = 1; i < snakeRec.Length; i++)
            {
                paper.DrawImage(upHead, snakeRec[i]);//vẽ hình ảnh thân trên
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
