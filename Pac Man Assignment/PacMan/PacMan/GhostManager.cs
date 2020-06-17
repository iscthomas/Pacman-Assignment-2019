//The GhostManager class is used to create a list of ghosts that are drawn to the form individually, run from the Ghost class.
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class GhostManager
    {
        //constants
        private const int GHOSTSTARTX = 8;      //Constants that store the starting positions for the ghosts are here..
        private const int GHOSTSTARTXY = 9;     //..the ghosts all start in the holding pen in this configuration 
        private const int GHOSTSTARTY = 10;     
        //fields
        private List<Ghost> ghosts; //list stores the array of ghosts

        //constructor
        public GhostManager(Board board, Random random) //Initializes the fields required for the Ghost Manager class
        {
            ghosts = new List<Ghost>();

            ghosts.Add(new Ghost(Direction.NoDirection, new Point(GHOSTSTARTX, GHOSTSTARTXY), Properties.Resources.green1, board, random)); //create a new ghost in the Ghosts list
            ghosts.Add(new Ghost(Direction.NoDirection, new Point(GHOSTSTARTX, GHOSTSTARTY), Properties.Resources.purple1, board, random));
            ghosts.Add(new Ghost(Direction.NoDirection, new Point(GHOSTSTARTY, GHOSTSTARTXY), Properties.Resources.orange1, board, random));
            ghosts.Add(new Ghost(Direction.NoDirection, new Point(GHOSTSTARTY, GHOSTSTARTY), Properties.Resources.red1, board, random));
        }

        public void DrawGhosts() //Draws the ghosts to the screen using bitmap images 
        {
            foreach (Ghost ghost in ghosts)
            {
                ghost.Draw();
            }
        }
        public void DetectWalls() //Executes the Teleport and Wall detection methods for each ghost per timer tick
        {
            foreach (Ghost ghost in ghosts)
            {
                ghost.Teleport();
                ghost.DetectWall();
            }
        }
        public void MoveGhosts() //Executes the Move method for each ghost per timer tick
        {
            foreach (Ghost ghost in ghosts)
            {
                ghost.Move();
            }
        }

        public bool CheckLose(Point pacmanPosition) //Checks if a ghost has eaten pacman on every timer tick and returns the value to the controller
        {
            bool lose = false;
            foreach (Ghost ghost in ghosts)
            {
                if (ghost.Eat(pacmanPosition))
                {
                    lose = true;
                }
            }
            return lose;
        }

        public List<Ghost> Ghosts { get => ghosts; set => ghosts = value; }
    }
}
