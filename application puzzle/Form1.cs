using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace application_puzzle
{
    public partial class Form1 : Form
    {
        private Button emptyBtn = null;
        private int tik = 3600;

        public Form1()
        {
            InitializeComponent();
        }

        private Button top()
        {
            foreach(Button btn in groupBox1.Controls)
            {
                if(btn.Location.X == emptyBtn.Location.X && btn.Location.Y == emptyBtn.Location.Y + emptyBtn.Size.Width)
                {
                    return btn;
                }
            }
            return null;
        }

        void moveTop()
        {
            Button topBtn = top();
            if (topBtn != null)
            {
                int bx;
                int by;
                bx = emptyBtn.Left;
                by = emptyBtn.Top;
                emptyBtn.Left = topBtn.Left;
                emptyBtn.Top = topBtn.Top;
                topBtn.Left = bx;
                topBtn.Top = by;

            }
        }

        private Button down()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Location.X == emptyBtn.Location.X && btn.Location.Y == emptyBtn.Location.Y - emptyBtn.Size.Width)
                {
                    return btn;
                }
            }
            return null;
        }
        void moveDown()
        {
            Button downBtn = down();
            if (downBtn != null)
            {
                int bx;
                int by;
                bx = emptyBtn.Left;
                by = emptyBtn.Top;
                emptyBtn.Left = downBtn.Left;
                emptyBtn.Top = downBtn.Top;
                downBtn.Left = bx;
                downBtn.Top = by;

            }


        }

        private Button left()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Location.Y == emptyBtn.Location.Y && btn.Location.X == emptyBtn.Location.X + emptyBtn.Size.Width)
                {
                    return btn;
                }

            }
            return null;
        }
        void moveLeft()
        {
            Button leftBtn = left();
            if (leftBtn != null)
            {
                int bx;
                int by;
                bx = emptyBtn.Left;
                by = emptyBtn.Top;
                emptyBtn.Left = leftBtn.Left;
                emptyBtn.Top = leftBtn.Top;
                leftBtn.Left = bx;
                leftBtn.Top = by;
            }

        }
        private Button right()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Location.Y == emptyBtn.Location.Y && btn.Location.X == emptyBtn.Location.X - emptyBtn.Size.Width)
                {
                    return btn;
                }

            }
            return null;
        }

        void moveRight()
        {
            Button rightBtn = right();
            if (rightBtn != null)
            {
                int bx;
                int by;
                bx = emptyBtn.Left;
                by = emptyBtn.Top;
                emptyBtn.Left = rightBtn.Left;
                emptyBtn.Top = rightBtn.Top;
                rightBtn.Left = bx;
                rightBtn.Top = by;

            }
        }


        void shuffle()
        {
            int cnt = 0;
            Random rand = new Random();
            var numbers = Enumerable.Range(1, 9).OrderBy(item => rand.Next()).ToList();
            foreach (Button btn in groupBox1.Controls)
            {
                btn.Text = numbers[cnt].ToString();
                cnt++;
                if (btn.Text == "9")
                {
                    btn.Text = "";
                }
            }
            
        }

        public bool checkWin()
        {
            int counter = 0;
            foreach(Button btn in groupBox1.Controls)
            {
                if(btn.Left==0 && btn.Top == 91)
                {
                    if (btn.Text == "1")
                        counter++;
                }
                if(btn.Left== 70 && btn.Top == 91)
                {
                    if (btn.Text == "2")
                        counter++;
                }
                if (btn.Left == 140 && btn.Top == 91)
                {
                    if (btn.Text == "3")
                        counter++;
                }
                if (btn.Left == 0 && btn.Top == 161)
                {
                    if (btn.Text == "4")
                        counter++;
                }
                if (btn.Left == 70 && btn.Top == 161)
                {
                    if (btn.Text == "5")
                        counter++;
                }
                if (btn.Left == 140 && btn.Top == 161)
                {
                    if (btn.Text == "6")
                        counter++;
                }
                if (btn.Left == 0 && btn.Top == 231)
                {
                    if (btn.Text == "7")
                        counter++;
                }
                if (btn.Left == 70 && btn.Top == 231)
                {
                    if (btn.Text == "8")
                        counter++;
                }
                if (btn.Left == 140 && btn.Top == 231)
                {
                    if (btn.Text == "")
                        counter++;
                }
            }
            if (counter == 9)
                return true;
            else
                return false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            checkWin();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tik--;
            timeBOX.Text = tik / 60 + " : " + ((0) >= 10 ? (0).ToString() : "0" + (0));
            if (timeBOX.Text != "0 : 00" && checkWin())
            {
                timer1.Stop();
                MessageBox.Show("Congratulation Hero! ", "PUZZLE GAME");
                disableBtns();
                button10.Enabled = true;
                button9.Enabled = true;

                shuffle();
            }
            if (timeBOX.Text == "0 : 00")
            {
                timer1.Stop();
                MessageBox.Show("Game Over !", "PUZZLE GAME");
                disableBtns();
                button9.Enabled = true;

                button10.Enabled = true;
                shuffle();

            }
        }

        public void enableBtns()
        {
            foreach(Button btn in groupBox1.Controls)
            {
                btn.Enabled = true;
            }

        }

        public void disableBtns()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                btn.Enabled = false;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            disableBtns();
            shuffle();
        }

        private void key_press(object sender, KeyPressEventArgs e)
        {
            foreach (Button btn2 in groupBox1.Controls)
            {

                if (btn2.Text == "")
                {
                    emptyBtn = btn2;

                    if (e.KeyChar == 'a')
                    {
                        moveLeft();
                        break;
                    }
                    if (e.KeyChar == 'w')
                    {
                        moveTop();
                        break;

                    }
                    if (e.KeyChar == 's')
                    {
                        moveDown();
                        break;

                    }
                    if (e.KeyChar == 'd')
                    {
                        moveRight();
                        break;

                    }
                }

            }
        }

        private void reset_btn(object sender, EventArgs e)
        {
            shuffle();
        }

        private void Start_Btn(object sender, EventArgs e)
        {
            button10.Enabled = false;
            button9.Enabled = false;

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;
            enableBtns();
            tik = 3600;
        }
    }
}
