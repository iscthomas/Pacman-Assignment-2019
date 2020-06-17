using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SnakeGame
{
    public class Snake : Creature
    {
        private const int LENGTH = 8;
        private const int SNAKESTART = 15;
        private const int CELLS = 30;

        private Bitmap body;
        private List<Point> position;
        private Direction direction;

        public Snake(Bitmap head, Grid grid, Bitmap body)
            : base(head, grid)
        {
            this.body = body;

            position = new List<Point>();

            for (int i = 0; i < LENGTH; i++)
            {
                position.Add(new Point(SNAKESTART - i, SNAKESTART));
            }

            direction = Direction.Right;
        }

        public override void Draw()
        {
            foreach (Point position in position)
            {
                grid.Rows[position.Y].Cells[position.X].Value = body;
            }

            grid.Rows[position[0].Y].Cells[position[0].X].Value = head;
        }
        public void Move()
        {
            for (int i = (position.Count - 1); i > 0; i--)
            {
                position[i] = position[i - 1];
            }

            switch (direction)
            {
                case Direction.Left:
                    {
                        position[0] = new Point(position[0].X - 1, position[0].Y);
                        break;
                    }
                case Direction.Right:
                    {
                        position[0] = new Point(position[0].X + 1, position[0].Y);
                        break;
                    }
                case Direction.Up:
                    {
                        position[0] = new Point(position[0].X, position[0].Y - 1);
                        break;
                    }
                case Direction.Down:
                    {
                        position[0] = new Point(position[0].X, position[0].Y + 1);
                        break;
                    }
                default:
                    break;
            }
        }
        public Direction Direction
        {
            get => direction;
            set
            {
                if (((direction == Direction.Left) && (value != Direction.Right)) ||
                    ((direction == Direction.Right) && (value != Direction.Left)) ||
                    ((direction == Direction.Up) && (value != Direction.Down)) ||
                    ((direction == Direction.Down) && (value != Direction.Up)))
                {
                    direction = value;
                }
            }
        }
        //public bool HitWall()
        //{
        //    if ((position[0].X < 0) || (position[0].X > 29))
        //    {
        //        return true;
        //    }
        //    if ((position[0].X < 0) || (position[0].Y > 29))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        public bool HitWall()
        {
            bool hit = false;
            if ((position[0].X < 0) || (position[0].X > (CELLS - 1)) || (position[0].Y < 0) || (position[0].Y > (CELLS - 1)))
            {
                hit = true;
            }
            return hit;
        }
        public bool HitSelf()
        {
            bool hit = false;
            for (int i = 1; i < position.Count; i++)
            {
                if (position[0] == position[i])
                {
                    hit = true;
                }
            }
            return hit;
        }
        public bool Eat(Point frogPosition)
        {
            bool eatenFrog = false;

            if (position[0] == frogPosition)
            {
                eatenFrog = true;
            }
            return eatenFrog;
        }
        public void Grow()
        {
            position.Add(position[position.Count - 1]);
        }
    }
}
