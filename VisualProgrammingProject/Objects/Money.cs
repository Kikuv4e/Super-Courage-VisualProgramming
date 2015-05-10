using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VisualProgrammingProject
{
    class Money : Objects.MovingObject
    {
        private Bitmap moneyPicture;
        public static int velocityX;
        private int velocity;
        public Money(int x, int y, int velocity) : base(x, y)
        {
            moneyPicture = new Bitmap(Properties.Resources.thumbs_up_gold_coin_hi, new Size(30, 30));
            this.velocity = velocity;
        }
        public void move(){
            x -= velocity;
        }
        public bool checkIfCollide(Point playerLocation)
        {

            return (playerLocation.X >= this.x && playerLocation.X <= this.x + 30 ) && (playerLocation.Y >= this.y && playerLocation.Y <= this.y + 30);
        }
        public override void  draw(Graphics g)
        {
            g.DrawImageUnscaled(moneyPicture, x, y);
        }
    }
}
