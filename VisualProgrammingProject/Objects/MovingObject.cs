using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VisualProgrammingProject.Objects
{
    abstract class MovingObject
    {
        public int x { get; set; }
        public int y { get; set; }
        public MovingObject(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        abstract public void draw(Graphics g);
        
       
    }
}
