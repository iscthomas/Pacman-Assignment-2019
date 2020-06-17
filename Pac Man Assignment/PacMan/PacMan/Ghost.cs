//The Ghost class is a child of the Player class and is used to control movement of the ghost sprites around the screen 
// and determine a game loss state when they eat Pacman.
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class Ghost : Player
    {
        //constants
        private const int NDIR = 4; //the value of directions for the case statement that the ghosts can move in (4 = up, down, left, right)

        //fields

        //constructor
        public Ghost(Direction direction, Point position, Bitmap sprite, Board board, Random random) //Initializes the fields required for the Ghost class and passes through the fields required to draw the Ghosts on the game board.
            : base(direction, position, sprite, board, random)
        {
            this.random = random; //random is required here or else a null error will be thrown when trying to choose the next move for the ghosts
        }
        public void NextMove() //Uses random number generation to determine the next movement direction of a ghost once it hits a wall and become stationary
        {
            int nDir = random.Next(NDIR);

            switch (nDir)
            {
                case 0:
                    {
                        direction = Direction.Left;
                        break;
                    }
                case 1:
                    {
                        direction = Direction.Right;
                        break;
                    }
                case 2:
                    {
                        direction = Direction.Up;
                        break;
                    }
                case 3:
                    {
                        direction = Direction.Down;
                        break;
                    }
                default:
                    break;
            }
        }
        public override void Move() //Controls the movement of the Ghosts around the game board
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        position = new Point(position.X - 1, position.Y); //movement values are determined by changing the position of pacman by one cell in the direction specified by the enum
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
            if (direction == Direction.NoDirection) //if the ghost has hit a wall, execute the next move function
            {
                NextMove();
            }
        }

        public bool Eat(Point pacmanPosition) //Detects when the ghost has encounters pacman and returns a Boolean when he is “eaten”
        {
            bool eatenPacman = false;

            if ((position.X == pacmanPosition.X) && (position.Y == pacmanPosition.Y)) //if pacman and the ghosts position are the same, then return eaten as true.
            {
                eatenPacman = true;
            }

            switch (direction) //additional switch statement is used to predict when pacman and the ghost will collide on the next timer tick 
            {                  //to prevent pacman and the ghosts passing through eachother on occasion.
                case Direction.Up:
                    {
                        if ((position.X == pacmanPosition.X) && (position.Y - 1 == pacmanPosition.Y)) //if the ghost is moving up, and pacman is in the cell above the ghost, return eaten as true.
                        {
                            eatenPacman = true;
                        }
                        break;
                    }
                case Direction.Down:
                    {
                        if ((position.X == pacmanPosition.X) && (position.Y + 1 == pacmanPosition.Y))
                        {
                            eatenPacman = true;
                        }
                        break;
                    }
                case Direction.Left:
                    {
                        if ((position.X - 1 == pacmanPosition.X) && (position.Y == pacmanPosition.Y))
                        {
                            eatenPacman = true;
                        }
                        break;
                    }
                case Direction.Right:
                    {
                        if ((position.X + 1 == pacmanPosition.X) && (position.Y == pacmanPosition.Y))
                        {
                            eatenPacman = true;
                        }
                        break;
                    }
                case Direction.NoDirection:
                    {
                        break;
                    }
                default:
                    break;
            }
            return eatenPacman;
        }
    }
}
