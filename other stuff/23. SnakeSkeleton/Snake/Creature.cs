using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public abstract class Creature
    {
        //fields
        protected Bitmap head;
        protected Grid grid;

        //constructor
        public Creature(Bitmap head, Grid grid)
        {
            this.head = head;
            this.grid = grid;
        }

        public abstract void Draw();
    }
}
