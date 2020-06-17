//The eBoard class is an enumeration that is used to store the directional parameters so that they can be easily referenced by other classes.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public enum Direction : byte //Contains the enumerations for the directions of movement for Pacman and the ghosts.
    {
        Up,
        Down,
        Left,
        Right,
        NoDirection, //no direction is required to halt movement of pacman and the ghosts when they hit a wall
    }
}
