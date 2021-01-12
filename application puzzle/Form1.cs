using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace application_puzzle
{
    public partial class Form1 : Form
    {
        private Button emptyBtn = null;
        private int tik = 3600;
        Size btnSize = new Size(70, 70);
        Rectangle dragbox;
        Button selectedBtn;
        int rows = 3;

        public Form1()
        {
            InitializeComponent();
            shuffle();
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Text == "")
                    emptyBtn = btn;
            }

            checkWin();
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
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Left == 0 && btn.Top == 91)
                {
                    if (btn.Text == "1")
                        counter++;
                }
                if (btn.Left == 70 && btn.Top == 91)
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
            foreach (Button btn in groupBox1.Controls)
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

        private void button_mouseDown(object sender, MouseEventArgs e)
        {
            selectedBtn = sender as Button;
            selectedBtn.MouseMove += new MouseEventHandler(this.button_mouseMove);

            if (selectedBtn == down() || selectedBtn == up() || selectedBtn == left() || selectedBtn == right())
            {
                Size drag = SystemInformation.DragSize;
                dragbox = new Rectangle(new Point(e.X - (drag.Width / 2), e.Y - (drag.Height / 2)), drag);
            }

            else
                dragbox = Rectangle.Empty;

        }
        private void button_mouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (!dragbox.Contains(e.X, e.Y) && dragbox != Rectangle.Empty)
                {
                    DragDropEffects drop = groupBox1.DoDragDrop(selectedBtn, DragDropEffects.All | DragDropEffects.Link);
                    swap(selectedBtn);
                }
            }
        }



        //finding buttons
        Button left()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Location.X == emptyBtn.Location.X + btnSize.Width && btn.Location.Y == emptyBtn.Location.Y)
                    return btn;
            }
            return null;

        }

        Button right()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Location.X == emptyBtn.Location.X - btnSize.Width && btn.Location.Y == emptyBtn.Location.Y)
                    return btn;
            }
            return null;

        }

        Button up()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Location.X == emptyBtn.Location.X && btn.Location.Y == emptyBtn.Location.Y + btnSize.Height)
                    return btn;
            }
            return null;

        }

        Button down()
        {
            foreach (Button btn in groupBox1.Controls)
            {
                if (btn.Location.X == emptyBtn.Location.X && btn.Location.Y == emptyBtn.Location.Y - btnSize.Height)
                    return btn;
            }
            return null;

        }

        //moving buttons

        void moveRight()
        {
            if (emptyBtn.Location.X == 0)
                return;

            swap(right());

        }

        void moveLeft()
        {
            if (emptyBtn.Location.X == 0 + btnSize.Width * (rows - 1))
                return;

            swap(left());
        }

        void moveUp()
        {
            if (emptyBtn.Location.Y == 91 + btnSize.Height * (rows - 1))
                return;

            swap(up());

        }

        void moveDown()
        {
            if (emptyBtn.Location.Y == 91)
                return;

            swap(down());
        }


        //swaping buttons
        void swap(Button btn)
        {
            Point xy = emptyBtn.Location;
            emptyBtn.Location = btn.Location;
            btn.Location = xy;
        }
    }
}
