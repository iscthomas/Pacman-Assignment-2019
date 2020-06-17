//The Pacman class is a child of the Player class and is used to control movement of Pacman around the game field,
// keep track of when Pacman eats a pallet or cherry, and animating Pacman during movements in game.
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
        private const int PALLETSCORE = 10;     //how many points the player receives when they eat a pallet
        private const int CHERRYSCORE = 100;    //how many points the player receives when they eat a cherry

        //fields
        private bool mouthOpen;
        private bool alive;
        private int nPallets;
        private int nCherries;
        private int totalScore;

        //constructor
        public Pacman(Direction direction, Point position, Bitmap sprite, Board board, Random random) //Initializes the fields required for the Pacman class and passes through the fields required to control Pacman on the game board.
            : base(direction, position, sprite, board, random)
        {
            alive = true; //set pacman to alive when starting a new game
            nPallets = 0; //set the score of npallets and cherries to 0 when starting a new game
            nCherries = 0;
            this.random = random; //random must be declared here otherwise a null error will be returned upon pacman eating a cherry bonus
        }

        public override void Move() //Controls the movement of Pacman around the game board, taking directional inputs from the arrow keys on the keyboard.
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        position = new Point(position.X - 1, position.Y);   //movement values are determined by changing the position of pacman by one cell in the direction specified by the enum
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
            mouthOpen = !mouthOpen; //switches the mouth open boolean on every movement to animate pacman "eating" pallets
        }

        public void Eat() //Checks when pacman is in the same position as a pallet or cherry and sets that cell of the game board to be blank while incrementing the cherry or pallets eaten up by one, 
        {                 //also calculates the total score from eating these items to pass to the controller.
            System.Media.SoundPlayer wakka = new System.Media.SoundPlayer(Properties.Resources.wakka);
            System.Media.SoundPlayer cherry = new System.Media.SoundPlayer(Properties.Resources.FruitEat);
            int i = 0;

            int currentStringPos = (position.Y * (int)eBoard.NCOLUMNS) + (position.X); //converts current position of pacman in the game board string to an integer value

            if (board.Map.Substring(currentStringPos, 1) == "p")                        //if pacmans current pos on the game board is a pallet...
            {
                board.Map = board.Map.Substring(0, currentStringPos) + "b" + board.Map.Substring(currentStringPos + 1); //..change the cell from a pallet to a blank...
                nPallets++;                                                                                             //... and increment the pallets eaten by 1
                wakka.Play();
            }
            if (board.Map.Substring(currentStringPos, 1) == "h")    //if pacmans current pos on the game board is a cherry...
            {
                board.Map = board.Map.Substring(0, currentStringPos) + "b" + board.Map.Substring(currentStringPos + 1); //..change the cell from a cherry to a blank...
                nCherries++;                                                                                            //... and increment the pallets eaten by 1..
                cherry.Play();

                do
                {
                    i = random.Next((int)eBoard.NCOLUMNS * (int)eBoard.NROWS);                      //..then generate a random position on the game board

                    if (board.Map.Substring(i, 1) == "b")                                           //if that position is a blank cell
                    {
                        board.Map = board.Map.Substring(0, i) + "h" + board.Map.Substring(i + 1);   //then set the cherries position to that cell
                    }
                } while (board.Map.Substring(i, 1) != "h");                                         //otherwise keep repeating (do...while) until a blank cell is found on the board.
            }
            totalScore = (nPallets * PALLETSCORE) + (nCherries * CHERRYSCORE);                      //total score = pallets eaten + cherries eaten multiplied by the score value constants.
        }
        public bool CheckWin() //Compares the amount of pallets eaten to the total number of pallets on the gameboard, the returns the win Boolean when the player has eaten all pallets on the game board.
        {
            bool winner = false;
            if (nPallets == (int)eBoard.NPALLETS)   //if the amount of pallets eaten match the enum value of total pallets then return true
            {
                winner = true;
            }
            return winner;
        }
        public void Animate() //Animates pacman using multiple bitmap images to simulate an eating motion as Pacman moves around the game board and a Boolean located in the Move method to switch between open and closed.
        {
            switch (Direction)
            {
                case Direction.Up:      //if paceman is moving up
                    {
                        if (mouthOpen)  //and the boolean for mouth open is set to true
                        {
                            sprite = Properties.Resources.pacman1up;    //then use image 1 for pacman to close his mouth
                        }
                        else
                        {
                            sprite = Properties.Resources.pacman2up;    //otherwise open his mouth if the boolean is set to false (closed mouth)
                        }
                        break;
                    }

                case Direction.Down:
                    {
                        if (mouthOpen)
                        {
                            sprite = Properties.Resources.pacman1down;
                        }
                        else
                        {
                            sprite = Properties.Resources.pacman2down;
                        }
                        break;
                    }
                case Direction.Left:
                    {
                        if (mouthOpen)
                        {
                            sprite = Properties.Resources.pacman1left;
                        }
                        else
                        {
                            sprite = Properties.Resources.pacman2left;
                        }
                        break;
                    }
                case Direction.Right:
                    {
                        if (mouthOpen)
                        {
                            sprite = Properties.Resources.pacman1right;
                        }
                        else
                        {
                            sprite = Properties.Resources.pacman2right;
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
        }
        
        public bool Alive { get => alive; set => alive = value; }
        public int TotalScore { get => totalScore; set => totalScore = value; }
    }
}
