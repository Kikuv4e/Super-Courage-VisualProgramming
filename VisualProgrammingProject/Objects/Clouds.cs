using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VisualProgrammingProject
{
    class Clouds
    {
        private int x;
        private int y;
        private int height;
        private int width;
        private Brush brush;
        private Point location;
        public static Bitmap clouds;
        private Money coin;
        private bool haveCoin;
        private bool haveBomb;
        private Timer bombTimeToLive;
        private bool isCloudAlive;
        private Bomb bomb;
        private int velocity;
        public Clouds(int x, int y)
        {
            location = new Point(x, y);
            Random rnd = new Random();
            int number = rnd.Next(0, 10);
            isCloudAlive = true;
            int yOffset = 25;
            this.velocity = 2;
            int bombProbability = rnd.Next(0, 100);
            if (bombProbability < 20)
            {
                haveBomb = true;
                int offset = rnd.Next(20, 100);
                bombTimeToLive = new Timer();
                bombTimeToLive.Tick += bombTimeToLive_Tick;
                int liveInterval = rnd.Next(2000, 10000);
                bombTimeToLive.Interval = liveInterval;
                bombTimeToLive.Start();
                bomb = new Bomb(x + offset, y - yOffset, velocity);
            }
            if (number < 3)
            {
                int offset = rnd.Next(20, 100);
                haveCoin = true;
                this.coin = new Money(x + offset, y - yOffset, velocity);
            }
            brush = new SolidBrush(Color.White);
            this.height = 30;
            this.width = 150;

        }

        void bombTimeToLive_Tick(object sender, EventArgs e)
        {
            haveBomb = false;
            isCloudAlive = false;
            bomb.explode();
        }


        public bool move()
        {
            if (!isCloudAlive) return bomb.explodeIsOver();
            if (haveCoin)
                this.coin.move();
            if (haveBomb)
            {
                this.bomb.move();
            }
            location.X -= velocity;
            return location.X <= -width;
        }

        public void draw(Graphics g)
        {
            if(isCloudAlive)
            g.DrawImage(clouds, location.X, location.Y);
            if (haveCoin && isCloudAlive)
                this.coin.draw(g);
            if (haveBomb)
            {
                this.bomb.draw(g);
            }
            else if (!haveBomb && this.bomb != null)
            {
                if (!this.bomb.explodeIsOver())
                {
                    this.bomb.draw(g);
                }
            }
            
        }
        public bool checkPlayerPosition(Point left, Point right, int playerHeight, int playerWidth, out bool isAlive, ref int score)
        {
            isAlive = true;
            if (bomb != null)
            {
                if (this.bomb.checkExplosionWithPlayer(left) || this.bomb.checkExplosionWithPlayer(right))
                {
                    isAlive = false;
                }
            }
            if (haveCoin)
            {
                haveCoin = this.checkWithCoin(left, right, playerHeight, playerWidth);
                if (haveCoin == false) score++;
            }
            return ((this.location.Y <= left.Y && (this.location.Y + this.height - 20) >= left.Y && this.location.X <= left.X && (this.location.X  + this.width - 40) >= left.X) || (this.location.Y <= right.Y && (this.location.Y + this.height - 20) >= right.Y && this.location.X + 30 <= right.X && (this.location.X + this.width ) >= right.X));
        }
        public bool checkWithCoin(Point left, Point right, int playerHeight, int playerWidth)
        {
            Point littleUp = new Point(right.X, right.Y - 30);
            Point littleRight = new Point(right.X - 30, right.Y);
            if (coin.checkIfCollide(left) || coin.checkIfCollide(right) || coin.checkIfCollide(littleUp) || coin.checkIfCollide(littleRight))
            {
                return false;
            }
            return true;
        }
        public override string ToString()
        {
            return String.Format("{0}   {1}", x, y);
        }
    }
}
