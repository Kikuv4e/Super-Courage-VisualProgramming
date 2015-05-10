using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;


namespace VisualProgrammingProject
{
    public partial class Menu : Form
    {
        // Variable Declaration
        private Image button;
        private Image button1;
        private Image dog;
        private Image trophey;
        private Bitmap mountain;
        private Image treasure;
        private int width1;
        private int height1;
        private int width2;
        private int height2;
        private int font;
        private List<Person> listHighScore;
        private Point point;
        private SignIn signInForm;
        private GameWindow game;
        private Image button2;
        private int width3;
        private int height3;
        //Variable declaration end

        public Menu()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;
            Invalidate();
            button = Properties.Resources.clouds1;
            button1 = Properties.Resources.clouds1;
            button2 = Properties.Resources.clouds1;
            dog = Properties.Resources.warrior_courage_by_gth089_d4gxbg2;
            trophey = Properties.Resources.trophey;
            DoubleBuffered = true;
            treasure = Properties.Resources.money;
            width1 = 250;
            height1 = 100;
            width2 = 250;
            height2 = 100;
            width3 = 250;
            height3 = 100;
            font = 18;
            signInForm = new SignIn();
            point = System.Windows.Forms.Cursor.Position;
            listHighScore = new List<Person>();
            mountain = new Bitmap(Properties.Resources.bg3, new Size(this.Width, this.Height));

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void Draw(Graphics g)
        {

            g.DrawImage(this.button, 170 - (width1 / 2), 100 - (height1 / 2), width1, height1);
            g.DrawImage(this.button1, 170 - (width2 / 2), 200 - (height2 / 2), width2, height2);
            g.DrawImage(this.button2, 370 - (width3 / 2), 300 - (height3 / 2), width3, height3);
            g.DrawImage(this.dog, 20, 250, 250, 300);
            g.DrawImage(this.treasure, 580, 110, 80, 70);
            g.DrawImage(this.trophey, 560, 480, 45, 65);


        }

        private void Menu_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawImage(this.mountain, 0, 0);
            Draw(e.Graphics);
            DrawString(e.Graphics);
        }
        public void DrawString(Graphics formGraphics)
        {

            string drawString = "New Game";
            string drawString1 = "Quit";
            string score = "High score";
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("SHOWG.TTF");
            string howtoplay = "How to play";

            System.Drawing.Font drawFont = new System.Drawing.Font(pfc.Families[0], 18, FontStyle.Regular);
            System.Drawing.Font drawFont1 = new System.Drawing.Font(pfc.Families[0], font);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            System.Drawing.SolidBrush drawBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Firebrick);
            float x = 150.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, 90, 80, drawFormat);
            formGraphics.DrawString(drawString1, drawFont, drawBrush, 120, 180, drawFormat);
            formGraphics.DrawString(score, drawFont1, drawBrush1, 610, 500, drawFormat);
            drawFont = new System.Drawing.Font(pfc.Families[0], 14, FontStyle.Regular);
            formGraphics.DrawString(howtoplay, drawFont, drawBrush, 285, 285, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            drawBrush1.Dispose();

        }

        private void Menu_MouseHover(object sender, EventArgs e)
        {
        }

        private void Menu_MouseLeave(object sender, EventArgs e)
        {

        }

        private void Menu_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            if (p.X > 50 && p.X < 300 && p.Y > 50 && p.Y < 150)
            {
                SignIn signIn = new SignIn();
                if (signIn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    game = new GameWindow();

                    this.Hide();
                    if (game.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        signIn.player.Points = game.playerScorePoints;
                        listHighScore.Add(signIn.player);
                        this.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }

            }
            else if (point.X > 50 && point.X < 300 && point.Y > 160 && point.Y < 250)
            {
                Application.Exit();
            }else if (point.X > 225 && point.X < 225 + 250 && point.Y > 260 && point.Y < 360)
            {
                string s = "How to play?\nYour goal is to collect as much as possible coins and to stay longer alive.\nPress the arrows of the keyboard to move Courage between the clouds and to avoid the dangers (bombs and the bird).\nFor Courage to jump press the Upper arrow (or key W) and navigate right and left with the corresponding arrows (or keys A and D).\nTo exit out of the game press the ESC button.";
                MessageBox.Show(s, "How to play", MessageBoxButtons.OK);
            }
            else if (point.X > 610 && point.X < 700 && point.Y > 500 && point.Y < 550)
            {
                string result = "";
                listHighScore.Sort();
                for (int i = 0; i < listHighScore.Count; i++ )
                {
                    result += (i+1).ToString() + ". " + listHighScore[i].ToString() + "\n";
                }
                DialogResult highScore = MessageBox.Show("Best Players:\n" + result , "High Score", MessageBoxButtons.OK, MessageBoxIcon.None);
                 
            }
        }

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            point = this.PointToClient(Cursor.Position);
            Console.WriteLine("MouseOver" + point);
            if (point.X > 50 && point.X < 300 && point.Y > 50 && point.Y < 150)
            {
                this.width1 = 270;
                this.height1 = 120;
                width2 = 250;
                height2 = 100;
                width3 = 250;
                height3 = 100;
            }
            else if (point.X > 50 && point.X < 300 && point.Y > 160 && point.Y < 250)
            {
                this.width2 = 270;
                this.height2 = 120;
                width1 = 250;
                height1 = 100;
                width3 = 250;
                height3 = 100;
            }
            else if (point.X > 610 && point.X < 700 && point.Y > 500 && point.Y < 550)
            {
                this.font = 20;
            }
            else if (point.X > 225 && point.X < 225 + 250 && point.Y > 260 && point.Y < 360)
            {
                this.width2 = 250;
                this.height2 = 100;
                width1 = 250;
                height1 = 100;
                width3 = 270;
                height3 = 120;
            }
            else
            {
                this.width1 = 250;
                this.height1 = 100;
                this.width2 = 250;
                this.height2 = 100;
                font = 18;
                width3 = 250;
                height3 = 100;
            }
            Invalidate();
        }
    }
}
