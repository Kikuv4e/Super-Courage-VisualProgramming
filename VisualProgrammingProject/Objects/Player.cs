using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProgrammingProject
{

    class Player : Objects.MovingObject
    {
        // Variable Declaration Begin
        private Image playerImage;
        private int velocity;
        private bool isFalling;
        private bool isUp;
        private Timer toJump;
        private Timer moveLeftRight;
        private Timer toMoveTimer;
        private int isUpMove;
        private double passingAngle;
        private int directionMove;
        private Image[] leftAnimationImage = { Properties.Resources.dogFliped, Properties.Resources.leg_upFliped, Properties.Resources.leg_up2Fliped, Properties.Resources.leg_upFliped };
        private Image[] rightAnimationImage = { Properties.Resources.playerObjectPicture, Properties.Resources.leg_up, Properties.Resources.leg_up2, Properties.Resources.leg_up };
        private Image leftImage;
        private Image rightImage;
        private int imageIndex;
        private Point rememberPosition;
        private int jumpLimit;
        private bool animateDeathCheck;
        private Image animateDeathPicture;
        private int animationDeathX;
        private int animationDeathY;
        private Timer deathAnimationTimer;
        private bool isAlivePlayer;
        private int radiusX;
        private int radiusY;
        private double currentAngle;
        private bool checkIfUp;
        private bool killPlayer;
        private bool pressedWhileUp;
        public static int playerVelocity;
        public bool leftPress { get; set; }
        public bool rightPress { get; set; }
        public enum DIRECTION { left, right, up };
        // Variable declaration end
        public Player(int x, int y) : base (x, y)
        {
            imageIndex = 1;
            isFalling = true;
            isUpMove = 0;
            playerVelocity = 2;
            initTimers();
            initImages();
            jumpLimit = 130;
            velocity = 4;
            leftPress = false;
            rightPress = false;
            this.animateDeathCheck = false;
            this.animationDeathX = 2;
            this.animationDeathY = 2;
            isAlivePlayer = true;
            this.passingAngle = (1.0 / 128) * 2 * Math.PI;
            this.currentAngle = 0;
            killPlayer = false;

        }
        public void initImages()
        {
            playerImage = Properties.Resources.playerObjectPicture;
            this.animateDeathPicture = Properties.Resources.courage_explode;
            this.leftImage = Properties.Resources.dogFliped;
            this.rightImage = Properties.Resources.playerObjectPicture;
        }
        // Timers initialization
        public void initTimers()
        {
            deathAnimationTimer = new Timer();
            deathAnimationTimer.Interval = 6000;
            deathAnimationTimer.Tick += deathAnimationTimer_Tick;
            toJump = new Timer();
            toJump.Interval = 1;
            toJump.Tick += new EventHandler(toJump_Tick);
            moveLeftRight = new Timer();
            moveLeftRight.Interval = 1;
            moveLeftRight.Tick += new EventHandler(moveLeftRight_Tick);
            toMoveTimer = new Timer();
            toMoveTimer.Interval = 1;
            toMoveTimer.Tick += toMoveTimer_Tick;

        }

        void deathAnimationTimer_Tick(object sender, EventArgs e)
        {
            killPlayer = true;
            deathAnimationTimer.Stop();
        }
        // Object moving animation
        public void toMove()
        {
            if (directionMove < 0)
            {
                this.playerImage = leftAnimationImage[imageIndex++];
                if (imageIndex == 4) imageIndex = 0;
            }
            else
            {
                this.playerImage = rightAnimationImage[imageIndex++];
                if (imageIndex == 4) imageIndex = 0;
            }
            if (leftPress && directionMove > 0 && !rightPress) directionMove = -velocity;
            if (rightPress && directionMove < 0 && !leftPress) directionMove = velocity;
            x += directionMove;
        }
        //Timer for object moving animation
        public void toMoveTimer_Tick(object sender, EventArgs e)
        {
            toMove();
        }
        //Left Right movement while in air
        public void moveLeftRight_Tick(object sender, EventArgs e)
        {
            x += isUpMove;
        }
        // Jumping function
        public void jump()
        {
            checkIfUp = leftPress || rightPress;
            if (!isFalling && !isUp && !animateDeathCheck)
            {
                int direction = directionMove < 0 ? -1 : 1;
                radiusY = 130;
                radiusX = 240;
                rememberPosition = new Point(this.x + direction * radiusX, this.y);
                isUp = true;
                this.jumpLimit = y - 200;
                toJump.Start();
            }

        }
        // Jump timer 
        public void stopJump()
        {
            toJump.Stop();
            moveLeftRight.Stop();
            currentAngle = 0;
            isUp = false;
            pressedWhileUp = false;
        }
        public void toJump_Tick(Object sender, EventArgs e)
        {
            if (!checkIfUp && !leftPress && !rightPress && !pressedWhileUp) 
            {
                if (y <= jumpLimit) stopJump();
                y -= velocity;
            }
            else
            {
                int direction = directionMove < 0 ? -1 : 1;
                y = (int)(rememberPosition.Y - radiusY * Math.Sin(currentAngle));
                x = (int)(rememberPosition.X - direction * radiusX * Math.Cos(currentAngle)) + (direction  * 15);
                currentAngle += passingAngle;
                if (currentAngle >= Math.PI / 2 || (leftPress == false && rightPress == false)) this.stopJump();
            }

        }
        public void toMakeFalling()
        {
            isFalling = !isFalling;
        }
        public void changeFalling(bool falling)
        {
            if (isAlivePlayer)
                isFalling = falling;
        }
        public bool killedPlayer()
        {
            return this.killPlayer;
        }
        public void falling()
        {
            y = isFalling && !isUp ? y += 5 : y;
            if (!isFalling && !isUp)
            {
                x -= Player.playerVelocity;
            }
        }
        public void playerStopMoving()
        {
            if (directionMove < 0) this.playerImage = Properties.Resources.dogFliped;
            else this.playerImage = Properties.Resources.playerObjectPicture;
            if (leftPress == false && rightPress == false)
                toMoveTimer.Stop();
        }
        public void move(DIRECTION direction)
        {
            if (this.animateDeathCheck) return;
            if (!leftPress && direction == DIRECTION.left)
            {
                if (isUp && pressedWhileUp) return;
                leftPress = true;
                directionMove = -velocity;
                if (isUp)
                {
                    pressedWhileUp = true;
                    int direction1 = directionMove < 0 ? -1 : 1;
                    rememberPosition.X = x + direction1*radiusX;
                    rememberPosition.Y = y;
                    y = (int)(rememberPosition.Y - radiusY * Math.Sin(currentAngle));
                    x = (int)(rememberPosition.X - direction1 * radiusX * Math.Cos(currentAngle)) + (direction1 * 15);
                }
            }
            if (!rightPress && direction == DIRECTION.right)
            {
                if (isUp && pressedWhileUp) return;
                rightPress = true;
                directionMove = velocity;
                if (isUp)
                {
                    pressedWhileUp = true;
                    int direction1 = directionMove < 0 ? -1 : 1;
                    rememberPosition.X = x + direction1*radiusX;
                    rememberPosition.Y = y;
                    y = (int)(rememberPosition.Y - radiusY * Math.Sin(currentAngle));
                    x = (int)(rememberPosition.X - direction1 * radiusX * Math.Cos(currentAngle)) + (direction1 * 15);
                }
            }
            
            if (direction == DIRECTION.left && !isUp)
            {
                toMoveTimer.Start();
                directionMove = -velocity;
            }
            else if (direction == DIRECTION.right && !isUp)
            {
                toMoveTimer.Start();
                directionMove = velocity;
            }


        }
        public Point getLocation()
        {
            return new Point(this.x, this.y);
        }
        public int getHeight()
        {
            return playerImage.Height;
        }
        public int getWidth()
        {
            return playerImage.Width;
        }
        public override void draw(Graphics g)
        {
            if (animateDeathCheck)
            {

                g.DrawImage(this.animateDeathPicture, x + this.animationDeathX, y + this.animationDeathY);
                Random rnd = new Random();
                this.animationDeathX = rnd.Next(1, 10);
                this.animationDeathY = rnd.Next(1, 10);
            }
            else g.DrawImage(this.playerImage, x, y, this.playerImage.Width, this.playerImage.Height);

        }
        public void animateDeath()
        {
            if (isAlivePlayer == true)
            {
                this.deathAnimationTimer.Start();
                isAlivePlayer = false;
                animateDeathCheck = true;
            }
        }
        public override string ToString()
        {
            return String.Format("Highscore : {0} ");
        }

    }

}
