//The Controller class is used to start, end and control the game, and pass through win/loss conditions to the form.
//
using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace PacMan
{
    public class Controller
    {
        //constants
        private const int PACMANSTARTX = 9;     //X pos of the starting position for pacman
        private const int PACMANSTARTY = 16;    //Y pos of the starting position for pacman
        private const int BEGIN = 4200;         //sleep timer of 4.2 seconds before game starts so that the game start sound fully plays before the player gains control.

        //fields
        private Board board;
        private Pacman pacman;
        private GhostManager ghostManager;
        private int highScore;
        private int totalScore;

        //constructor
        public Controller(Board board, Random random) //The constructor initializes the fields required to run the game as well as passing fields that are needed to run the game
        {
            this.board = board;

            board.Draw();
            ghostManager = new GhostManager(board, random);
            pacman = null;
            pacman = new Pacman(Direction.NoDirection, new Point(PACMANSTARTX, PACMANSTARTY), Properties.Resources.pacman1right, board, random); //creates a pacman for the game
        }
        public void Restart(Board board, Random random) //Re-initalizes all the fields to restart the game.
        {
            board.Draw();
            ghostManager = new GhostManager(board, random);
            pacman = null; //sets pacman to null on restart before recreating him
            pacman = new Pacman(Direction.NoDirection, new Point(PACMANSTARTX, PACMANSTARTY), Properties.Resources.pacman1right, board, random); //create a new pacman for a game restart

            System.Media.SoundPlayer gameStart = new System.Media.SoundPlayer(Properties.Resources.pacman_beginning);
            gameStart.Play();
            Thread.Sleep(BEGIN); //plays a 4.2 second game start sound on game start
        }
        public ErrorMessage Run() //Runs the game, initializes all required components of the game board, updates the highscore file and checks for win/loss conditions to return to the form
        {
            board.Draw();

            ErrorMessage message = ErrorMessage.noError; //sets the game to have no error message until otherwise updated by other conditions while running

            StreamReader sr = new StreamReader(@"highScore.txt"); 
            highScore = Convert.ToInt16(sr.ReadLine()); //reads the current best highscore from the txt file
            sr.Close();
            if (totalScore > highScore) //if the players score is higher than the stored score value
            {
                StreamWriter sw = new StreamWriter(@"highScore.txt");   //write the players score into the highscore file so that the highscore...
                sw.WriteLine(totalScore);                               //...is updated on the fly during gameplay
                sw.Close();
            }
            if (pacman.CheckWin())  //if pacman eats all the pallets
            {
                message = ErrorMessage.pacmanWin; //return game win state error message to the form
            }


            ghostManager.DetectWalls();
            ghostManager.MoveGhosts();
            ghostManager.DrawGhosts();

            pacman.Teleport();
            pacman.DetectWall();
            pacman.Move();
            pacman.Animate();
            pacman.Eat();
            totalScore = pacman.TotalScore;

            if (ghostManager.CheckLose(pacman.Position))    //if a ghost eats pacman
            {
                pacman.Alive = false;                       //set his alive state to false
                message = ErrorMessage.pacmanEaten;         //and return game lose state to form
            }

            pacman.Draw();

            return message;     //returns applicable error message to the form on each timer tick
        }
        public void SetPacmanDirection(Direction direction) //passes the keypress from the form to pacmans class to control his movement during gameplay
        {
            pacman.Direction = direction;
        }
        public int TotalScore { get => totalScore; set => totalScore = value; }
        public int HighScore { get => highScore; set => highScore = value; }
    }
}
