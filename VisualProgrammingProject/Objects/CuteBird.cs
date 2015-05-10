using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace VisualProgrammingProject.Objects
{
    class CuteBird : MovingObject
    {
        private Image[] animation;
        private int index;
        private Timer t;
        private Image currentImage;
        public CuteBird(int x, int y)
            : base(x, y)
        {
            t = new Timer();
            t.Interval = 100;
            t.Tick += t_Tick;
            t.Start();
            animation = getFrames(Properties.Resources.cuteGifAnimation);
            index = 0;
            currentImage = animation[index];
        }

        void t_Tick(object sender, EventArgs e)
        {
            currentImage = animation[index++];
            if (index >= animation.Length) index = 0;
        }
        public bool move()
        {
            x -= 6;
            return x >= 0;
        }
        Image[] getFrames(Image originalImg)
        {

            int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);

            Image[] frames = new Image[numberOfFrames];



            for (int i = 0; i < numberOfFrames; i++)
            {

                originalImg.SelectActiveFrame(FrameDimension.Time, i);

                frames[i] = ((Image)originalImg.Clone());

            }



            return frames;

        }
        public bool checkIfCollide(Point playerLocation)
        {
            int topLeftX = x + currentImage.Width / 2;
            int topLeftY = y + currentImage.Width / 2;
            double d = GameWindow.calculateDistance(x, y, playerLocation.X, playerLocation.Y);
            double radius = GameWindow.calculateDistance(topLeftX, topLeftY, x, y);
            return (d <= radius);
        }
        public override void draw(Graphics g)
        {
            g.DrawImage(currentImage, x, y);
        }
    }
}
