namespace Demo_Snake
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbInfor = new System.Windows.Forms.Label();
            this.btHighScore = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btInstruction = new System.Windows.Forms.Button();
            this.btNewGame = new System.Windows.Forms.Button();
            this.btMainMenu = new System.Windows.Forms.Button();
            this.btResume = new System.Windows.Forms.Button();
            this.lbHightScore = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.diemSo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.thoiGian = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.Level = new System.Windows.Forms.ToolStripStatusLabel();
            this.btContinue = new System.Windows.Forms.Button();
            this.lbInstruction = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.statusStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRun
            // 
            this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Score:";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // statusScore
            // 
            this.statusScore.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.statusScore.Name = "statusScore";
            this.statusScore.Size = new System.Drawing.Size(13, 17);
            this.statusScore.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel3.Text = "Level:1";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel4.Text = "Score:";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel5.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbInfor
            // 
            this.lbInfor.AutoSize = true;
            this.lbInfor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbInfor.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfor.Location = new System.Drawing.Point(311, 67);
            this.lbInfor.Name = "lbInfor";
            this.lbInfor.Size = new System.Drawing.Size(197, 37);
            this.lbInfor.TabIndex = 7;
            this.lbInfor.Text = "Game Over!";
            this.lbInfor.Click += new System.EventHandler(this.lbInfor_Click);
            // 
            // btHighScore
            // 
            this.btHighScore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btHighScore.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btHighScore.ForeColor = System.Drawing.Color.Transparent;
            this.btHighScore.Location = new System.Drawing.Point(314, 260);
            this.btHighScore.Name = "btHighScore";
            this.btHighScore.Size = new System.Drawing.Size(194, 53);
            this.btHighScore.TabIndex = 14;
            this.btHighScore.Text = "High Score";
            this.btHighScore.UseVisualStyleBackColor = true;
            this.btHighScore.Click += new System.EventHandler(this.btHighScore_Click);
            this.btHighScore.MouseHover += new System.EventHandler(this.btHighScore_MouseHover);
            // 
            // btExit
            // 
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExit.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExit.ForeColor = System.Drawing.Color.Transparent;
            this.btExit.Location = new System.Drawing.Point(314, 319);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(194, 53);
            this.btExit.TabIndex = 13;
            this.btExit.Text = "Exit";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            this.btExit.MouseHover += new System.EventHandler(this.btExit_MouseHover);
            // 
            // btInstruction
            // 
            this.btInstruction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btInstruction.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btInstruction.ForeColor = System.Drawing.Color.Transparent;
            this.btInstruction.Location = new System.Drawing.Point(314, 201);
            this.btInstruction.Name = "btInstruction";
            this.btInstruction.Size = new System.Drawing.Size(194, 53);
            this.btInstruction.TabIndex = 12;
            this.btInstruction.Text = "Instruction";
            this.btInstruction.UseVisualStyleBackColor = true;
            this.btInstruction.Click += new System.EventHandler(this.btInstruction_Click);
            this.btInstruction.MouseHover += new System.EventHandler(this.btInstruction_MouseHover);
            // 
            // btNewGame
            // 
            this.btNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNewGame.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNewGame.ForeColor = System.Drawing.Color.Transparent;
            this.btNewGame.Location = new System.Drawing.Point(314, 142);
            this.btNewGame.Name = "btNewGame";
            this.btNewGame.Size = new System.Drawing.Size(194, 53);
            this.btNewGame.TabIndex = 11;
            this.btNewGame.Text = "New Game";
            this.btNewGame.UseVisualStyleBackColor = true;
            this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click_1);
            this.btNewGame.MouseHover += new System.EventHandler(this.btNewGame_MouseHover);
            // 
            // btMainMenu
            // 
            this.btMainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMainMenu.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btMainMenu.ForeColor = System.Drawing.Color.Transparent;
            this.btMainMenu.Location = new System.Drawing.Point(535, 166);
            this.btMainMenu.Name = "btMainMenu";
            this.btMainMenu.Size = new System.Drawing.Size(194, 53);
            this.btMainMenu.TabIndex = 15;
            this.btMainMenu.Text = "Main Menu";
            this.btMainMenu.UseVisualStyleBackColor = true;
            this.btMainMenu.Click += new System.EventHandler(this.btMainMenu_Click);
            this.btMainMenu.MouseHover += new System.EventHandler(this.btMainMenu_MouseHover);
            // 
            // btResume
            // 
            this.btResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btResume.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btResume.ForeColor = System.Drawing.Color.Transparent;
            this.btResume.Location = new System.Drawing.Point(535, 225);
            this.btResume.Name = "btResume";
            this.btResume.Size = new System.Drawing.Size(194, 53);
            this.btResume.TabIndex = 16;
            this.btResume.Text = "Resume";
            this.btResume.UseVisualStyleBackColor = true;
            this.btResume.Click += new System.EventHandler(this.btResume_Click);
            this.btResume.MouseHover += new System.EventHandler(this.btResume_MouseHover);
            // 
            // lbHightScore
            // 
            this.lbHightScore.AutoSize = true;
            this.lbHightScore.BackColor = System.Drawing.Color.ForestGreen;
            this.lbHightScore.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHightScore.Location = new System.Drawing.Point(240, 163);
            this.lbHightScore.Name = "lbHightScore";
            this.lbHightScore.Size = new System.Drawing.Size(251, 37);
            this.lbHightScore.TabIndex = 17;
            this.lbHightScore.Text = "Hight Score: ";
            this.lbHightScore.Click += new System.EventHandler(this.lbHightScore_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diemSo});
            this.statusStrip1.Location = new System.Drawing.Point(42, 9);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(65, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked_1);
            // 
            // diemSo
            // 
            this.diemSo.Name = "diemSo";
            this.diemSo.Size = new System.Drawing.Size(48, 17);
            this.diemSo.Text = "Score: 0";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thoiGian});
            this.statusStrip2.Location = new System.Drawing.Point(348, 9);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(63, 22);
            this.statusStrip2.TabIndex = 22;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // thoiGian
            // 
            this.thoiGian.Name = "thoiGian";
            this.thoiGian.Size = new System.Drawing.Size(46, 17);
            this.thoiGian.Text = "Time: 0";
            // 
            // statusStrip3
            // 
            this.statusStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Level});
            this.statusStrip3.Location = new System.Drawing.Point(635, 9);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Size = new System.Drawing.Size(63, 22);
            this.statusStrip3.TabIndex = 23;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // Level
            // 
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(46, 17);
            this.Level.Text = "Level: 1";
            // 
            // btContinue
            // 
            this.btContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btContinue.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btContinue.ForeColor = System.Drawing.Color.Transparent;
            this.btContinue.Location = new System.Drawing.Point(314, 83);
            this.btContinue.Name = "btContinue";
            this.btContinue.Size = new System.Drawing.Size(194, 53);
            this.btContinue.TabIndex = 24;
            this.btContinue.Text = "Continue";
            this.btContinue.UseVisualStyleBackColor = true;
            this.btContinue.Click += new System.EventHandler(this.btContinue_Click);
            this.btContinue.MouseHover += new System.EventHandler(this.btContinue_MouseHover);
            this.btContinue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btContinue_MouseMove);
            // 
            // lbInstruction
            // 
            this.lbInstruction.AutoSize = true;
            this.lbInstruction.BackColor = System.Drawing.Color.ForestGreen;
            this.lbInstruction.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInstruction.Image = global::Demo_Snake.Properties.Resources.Background;
            this.lbInstruction.Location = new System.Drawing.Point(80, 67);
            this.lbInstruction.Name = "lbInstruction";
            this.lbInstruction.Size = new System.Drawing.Size(98, 26);
            this.lbInstruction.TabIndex = 25;
            this.lbInstruction.Text = "My Snake";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.BackgroundImage = global::Demo_Snake.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(785, 425);
            this.Controls.Add(this.lbInstruction);
            this.Controls.Add(this.btContinue);
            this.Controls.Add(this.statusStrip3);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbHightScore);
            this.Controls.Add(this.btResume);
            this.Controls.Add(this.btMainMenu);
            this.Controls.Add(this.btHighScore);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btInstruction);
            this.Controls.Add(this.btNewGame);
            this.Controls.Add(this.lbInfor);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "My snake";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerRun;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusScore;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbInfor;
        private System.Windows.Forms.Button btHighScore;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btInstruction;
        private System.Windows.Forms.Button btNewGame;
        private System.Windows.Forms.Button btMainMenu;
        private System.Windows.Forms.Button btResume;
        private System.Windows.Forms.Label lbHightScore;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel diemSo;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel thoiGian;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel Level;
        private System.Windows.Forms.Button btContinue;
        private System.Windows.Forms.Label lbInstruction;
    }
}

