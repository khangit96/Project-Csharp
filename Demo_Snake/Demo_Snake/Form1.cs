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
using System.Threading;



namespace Demo_Snake
{
    public partial class Form1 : Form
    {   
        //Khởi tạo điểm số
        int Score = 0;
        //Khai báo biến đếm để tạo thực phẩm lớn
        int foodCount = 0;
        int fakeFoodCount = 0;
        //khởi tạo giấy vẽ
        Graphics paper;
        Graphics paperBigFood;
        //Khởi tạo rắn
         Snake snake = new Snake();
        //Khởi tạo điều khiển
         Boolean left = false, right = false, up = false, down = false;
        //Khởi tạo thực phẩm
         Random randFood = new Random();
         Random randBigFood = new Random();
         Food food;
         Food bigFood;
        //Khởi tạo biến check xem rắn đã vẽ rắn hay chưa
         Boolean check = false;
        //Biến lưu thời gian chơi;
         int time=0;
         
        public Form1()
        {
            InitializeComponent();
            food = new Food(randFood,20,20);
            bigFood = new Food(randBigFood,50,50);
            MaximizeBox = false;//không cho phóng to form
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Hide();
            lbInfor.Text = "";
        }
     
        private void Form1_Click(object sender, EventArgs e)
        {
  
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {  
            paper = e.Graphics;

            //vẽ rắn
            snake.drawSnake(paper);

            //vẽ food
            if (foodCount == 3)//nếu như mà rắn ăn đủ 2 thực phẩm
            {
                bigFood.drawFood(paper);
                check = true;
            }
        
                food.drawFood(paper);
              
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {   
            //Sự kiện bắm phím cách để chơi
            if (e.KeyData == Keys.Space)
            {
                lbInfor.Text = "";
                timerRun.Enabled = true;//bắt đầu time
                timer1.Enabled = true;
                left = false;
                right = false;
                up = false;
                down = false;
               
            }
            //sự kiện nhấn phím up
            if (e.KeyData == Keys.Up && down ==false)
            {
                //timerRun.Start();
                up = true;
                down = false;
                left = false;
                right = false;
            }
            //sự kiện nhấn phím down
            if (e.KeyData == Keys.Down && up == false)
            {  
                up = false;
                down = true;
                left = false;
                right = false;
               // timerRun.Start();

            }
            //sự kiện nhấn phím left
            if (e.KeyData == Keys.Left && right == false)
            {
                up = false;
                down = false;
                left = true;
                right = false;
               // timerRun.Start();

            }
            //sự kiện nhấn phím right
            if (e.KeyData == Keys.Right && left == false)
            {
                up = false;
                down = false;
                left = false;
                right = true;
              //  timerRun.Start();

            }
        }

        //Khai báo timer để cho rắn di chuyển
        private void timerRun_Tick(object sender, EventArgs e)
        {    
            if (down == true)//nhấn phím down
            {
                snake.moveDown();
            }
            if (up == true)//nhấn phím up
            {
                snake.moveUp();
            }
            if (left == true)//nhấn phím left
            {
                snake.moveLeft();

            }
            if (right == true) //nhấn phím right              
            {
                snake.moveRight();
            }
            //Kiểm tra nếu rắn ăn thực phẩm
            for (int i = 0; i < snake.SnakeRec.Length; i++)
            {
                if (snake.SnakeRec[i].IntersectsWith(food.foodRec))//nếu snake va chạm với food
                {  //Play music khi ăn mồi nhỏ
                    playSmallFoodSound();
                    //Nếu như va chạm thì sẽ gán foodCount=0 để cho mất bigFood
                   // foodCount = fakeFoodCount;
                    Score += 5;
                    diemSo.Text = Score.ToString();
                    snake.growSnake();//phát triển rắn
                    //Tạo ra thực phẩm lớn nếu con rắn ăn đủ 5 thực phẩm  nhỏ
                    fakeFoodCount++;
                    check = false;
                  //thay đổi vị trí food
                    if (fakeFoodCount ==3)
                    {
                        //check = true;
                        foodCount =fakeFoodCount;
                         bigFood.foodLocation(randBigFood);
                         fakeFoodCount = 0;
                    }
                    
                        food.foodLocation(randFood);                                         
                }


                if (snake.SnakeRec[i].IntersectsWith(bigFood.foodRec))//nếu snake va chạm với bigFood
                {
                    if (check == true)
                    {
                    
                        playBigFoodSound();//play âm thanh khi ăn bigFood
                        Score += 10;
                        diemSo.Text = Score.ToString();
                        foodCount = fakeFoodCount;
                        check = false;
                    }

                }
                  
             
            }

                //Kiểm tra va chạm
                collission();
            this.Invalidate();//cập nhật lại con rắn
        }

        //Hàm Kiểm tra va chạm
        public void collission()
        {
            for (int i = 1; i < snake.SnakeRec.Length; i++)
            {   
                //nếu rắn tự cắn thân mình
                /*if (snake.SnakeRec[0].IntersectsWith(snake.SnakeRec[i]))
                {
                    playDiedSound();//Play sound khi chết
                    Thread.Sleep(2000);
                    Restart();
                }
                 */
            }
            //nếu rắn đâm vào 2 đường biên ngang
            if (snake.SnakeRec[0].Y <-10 || snake.SnakeRec[0].Y >465)
            {
                playDiedSound();//Play sound khi chết
                Thread.Sleep(2000);
                Restart();
            }
            //nếu rắn đâm vào 2 đường biên dọc
            if (snake.SnakeRec[0].X <-10 || snake.SnakeRec[0].X >805)
            {
                playDiedSound();//Play sound khi chết
                Thread.Sleep(2000);
                Restart();
            }
        }
        //Hàm restart lại con rắn khi chết
        public void Restart()
        {
            string score = Score.ToString();//gán điểm của người chơi
            timerRun.Enabled = false;
            timer1.Enabled = false;
            diemSo.Text = "0";
            Score = 0;
            time = 0;
            thoiGian.Text = "0";
           // MessageBox.Show("Snake died!");
            lbInfor.Text = "Game Over!"+"\n"+"Score: "+score;
            playGameOverSound();
            snake = new Snake();
        }
        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //Play âm thanh khi ăn mồi  nhỏ
        private void playSmallFoodSound()

        { 
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.smallFood);
                simpleSound.Play();
            }
            catch(Exception e){

            }
        }
        //Play âm thanh khi ăn mồi lớn
        private void playBigFoodSound()
        {
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Pickup_Coin5);
                simpleSound.Play();
            }
            catch (Exception e)
            {

            }
        }
        //Play âm thanh khi đụng vào tường
        private void playDiedSound()
        {
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Explosion);
                simpleSound.Play();
            }
            catch (Exception e)
            {

            }
        }
        //Play âm thanh khi gameOver
        private void playGameOverSound()
        {
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.gameOver);
                simpleSound.Play();
            }
            catch (Exception e)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            thoiGian.Text = time.ToString();
        }

        private void lbInfor_Click(object sender, EventArgs e)
        {

        }

    
    }
}
