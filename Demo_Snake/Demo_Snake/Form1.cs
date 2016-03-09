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
using System.IO;
using System.Resources;
using System.Reflection;



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
         Snake snake = new Snake(3,350,200);
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
         Boolean escCheck=false;//biến này để check ESC hoặt động
         Boolean checkContinue = false;//biến này dùng để kiểm tra tiếp tục chơi game hay game mới
         string saveScore = "";//biến này dùng để lưu điểm vào file
         string snakeLength = "";//biến này dùng để lưu độ dài của rắn
         string toaDoX="";//lấy tọa độ x lưu vào file
         string toaDoY ="";//lấy tọa độ y lưu vào file
         string checkRun = "";//biến này dùng để lấy hướng di chuyển để lưu vào file
         string timePlay = "";//biến này dùng để lưu thời gian chơi vào file
         Boolean Check_Continue = true;//biến này dùng để kiểm tra xem continue lần đầu vào game hay trong quá trình chơi
        //Khởi tạo Boom
         Boom boom;
        //Biến này dùng để đếm cứ sau 3s thì sẽ random Boom
         int countRandBoom = 0;
         int fakeCountRandBoom = 0;
        //Khởi tạo đối tượng random boom
  
         Random randBoom = new Random();
        //Biến kiểm tra để paint boom
         Boolean boomCheck = false;
        public Form1()
        {   
           // this.FormBorderStyle
            InitializeComponent();
            food = new Food(randFood,20,20);
            bigFood = new Food(randBigFood,50,50);
             boom = new Boom(randBoom);


            MaximizeBox = false;//không cho phóng to form
        
        }
        //Hàm write file
        public void writeFile(string content,string directory)
        {
            using (StreamWriter Writer = new StreamWriter(directory))
            {
                Writer.WriteLine(content);
            }
        }
        //Hàm read file
        public string readFile(string directory)
        {  string text="";
            using(StreamReader Reader=new StreamReader(directory))
            {
                while(!Reader.EndOfStream){
                    text += Reader.ReadLine();
                }
            }
            return text;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lbInfor.Text = "";
            lbHightScore.Text = "";//ẩn label hightScore
            hide = 0;//ẩn rắn vs food
            btMainMenu.Hide();
            btResume.Hide();
            CheckContinue();//kiểm tra có Continue hay New Game     
            lbInstruction.Hide();//ẩn label Instruction
       }
        //Hàm hiện Continue Button
        public void showContinueButton()
        {
            btContinue.Show();
            btContinue.Enabled = true;
            
        }
        //Hàm ẩn Continue Button
        public void hideContinueButton()
        {
            btContinue.Hide();
            btContinue.Enabled = false;
        }
        //Hàm kiểm tra xem có Continue hay New Game
        public void CheckContinue()
        {
            string check = readFile(@"checkContinue.txt");
            if (check =="true")//nếu mà check=="true" có nghĩa là người dùng resume game và thoát ra thì khi đó sẽ hiện continue
            {
                btContinue.Top = 85;
                btContinue.Left = 315;
                showContinueButton();//hiện continue button
            }
            else
            {
                hideContinueButton(); //ẩn continue button
            }
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
            if (boomCheck == true)
            {
                boom.drawBoom(paper);
            }
          
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
            if (foodCount == 3)//nếu như mà rắn ăn đủ 3 thực phẩm
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
         
            //Sự kiện bắm phím cách để chơi
            if (e.KeyData == Keys.Space)
            {   //kiểm tra lần đầu chơi game
                if (pauseCheck == true)
                {
                        if (pause == 1)
                        {
                            escCheck = true;//esc key hoặt động
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
                            escCheck = false;//esc key ngừng hoặt động
                            timerRun.Enabled = false;//ngừng di chuyển rắn
                            timer1.Enabled = false;//ngừng  thời gian chơi
                            pause = 1;//tiếp tục game
                            escCheck = false;//esc key ngừng hoặt động
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
              
            }
            //sự kiện nhấn phím left
            if (e.KeyData == Keys.Left && right == false)
            {
                up = false;
                down = false;
                left = true;
                right = false;
                once = 0;
             

            }
            //sự kiện nhấn phím right
            if (e.KeyData == Keys.Right && left == false)
            {
                up = false;
                down = false;
                left = false;
                right = true;
                once = 0;
             

            }
            //Sự kiện nhấn phím ESC để Resume và hiện Main Menu
            if (e.KeyData == Keys.Escape)
            {    
                    if (escCheck == true)
                    {
                        boomCheck = false;
                        checkContinue = true;
                        Check_Continue = false;
                        saveScore = Score.ToString();//lưu điểm số vào file để lần load lên khi continue
                        snakeLength = snake.SnakeRec.Length.ToString();//lưu độ dài của rắn vào file
                        toaDoX = snake.SnakeRec[0].X.ToString();//lưu tọa độ X của rắn vào file
                        toaDoY = snake.SnakeRec[0].Y.ToString();//lưu tọa độ Y của rắn vào file
                        timePlay = time.ToString();

                        //tiến hành kiểm tra và lưu điểm cap vào file
                        int hightScore = Int16.Parse(readFile("data.txt").ToString());
                        //Tiến hành lưu điểm cao vào file data.txt
                        if (Score > hightScore)//nếu điểm số hiện tại cao hơn điểm số trong file data.txt thì lưu vào
                        { 
                            writeFile(Score.ToString(), @"data.txt");

                        }
                        once = 0;
                        hide = 0;
                        if (left == true)
                        {
                            checkLeft =true;
                            checkRun = "left";//lưu hướng di chuyển là left vào file
                            checkRigt = false; checkUp = false; checkDown = false;
                            left = false; right = false; up = false; down = false;
                        }

                        else  if (right == true)
                        {
                            checkRun = "right";//lưu hướng di chuyển là right vào file
                            checkRigt = true;
                            checkLeft = false; checkUp = false; checkDown = false;
                            left = false; right = false; up = false; down = false;
                        }
                         else if (up == true)
                        {
                            checkRun = "up";//lưu hướng di chuyển là up vào file
                            checkUp =true;
                            checkLeft = false; checkRigt = false; checkDown = false;
                            left = false; right = false; up = false; down = false;
                        }
                         else if (down == true)
                        {
                            checkRun = "down";//lưu hướng di chuyển là down vào file
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
                        btMainMenu.Text = "Main Menu";

                        btMainMenu.Show();
                        btMainMenu.Enabled = true;
                        btResume.Top = 150;
                        btResume.Left = 300;
                        btMainMenu.Left = 300;
                        btMainMenu.Top = 210;
                        hideContinueButton();//ẩn continue button
                    } 

            }
        }

        //Khai báo timer để cho rắn di chuyển
        private void timerRun_Tick(object sender, EventArgs e)
        {
            if (countRandBoom ==2)
            {
                countRandBoom = 0;
                fakeCountRandBoom =2 ;
               // boom.boomLocation(randBoom);
                boom = new Boom(randBoom);
            }
            if (fakeCountRandBoom ==2)
            {
                boom.moveDown();
            }
         
           
            boom.moveDown();
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
                  //  diemSo.Text = Score.ToString();
                    diemSo.Text = "Score: " + Score.ToString();
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
                        //diemSo.Text = Score.ToString();
                        diemSo.Text = "Score: "+Score.ToString();
                        foodCount = fakeFoodCount;
                        check = false;
                    }

                }

                //nếu rắn va chạm với vật cản

                if (snake.SnakeRec[i].IntersectsWith(boom.boomRec[0]))
                {
                  /*  if (check == true)
                    {

                        playBigFoodSound();//play âm thanh khi ăn bigFood
                        Score += 10;
                        //diemSo.Text = Score.ToString();
                        diemSo.Text = "Score: " + Score.ToString();
                        foodCount = fakeFoodCount;
                        check = false;
                    }
                   */
                    playDiedSound();//Play sound khi chết
                    Thread.Sleep(2000);
                    Restart();

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
           /* for (int i = 1; i < snake.SnakeRec.Length; i++)
            {   
                //nếu rắn tự cắn thân mình
                if (snake.SnakeRec[0].IntersectsWith(snake.SnakeRec[snake.SnakeRec.Length-1]))
                {
                    playDiedSound();//Play sound khi chết
                    Thread.Sleep(2000);
                    Restart();
                }
                 
            }*/
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
            //Tiến hành lấy điểm số từ trong file data.txt và so sánh để chọn ra điểm cao
            int hightScore = Int16.Parse(readFile("data.txt").ToString());
            //Tiến hành lưu điểm cao vào file data.txt
            if (Score > hightScore)//nếu điểm số hiện tại cao hơn điểm số trong file data.txt thì lưu vào
            {
                writeFile(score, @"data.txt");

            }
         
            timerRun.Enabled = false;
            timer1.Enabled = false;
            //diemSo.Text = "0";
            diemSo.Text = "Score: 0";
            Score = 0;
            foodCount = 0;
            time = 0;
            thoiGian.Text = "Time: 0";
            hide = 0;//ẩn rắn vs food
            pause = 1;//để khi nhắn lại phím cách sẽ play again
            lbInfor.Top = 90;
            lbInfor.Left = 300;
            lbInfor.Text = "Game Over!"+"\n"+"Score: "+score+"\n"+"Time: "+playTime+"\n"+"Level: 1";
            checkContinue = false;
            right = false;
            left = false; down = false; up = false;
            playGameOverSound();
            //Hiện button chơi lại khi thua
            ShowPlayAgainButton(); 
            //Hiện button Main Menu
            ShowMainMenuButton();
            snake = new Snake(3,350,200);
            pauseCheck = false;//ngường biến pause
            escCheck = false;//ESC ko hoặt động
            hideContinueButton();//ẩn continue button
            boomCheck = false;//boom ngừng rơi
         
            
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
            btMainMenu.Text = "Main Menu";
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
            countRandBoom++;
            thoiGian.Text = "Time: "+time.ToString();
        }

        private void lbInfor_Click(object sender, EventArgs e)
        {

        }

        private void btNewGame_Click(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (checkContinue == true)
            {
              
                writeFile("true", @"checkContinue.txt");//ghi biến checkContinue vào file
                writeFile(toaDoX, @"xCoordinate.txt");//lưu tọa độ x vào file
                writeFile(toaDoY, @"yCoordinate.txt");//lưu tọa độ y vào file
                writeFile(saveScore, @"score.txt");
                writeFile(snakeLength, @"snakeLength.txt");
                writeFile(checkRun, @"checkRun.txt");
                writeFile(timePlay, @"time.txt");

            }
            else
            {
                writeFile("false", @"checkContinue.txt");//ghi biến checkContinue vào file
                writeFile("", @"xCoordinate.txt");//lưu tọa độ x vào file
                writeFile("", @"yCoordinate.txt");//lưu tọa độ y vào file
                writeFile("", @"score.txt");
                writeFile("", @"snakeLength.txt");
                writeFile("", @"checkRun.txt");
                writeFile("", @"time.txt");

            }
           
            Application.Exit();
        }

        private void btHighScore_Click(object sender, EventArgs e)
        {
            boomCheck = false;//boom ngừng rơi
            hideButton();
            lbHightScore.Text="Hight Score: "+readFile("data.txt");
            btMainMenu.Enabled = true;
            btMainMenu.Text = "Back";
            btMainMenu.Top = 250;
            btMainMenu.Left = 300;
            btMainMenu.Show();
            hideContinueButton();//ẩn continue button
            lbInstruction.Hide();
          
           
           
        }

        private void btInstruction_Click(object sender, EventArgs e)
        {
            lbInstruction.Show();//hiện label Instruction
            hideButton();
            btMainMenu.Show();
            btMainMenu.Enabled = true;
            btContinue.Enabled = false;
            btContinue.Hide();
            btMainMenu.Text = "Back";
        }

        private void btNewGame_Click_1(object sender, EventArgs e)
        {
            boomCheck = true;//bắt đầu có boom rơi
            hideContinueButton();//ẩn continue button
            checkContinue = true;
            pauseCheck = true;//biến pause hoạt động
            escCheck = true;//key ESC hoạt động
            newGame();
            lbHightScore.Text = "";//lbHightScore=""
            lbInstruction.Hide();
          
            
        }
        //Hàm tạo mới trò chơi
        public void newGame()
        {
            hideButton();
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
            thoiGian.Text = "Time: 0";//gán label thời gian =""
            diemSo.Text = "Score: 0";//gán label điểm số =""
            snake = new Snake(3,350,200);
          
            
         
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
            boomCheck = false;
            lbInstruction.Hide();
           
            if (checkContinue == true)
            {
                  btContinue.Top = 50;
                 btNewGame.Top = 350;
                 btContinue.Left = 315;
                 btInstruction.Top = 215;
                 btHighScore.Top = 160;
                 btExit.Top = 270;
                showContinueButton();//hiện continue button

            }
            else
            {
                 hideContinueButton();//ẩn continue button

                 btContinue.Top = 50;
                 btNewGame.Top = 350;
                 btContinue.Left = 315;
                 btInstruction.Top = 215;
                 btHighScore.Top = 160;
                 btExit.Top = 270;
                
            }
            lbHightScore.Text = "";//gán lable hightScore=""
            hide = 0;//ẩn rắn vs food
            right = false;
            once = 0;
            left = false; down = false; up = false;
            lbInfor.Text = "";
            showButton();
            btResume.Hide();
            Score = 0;
            time = 0;
            thoiGian.Text = "Time: 0";//gán label thời gian =""
            diemSo.Text = "Score: 0";//gán label điểm số =""
            
        }

        private void btMainMenu_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();
        }

        private void btResume_Click(object sender, EventArgs e)
        {
            boomCheck = true;//boom bắt đầu rơi
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

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btContinue_Click(object sender, EventArgs e)
        {
            continueGame();//hàm tiếp tục chơi game
            boomCheck = true;//boom bắt đầu rơi
            lbInstruction.Hide();
          
        }
        //Hàm tiếp tục chơi game
        public void continueGame()
        {   
            //load thông tin từ file
            if (Check_Continue == true)//nếu đây là lần đầu vào game thì lấy thông tin từ file ra
            {
                    string direction = readFile("checkRun.txt");//lấy hướng di chuyển lần cuối đã lưu trong file 
                    int X_Coordinate = Int16.Parse(readFile("xCoordinate.txt").ToString());//lấy tọa độ X lần cuối của rắn đã lưu trong file 
                    int Y_Coordinate = Int16.Parse(readFile("yCoordinate.txt").ToString());//lấy tọa độ Y lần cuối của rắn đã lưu trong file 
                    int Snake_Length = Int16.Parse(readFile("snakeLength.txt").ToString());//lấy độ dài của rắn lần cuối đã lưu trong file
                    int SCORE = Int16.Parse(readFile("score.txt").ToString());//lấy điểm số đã lưu lần cuối trong file
                    int TIME = Int16.Parse(readFile("time.txt").ToString());//lấy thời gian đã lưu lần cuối trong file
                    if (direction == "left")
                    {
                        left = true;
                        once = 0;//chỉ vẽ rắn một lần
                        right = false; up = false; down = false;
                    }
                    else if (direction == "right")
                    {
                        right = true;
                        once = 0;//chỉ vẽ rắn một lần
                        left = false; up = false; down = false;
                    }
                    else if (direction == "up")
                    {
                        up = true;
                        once = 0;//chỉ vẽ rắn một lần
                        left = false; right = false; down = false;
                    }
                    else if (direction == "down")
                    {
                        down = true;
                        once = 0;//chỉ vẽ rắn một lần
                        left = false; right = false; up = false;
                    }
                    //khởi tạo rắn giống như lần chơi cuối
                    snake = new Snake(Snake_Length, X_Coordinate, Y_Coordinate);

                    Score = SCORE;
                    diemSo.Text = "Score: " + Score.ToString();
                    foodCount = 0;
                    time = TIME;
                    thoiGian.Text = "Time: " + time.ToString();
            }
            else//ngược lại thì lấy thông tin đang chơi
            {
                    snake = new Snake(Int16.Parse(snakeLength), Int16.Parse(toaDoX), Int16.Parse(toaDoY));
                    if (checkRun == "left")
                    {
                        left = true;
                        once = 0;//chỉ vẽ rắn một lần
                        right = false; up = false; down = false;
                    }
                    else if (checkRun == "right")
                    {
                        right = true;
                        once = 0;//chỉ vẽ rắn một lần
                        left = false; up = false; down = false;
                    }
                    else if (checkRun == "up")
                    {
                        up = true;
                        once = 0;//chỉ vẽ rắn một lần
                        left = false; right = false; down = false;
                    }
                    else if (checkRun == "down")
                    {
                        down = true;
                        once = 0;//chỉ vẽ rắn một lần
                        left = false; right = false; up = false;
                    }
                    Score = Int16.Parse(saveScore);
                    diemSo.Text = "Score: " + Score.ToString();
                    foodCount = 0;
                    time = Int16.Parse(timePlay);
                    thoiGian.Text = "Time: " + time.ToString();
            }
            //
            playSelectSound();
            timerRun.Enabled = true;
            timer1.Enabled = true;
            hideButton();    
            hide = 1;//hiện rắn vs food
            pauseCheck = true;// biến pause hoặt động
            escCheck = true;//ESC  hoặt động
            hideContinueButton();//ẩn continue button
        }

        private void btContinue_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void btContinue_MouseHover(object sender, EventArgs e)
        {
            playSelectSound();
        }

        private void lbHightScore_Click(object sender, EventArgs e)
        {

        }

    
    }
}
