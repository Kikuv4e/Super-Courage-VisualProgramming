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
using System.Drawing.Text;


namespace VisualProgrammingProject
{
    public partial class GameWindow : Form
    {

        private Timer movingTimer;
        private CloudDocs rectangles;
        private Timer addRectangle;
        private Player player;
        private Bitmap background;
        private int playerScore;
        private SolidBrush drawBrush = new SolidBrush(Color.Black);
        private Font drawFont = new Font("Show", 16);
        private PrivateFontCollection pfc = new PrivateFontCollection();
        private Timer scoreTimer;
        private Objects.CuteBird bird;
        public int playerScorePoints { get; set; }
        private System.Media.SoundPlayer backGroundMusic;
        private bool gameOver;
        private Timer addBird;
        public GameWindow()
        {
            pfc.AddFontFile("SHOWG.TTF");
            gameOver = false;
            playerScore = 0;
            InitializeComponent();
            Clouds.clouds = new Bitmap(Properties.Resources.clouds1, new Size(150, 30));
            drawFont = new Font(pfc.Families[0], 18);
            int getHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int getWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Height = getHeight;
            this.Width = getWidth;
            this.DoubleBuffered = true;
            background = new Bitmap(Properties.Resources.clouds, new Size(this.Width, this.Height));
            rectangles = new CloudDocs(this.Width, this.Height);
            createTimers();
            Money.velocityX = 2;
            Bomb.velocityX = 2;
            player = new Player(this.Width / 2, -1800);
            backGroundMusic = new System.Media.SoundPlayer(Properties.Resources.Awawawawa___Super_Mario_Galaxy_2_1_converted);
            backGroundMusic.PlayLooping();

        }
        void createTimers()
        {
            scoreTimer = new Timer();
            scoreTimer.Interval = 500;
            scoreTimer.Tick += scoreTimer_Tick;
            scoreTimer.Start();
            addBird = new Timer();
            addBird.Interval = 3000;
            addBird.Tick += addBird_Tick;
            addBird.Start();
            movingTimer = new Timer();
            movingTimer.Interval = 1;
            movingTimer.Tick += new EventHandler(movingTimer_Tick);
            movingTimer.Start();
            addRectangle = new Timer();
            addRectangle.Interval = 1000;
            addRectangle.Tick += new EventHandler(addRectangle_Tick);
            addRectangle.Start();



        }

        void addBird_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            bird = new Objects.CuteBird(this.Width, rnd.Next(100, this.Height - 100));
            addBird.Interval = rnd.Next(8000, 10000);
        }

        void scoreTimer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            playerScorePoints += rnd.Next(100, 300);
        }


        public static double calculateDistance(int X, int Y, int X1, int Y1)
        {
            return (X1 - X) * (X1 - X) + (Y1 - Y) * (Y1 - Y);
        }
        void addRectangle_Tick(Object sender, EventArgs e)
        {
            Random rnd = new Random();
            rectangles.addRectangle();
            addRectangle.Interval = rnd.Next(500, 700);
        }
        public void gameOverFunct()
        {
            this.scoreTimer.Stop();
            gameOver = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.backGroundMusic.Stop();
            this.Close();
        }
        public void gameOverDeath()
        {
            this.scoreTimer.Stop();
            gameOver = true;
            DialogResult dialogResult = MessageBox.Show("Game Over! Would you like to continue to play?", "Game Over!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.scoreTimer.Start();
                this.playerScorePoints = 0;
                player = new Player(this.Width / 2, -1800);
                gameOver = false;
                
            }
            else if (dialogResult == DialogResult.No)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.backGroundMusic.Stop();
                this.Close();
            }
        }
        void movingTimer_Tick(Object sender, EventArgs e)
        {
            bool isAlive;
            if (bird != null && player != null)
            {
                bird.move();
                if (bird.checkIfCollide(player.getLocation()) && !gameOver)
                {
                    player.animateDeath();
                    gameOverDeath();
                }
            }

            if (player != null)
                if (player.y > this.Height && !this.gameOver)
                {
                    gameOverDeath(); 
                }
            if (player != null)
            {
                bool t = (rectangles.checkIfCollide(player.getLocation(), player.getHeight(), player.getWidth(), out isAlive, ref playerScore));

                playerScorePoints += playerScore * 1000;
                playerScore = 0;
                if (player.killedPlayer())
                {
                    player = null;
                }
                else if (isAlive)
                {
                    player.changeFalling(!t);
                    player.falling();
                }
                else
                {
                    player.changeFalling(true);
                    player.animateDeath();

                }
            }
            rectangles.move();
            Invalidate(true);
        }

        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawImage(this.background, 0, 0);
            rectangles.draw(e.Graphics);
            if (bird != null)
                bird.draw(e.Graphics);
            e.Graphics.DrawString(this.playerScorePoints.ToString(), drawFont, drawBrush, new Point(this.Width - 100, 50));
            if (player != null)
                player.draw(e.Graphics);
        }



        private void GameWindow_MouseClick(object sender, MouseEventArgs e)
        {
            player = new Player(e.Location.X, e.Location.Y);
        }

        private void GameWindow_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.player == null) return;
            if (e.KeyCode == Keys.U)
            {
                player.toMakeFalling();
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                player.move(Player.DIRECTION.left);

            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                player.move(Player.DIRECTION.right);
            }
            else if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                player.jump();
            }
        }

        private void GameWindow_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                gameOverFunct();
                this.Close();
            }

            if (player == null) return;
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                player.stopJump();
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                player.leftPress = false;
                player.playerStopMoving();
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                player.rightPress = false;
                player.playerStopMoving();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }


        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

        }

    }
}
