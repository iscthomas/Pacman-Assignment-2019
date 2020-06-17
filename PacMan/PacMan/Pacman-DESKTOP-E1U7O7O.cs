//The Pacman class is a child of the Player class and is used to control movement of Pacman around the game field,
// keep track of when Pacman eats a pallet, and animating Pacman during movements in game.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public class Pacman : Player
    {
        //constants

        //fields
        private Bitmap pacmanL;
        private Bitmap pacmanR;
        private Bitmap pacmanU;
        private Bitmap pacmanD;

        //constructor
        public Pacman(Direction direction, Point position, Bitmap sprite, Board board)
            : base(direction, position, sprite, board)
        {

        }
        public override void Draw()
        {
            board.Rows[position.Y].Cells[position.X].Value = sprite;
        }
        public override void Move()
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        position = new Point(position.X - 1, position.Y);
                        break;
                    }
                case Direction.Right:
                    {
                        position = new Point(position.X + 1, position.Y);
                        break;
                    }
                case Direction.Up:
                    {
                        position = new Point(position.X, position.Y - 1);
                        break;
                    }
                case Direction.Down:
                    {
                        position = new Point(position.X, position.Y + 1);
                        break;
                    }
                case Direction.NoDirection:
                    {
                        position = new Point(position.X, position.Y);
                        break;
                    }
                default:
                    break;
            }
        }
        public override void DetectWall()
        {

            int currentStringPos = (position.Y * 21) + (position.X);

            switch (direction)
            {
                case Direction.Left:
                    {
                        //if (board.Rows[position.Y].Cells[position.X - 1].Value != Properties.Resources.wall)
                        //MessageBox.Show(board.Map.Substring(stringpos - 1,1));
                        //Debug.WriteLine(board.Map.Substring(nextStringPos, 1));
                        //Debug.WriteLine(nextStringPos.ToString());

                        int nextStringPos = currentStringPos - 1;
                        if (board.Map.Substring(nextStringPos,1) != "w")
                        {
                            Move();
                        }
                            break;
                    }
                case Direction.Right:
                    {
                        int nextStringPos = currentStringPos + 1;
                        if (board.Map.Substring(nextStringPos, 1) != "w")
                        {
                            Move();
                        }
                        break;
                    }
                case Direction.Up:
                    {
                        int nextStringPos = currentStringPos - 21;
                        if (board.Map.Substring(nextStringPos, 1) != "w")
                        {
                            Move();
                        }
                        break;
                    }
                case Direction.Down:
                    {
                        int nextStringPos = currentStringPos + 21;
                        if (board.Map.Substring(nextStringPos, 1) != "w")
                        {
                            Move();
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public void EatPallet()
        {

        }
        public void Animate()
        {

        }
        public Direction Direction
        {
            get => direction;
            set
            {
                //if (((direction == Direction.Left) && (value != Direction.Right)) ||
                //    ((direction == Direction.Right) && (value != Direction.Left)) ||
                //    ((direction == Direction.Up) && (value != Direction.Down)) ||
                //    ((direction == Direction.Down) && (value != Direction.Up)))
                //{
                    direction = value;
                //}
            }
        }
    }
}
