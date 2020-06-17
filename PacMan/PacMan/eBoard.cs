// The eBoard class is an enumeration that is used to store the values of the board parameters so that they can be easily changed if required.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public enum eBoard //Contains the enumerations of the parameters for the game board, such as the columns and rows, 
                       //the locations of the teleport tunnels and the total number of pallets on the game board
    {
        NCOLUMNS = 19,  //amount of columns in the game board
        NROWS = 22,     //amount of rows in the game board
        TELEPORTX = 18, //x pos of the teleportation cell
        TELEPORTY = 10, //y pose of the teleportation cell
        NPALLETS = 156, //number of pallets in the game board, number is calculated by counting all of the "p" characters 
                        //in the string constant from Board.cs using a letter count externally.
    }
}

