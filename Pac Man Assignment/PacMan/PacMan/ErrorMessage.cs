//The ErrorMessage class is an enumeration that is used to carry error messages that are references 
// by the rest of the program for detecting game states such as wins and losses.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public enum ErrorMessage ////Enumerations that contains the error messages used for detection of game win and loss states
    {
        pacmanEaten,
        pacmanWin,
        noError //required to return no error when the game is not in a win or loss state
    }
}
