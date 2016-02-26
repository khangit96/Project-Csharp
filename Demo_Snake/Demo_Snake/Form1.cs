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
    //  public partial class Form1 : Form
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
         //Biến lưu thời gian hide/show bigFood
         int bigFoodTime = 0;
        //Khởi tạo biến check xem rắn đã vẽ rắn hay chưa
         Boolean check = false;
        //Biến lưu thời gian chơi;
         int time=0;
         int hide = 1;
         int once = 1;//biến kiểm tra để vẽ con rắn bến phải lần đầu
         //kiểm tra để pause
         int pause = 1;
        //Biến dùng để check hoặt động biến Pause
         Boolean pauseCheck = false;//nếu fale thì biến Pause sẽ ko hoạt động
         Boolean checkLeft = false;
         Boolean checkRigt = false;
         Boolean checkUp = false;
         Boolean checkDown = false;
        //
         Boolean escCheck=false;//biến này để check ESC hoặt động
        public Form1()
        {   
           // this.FormBorderStyle
            InitializeComponent();
            food = new Food(randFood,20,20);
            bigFood = new Food(randBigFood,50,50);
            MaximizeBox = false;//không cho phóng to form
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbInfor.Text = "";
            hide = 0;//ẩn rắn vs food
            //lbGo.Hide();//ẩn 3 2 1 GO
            btMainMenu.Hide();
            btResume.Hide();

        }
        //Disable CLose Button
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.ClassStyle |= 0x200;
                return parms;
            }
        }
     
        private void Form1_Click(object sender, EventArgs e)
        {
  
        }
        //hide all button
        public void hideButton()
        {
            btExit.Enabled = false;
            btHighScore.Enabled = false;
            btInstruction.Enabled = false;
            btNewGame.Enabled = false;
            btNewGame.Hide();
            btInstruction.Hide();
            btHighScore.Hide();
            btExit.Hide();
            btMainMenu.Enabled = false;
            btMainMenu.Hide();
        }
        //show all button
        public void showButton()
        {
            btExit.Enabled = true;
            btHighScore.Enabled =true;
            btInstruction.Enabled =true;
            btNewGame.Enabled =true;
            btNewGame.Show();
            btInstruction.Show();
            btHighScore.Show();
            btExit.Show();
            btMainMenu.Enabled = false;
            btMainMenu.Hide();
            btNewGame.Text = "New Game";
            btNewGame.Left = 315;
            btNewGame.Top = 105;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            if (hide != 0)
            {
                //vẽ rắn bển phải lần đầu
                if (once == 1)
                {
                    snake.drawSnake(paper);
                }


            }
            if (right == true)
            {
                snake.drawSnake(paper);
            }
            else if (left == true)
            {
                snake.drawLeft(paper);
            }
            else if (down == true)
            {
                snake.drawDown(paper);
            }
            else if (up == true)
            {
                snake.drawUp(paper);
            }

            //vẽ food
            if (foodCount == 3)//nếu như mà rắn ăn đủ 2 thực phẩm
            {
                if (hide != 0)
                {
                    bigFood.drawBigFood(paper);
                    check = true;
                }
            }
            if (hide != 0)
            {
                food.drawFood(paper);
            }
           

              
              
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {   
          /*  //Sự kiện bắm phím cách để chơi
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
           */
            //Sự kiện bắm phím cách để chơi
            if (e.KeyData == Keys.Space)
            {   //kiểm tra lần đầu chơi game
                if (pauseCheck == true)
                {
                        if (pause == 1)
                        {
                            hide = 1;//biến này để check ẩn button
                            lbInfor.Text = "";//gán thông tin =""
                            timerRun.Enabled = true;//bắt đầu time
                            timer1.Enabled = true;//bắt đầu thời gian chơi
                            //kiểm tra nếu lần đầu chạy game thì sẽ cho tự động di chuyến sang phải
                            if (left == false && down == false && up == false)
                            {
                                right = true;
                            }

                            once = 0;//kiểm tra để vẽ con rắn bên phải lần đầu(trường hợp once=0 thì sẽ ko vẽ
                            pause = 0;
                        }
                        //kiểm tra để dừng lại 
                        else if (pause == 0)
                        {
                            timerRun.Enabled = false;//ngừng di chuyển rắn
                            timer1.Enabled = false;//ngừng  thời gian chơi
                            pause = 1;//tiếp tục game
                            lbInfor.Top = 180;
                            lbInfor.Left = 350;
                            lbInfor.Text = "Pause";
                            playGamePauseSound();
                            //kiểm tra nếu rắn đang di chuyển sang trái,lên trên hoặc xuống dưới thì sẽ tiếp tục di chuyển theo hướng đó
                            if (left == true || down == true || up == true)
                            {
                                right = false;
                            }

                        }
                }

            }
            //sự kiện nhấn phím up
            if (e.KeyData == Keys.Up && down ==false)
            {
                //timerRun.Start();
                up = true;
                down = false;
                left = false;
                right = false;
                once = 0;
            }
            //sự kiện nhấn phím down
            if (e.KeyData == Keys.Down && up == false)
            {  
                up = false;
                down = true;
                left = false;
                right = false;
                once = 0;
               // timerRun.Start();

            }
            //sự kiện nhấn phím left
            if (e.KeyData == Keys.Left && right == false)
            {
                up = false;
                down = false;
                left = true;
                right = false;
                once = 0;
               // timerRun.Start();

            }
            //sự kiện nhấn phím right
            if (e.KeyData == Keys.Right && left == false)
            {
                up = false;
                down = false;
                left = false;
                right = true;
                once = 0;
              //  timerRun.Start();

            }
            //Sự kiện nhấn phím ESC để Resume và hiện Main Menu
            if (e.KeyData == Keys.Escape)
            {
                    if (escCheck == true)
                    {
                        once = 0;
                        hide = 0;
                        if (left == true)
                        {
                            checkLeft =true;
                            checkRigt = false; checkUp = false; checkDown = false;
                            left = false; right = false; up = false; down = false;
                        }

                        else  if (right == true)
                        {
                            checkRigt = true;
                            checkLeft = false; checkUp = false; checkDown = false;
                            left = false; right = false; up = false; down = false;
                        }
                         else if (up == true)
                        {
                            checkUp =true;
                            checkLeft = false; checkRigt = false; checkDown = false;
                            left = false; right = false; up = false; down = false;
                        }
                         else if (down == true)
                        {
                            checkDown = true;
                            checkLeft = false; checkRigt = false; checkUp = false;
                            left = false; right = false; up = false; down = false;
                        }
                         
                        pauseCheck = false;//biến pause ko hoặt động
                        escCheck = false;
                        playGamePauseSound();

                     //   timerRun.Enabled = false;//ngừng di chuyển rắn
                        timer1.Enabled = false;//ngừng  thời gian chơi
                        btResume.Show();
                        btResume.Enabled = true;
                        btMainMenu.Show();
                        btMainMenu.Enabled = true;
                        btResume.Top = 150;
                        btResume.Left = 300;
                        btMainMenu.Left = 300;
                        btMainMenu.Top = 210;
                    } 

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
                         bigFoodTime = 0;
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
            //nếu sau 10s mà ko ăn thực phẩm lớn thì sẽ biến mất
            if (bigFoodTime == 10)
            {
                foodCount = 0;
                bigFoodTime = 0;
                fakeFoodCount = 0;
                check = false;
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
            string playTime = time.ToString();//lưu thời gian chơi
            timerRun.Enabled = false;
            timer1.Enabled = false;
            diemSo.Text = "0";
            Score = 0;
            foodCount = 0;
            time = 0;
            thoiGian.Text = "0";
            hide = 0;//ẩn rắn vs food
            pause = 1;//để khi nhắn lại phím cách sẽ play again
            lbInfor.Top = 90;
            lbInfor.Left = 300;
            lbInfor.Text = "Game Over!"+"\n"+"Score: "+score+"\n"+"Time: "+playTime+"\n"+"Level: 1";
            right = false;
            left = false; down = false; up = false;
            playGameOverSound();
            //Hiện button chơi lại khi thua
            ShowPlayAgainButton(); 
            //Hiện button Main Menu
            ShowMainMenuButton();
            snake = new Snake();
            pauseCheck = false;//ngường biến pause
            escCheck = false;//ESC ko hoặt động
        }
        //Show Play Again Button
        public void ShowPlayAgainButton()
        {
            btNewGame.Text = "Play Again";
            btNewGame.Enabled = true;
            btNewGame.Show();
            btNewGame.Top = 250;
            btNewGame.Left = 300;
        }
        //Show Main menu Button
        public void ShowMainMenuButton()
        {
            btMainMenu.Enabled = true;
            btMainMenu.Show();
            btMainMenu.Left = 300;
            btMainMenu.Top = 310;
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
        //âm thanh khi pause game
        public void playGamePauseSound()
        {
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.pause);
                simpleSound.Play();
            }
            catch (Exception e)
            {

            }
        }
        //Play âm thanh khi select
        private void playSelectSound()
        {
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.button_3);
                simpleSound.Play();
            }
            catch (Exception e)
            {

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            bigFoodTime++;
            time++;
            thoiGian.Text = time.ToString();
        }

        private void lbInfor_Click(object sender, EventArgs e)
        {

        }

        private void btNewGame_Click(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btHighScore_Click(object sender, EventArgs e)
        {

        }

        private void btInstruction_Click(object sender, EventArgs e)
        {

        }

        private void btNewGame_Click_1(object sender, EventArgs e)
        {
            pauseCheck = true;//biến pause hoạt động
            escCheck = true;//key ESC hoạt động
            newGame();
            
        }
        //Hàm tạo mới trò chơi
        public void newGame()
        {
            hideButton();
            //Hiện chữ 3
        /*    lbGo.Show();
            Thread.Sleep(1000);
            lbGo.Hide();

            //Hiện chữ 2
            lbGo.Text = "2";
            lbGo.Show();
            Thread.Sleep(1000);
            lbGo.Hide();
            //Hiện chữ 1
            lbGo.Text = "1";
            lbGo.Show();
            Thread.Sleep(1000);
            lbGo.Hide();
            //Hiện chư Go!
            lbGo.Text = "Go";
            lbGo.Show();
            Thread.Sleep(1000);
            lbGo.Hide();
         */
            //
            left = false; right = true; up = false; down = false;
            hide = 1;//biến này để check ẩn button
            lbInfor.Text = "";//gán thông tin =""
            timerRun.Enabled = true;//bắt đầu time
            timer1.Enabled = true;//bắt đầu thời gian ch
            once = 0;//kiểm tra để vẽ con rắn bên phải lần đầu(trường hợp once=0 thì sẽ ko vẽ
            pause = 0;
            Score = 0;
            time = 0;
            thoiGian.Text = "0";//gán label thời gian =""
            diemSo.Text = "0";//gán label điểm số =""
            snake = new Snake();
          
            
         
        }

        private void btNewGame_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();
        }

        private void btInstruction_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();

        }

        private void btHighScore_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();

        }

        private void btExit_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();

        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void btMainMenu_Click(object sender, EventArgs e)
        {
            hide = 0;//ẩn rắn vs food
            right = false;
            once = 0;
            left = false; down = false; up = false;
            lbInfor.Text = "";
            showButton();
            btResume.Hide();
            Score = 0;
            time = 0;
            thoiGian.Text = "0";//gán label thời gian =""
            diemSo.Text = "0";//gán label điểm số =""
        }

        private void btMainMenu_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();
        }

        private void btResume_Click(object sender, EventArgs e)
        {
            if (checkLeft == true)
            {
                left = true;
            }
            else if(checkRigt==true){
                right = true;
            }
             else if (checkUp == true)
            {
                up = true;
            }
            else if (checkDown == true)
            {
                down = true;
            }
            hide = 1;
            once = 0;
            timerRun.Enabled = true;//bắt đầu time
            timer1.Enabled = true;//bắt đầu thời gian ch
           // left = checkLeft; right = checkRigt; up = checkUp; down = checkDown;
            
            pauseCheck = true;//biến pause hoặt động
            escCheck = true;//ESC key hoặt đ
            btMainMenu.Hide();
            btMainMenu.Enabled =false;
            btResume.Hide();
            btResume.Enabled =false;
          
          

        }

        private void btResume_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();
        }

    
    }
}
