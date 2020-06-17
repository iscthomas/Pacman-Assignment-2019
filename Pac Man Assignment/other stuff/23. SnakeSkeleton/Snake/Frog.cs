using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeGame
{
    public class Frog : Creature
    {
        private Point position;
        private bool alive;

        public Frog(Bitmap head, Grid grid)
            : base(head, grid)
        {
            position = new Point(0, 0);
            alive = false;
        }

        public override void Draw()
        {
            grid.Rows[position.Y].Cells[position.X].Value = head;
        }

        public bool Alive { get => alive; set => alive = value; }
        public Point Position { get => position; set => position = value; }
    }
}
