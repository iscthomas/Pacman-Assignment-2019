//The Player class is a parent that is used to draw Pacman and the ghosts to the screen, 
//as well as determine the position of the sprites on screen and detect collision with the walls and boundaries of the game field.
//Ghost and Pacman share the same code for draw and teleport methods, so they are declared in the Parent class here.
//Since Player is a parent, the methods in this class are inherited by the child classes; pacman and ghost.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public abstract class Player
    {
        //constants

        //fields
        protected Direction direction;
        protected Point position;
        protected Bitmap sprite;
        protected Board board;
        protected Random random;

        //constructor
        public Player(Direction direction, Point position, Bitmap sprite, Board board, Random random) //Initializes the fields required for the Player classes and passes through the fields required to allow the Ghost and Pacman classes to operate
        {
            this.direction = direction;
            this.position = position;
            this.sprite = sprite;
            this.board = board;
        }
        public void Draw() //Draws the sprites to the game board using bitmap Images
        {
            board.Rows[position.Y].Cells[position.X].Value = sprite; //since both ghost and pacman use the same code for draw, we can declare it in the parent class
        }
        public void Teleport() //Detects when the position of a sprite is in the teleport tunnel location and teleports that sprite to the exit position on the opposite side of the game board
        {
            System.Media.SoundPlayer teleport = new System.Media.SoundPlayer(Properties.Resources.portal);

            if ((position.X == 0) && (position.Y == (int)eBoard.TELEPORTY))             //when the sprite enters the end of the left tunnel..
            {
                position = new Point((int)eBoard.TELEPORTX - 1, (int)eBoard.TELEPORTY); //..they will be teleported to the end of the right tunnel
                teleport.Play();
            }
            if ((position.X == (int)eBoard.TELEPORTX) && (position.Y == (int)eBoard.TELEPORTY))
            {
                position = new Point(1, (int)eBoard.TELEPORTY);
                teleport.Play();
            }
        }
        public void DetectWall() //Detects when the position of a sprite is trying to move into a wall so that it doesn’t pass through the wall on the game board

        {
            int currentStringPos = (position.Y * (int)eBoard.NCOLUMNS) + (position.X);  //converts current position of the sprite in the game board string to an integer value

            switch (direction)
            {
                case Direction.Left:
                    {
                        int nextStringPos = currentStringPos - 1;           //set next string position to be ahead of current position by 1 cell
                        if (board.Map.Substring(nextStringPos, 1) == "w")   //if the next position of the sprite is a wall...
                        {
                            direction = Direction.NoDirection;              //... then we halt the sprites movement
                        }
                        break;
                    }
                case Direction.Right:
                    {
                        int nextStringPos = currentStringPos + 1;
                        if (board.Map.Substring(nextStringPos, 1) == "w")
                        {
                            direction = Direction.NoDirection;
                        }
                        break;
                    }
                case Direction.Up:
                    {
                        int nextStringPos = currentStringPos - (int)eBoard.NCOLUMNS;
                        if (board.Map.Substring(nextStringPos, 1) == "w")
                        {
                            direction = Direction.NoDirection;
                        }
                        break;
                    }
                case Direction.Down:
                    {
                        int nextStringPos = currentStringPos + (int)eBoard.NCOLUMNS;
                        if (board.Map.Substring(nextStringPos, 1) == "w")
                        {
                            direction = Direction.NoDirection;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public abstract void Move(); //Controls the movement of the sprites around the game board on the screen.

        public Direction Direction { get => direction; set => direction = value; }
        public Point Position { get => position; set => position = value; }
        public Bitmap Sprite { get => sprite; set => sprite = value; }
    }
}
