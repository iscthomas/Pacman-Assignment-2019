//The Controller class is used to start, end and control the game, and pass through win/loss conditions to the form.
//
using System;
using System.Drawing;

namespace PacMan
{
    public class Controller
    {
        //constants
        private const int PACMANSTART = 10;

        //fields
        private Board board;
        private Random random;
        private Pacman pacman;
        private Ghost ghost;

        //constructor
        public Controller(Board board)
        {
            this.board = board;

            pacman = new Pacman(Direction.Right, new Point(PACMANSTART, PACMANSTART), Properties.Resources.Pacman1, board);
        }
        public void Restart()
        {
            board.Draw();
            pacman = null;
            pacman = new Pacman(Direction.Right, new Point(PACMANSTART, PACMANSTART), Properties.Resources.Pacman1, board);
        }
        public ErrorMessage Run()
        {
            board.Draw();
            pacman.DetectWall();
            pacman.Draw();


            ErrorMessage message = ErrorMessage.noError;

            //if (pacman.DetectWall() == true)
            //{
            //    message = ErrorMessage.pacmanHitWall;
            //}
            return message;
        }
        public void GameWin()
        {

        }
        public void GameLose()
        {

        }
        public void SetPacmanDirection(Direction direction)
        {
            pacman.Direction = direction;
        }
    }
}
