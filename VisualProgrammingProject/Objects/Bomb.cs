using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VisualProgrammingProject
{
    class Bomb : Objects.MovingObject
    {
        private Bitmap bombPicture;
        public static int velocityX;
        private Image[] bombAnimation = { Properties.Resources.explozija1, Properties.Resources.explozija2, Properties.Resources.explozija3, Properties.Resources.explozija4 };
        private Timer bombTimer;
        private Image bombAnimate;
        private int index;
        private int velocity;
        public Bomb(int X, int Y, int velocity) : base(X, Y)
        {
            bombPicture = new Bitmap(Properties.Resources.Bomb);
            bombAnimate = bombAnimation[0];
            index = -1;
            bombTimer = new Timer();
            bombTimer.Interval = 100;
            bombTimer.Tick += bombTimer_Tick;
            this.velocity = velocity;
        }

        void bombTimer_Tick(object sender, EventArgs e)
        {
            if (index < 4)
                bombAnimate = bombAnimation[index++];
        }
        public void move()
        {
            x -= velocity;
        }
        public bool checkExplosionWithPlayer(Point playerLocation)
        {
            if (index >= 0)
            {

                int topLeftX = x - bombAnimate.Width / 2;
                int topLeftY = y - bombAnimate.Width / 2;
                double d = GameWindow.calculateDistance(x, y, playerLocation.X, playerLocation.Y);
                double radius = GameWindow.calculateDistance(topLeftX, topLeftY, x, y);
                return (d <= radius);
            }
            return false;
        }
        public void explode()
        {
            index = 0;
            bombTimer.Start();
        }
        public bool explodeIsOver()
        {
            return index >= 4;
        }
        public override void draw(Graphics g)
        {
            if (index < 4 && index != -1)
            {
                g.DrawImageUnscaled(bombAnimate, x - bombAnimate.Width / 2, y - bombAnimate.Height / 2);
            }
            else
            {
                g.DrawImageUnscaled(bombPicture, x, y);
            }
        }
    }
}
