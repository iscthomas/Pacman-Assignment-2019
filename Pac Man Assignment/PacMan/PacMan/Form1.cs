// Program Name:        Pacman
// Project File Name:   PacMan
// Author:              Isaac Thomas
// Date:                15/11/2019
// Language:            C#
// Platform:            Microsoft Visual Studio 2017
// Purpose:             To recreate the classic arcade game, Pacman. 
//
// Description:         Pacman is a game that involves a character called Pacman that moves around the game board, 
//                      through inputs from arrow keys by the player.
//                      Pacmans goal is to eat all the pellets on the game screen. Pacman cannot move through walls or fall off the game board. 
//                      The game board is set at the start of the game and does not change. 
//                      The ghosts will start in a holding pen and move around the screen to chase Pacman. Pacman will start outside of the holding pen.
//                      There are four ghosts that chase Pacman around the game board, with the aim of “catching” Pacman and causing him to lose the game. 
//                      The player scores points for every pallet that Pacman consumes during game play. 
//                      The game is won when the player consumes all the pellets on the game board.
//                      The game is lost when Pacman loses all of his lives from running into the ghosts. 
//
//                      Pacman is controlled by inputs from the player via the arrow keys. When the left key is pressed, Pacman moves to the left, etc.
//                      Pacman will stop moving in any given direction when he encounters a wall. 
//                      Pacman must open and close his mouth during movement to simulate him eating the pallets.
//
//                      The ghosts are controlled via random inputs where they move around the game board to catch Pacman. 
//                      The ghosts start in the holding pen in the middle of the game board and move around from that starting point.
//                      When the ghosts catch Pacman, the game is lost.
//
// Known Bugs:          The "How to play" Panel stays on screen until the 4.2 second thread sleep for the game beginning sound ends, this does not affect the functionality of the program.
//                      The highscore display is a tick behind the actual score, but this does not affect functionality and the actual score stored in the text file is correct.
// Additional Features: Pause/Resume/Restart functionality
//                      Bonus points cherry pickup
//                      Highscore system that persists through a text file outside of program
//                      Teleporting tunnels on the left and right sides of the game board
//
// Notes:               Board layout changed to 19x21 to allow for a more realistic representation of the pacman game board from the arcade game
//                      Score for pallets was changed from 1 to 10 as I felt that a 0 on the end of the score is more aesthetically pleasing and
//                      more rewarding to players to get higher scores.
//
//                      Code for embedding external fonts to a Visual Studio project referenced from: https://stackoverflow.com/questions/556147/how-to-quickly-and-easily-embed-fonts-in-winforms-app-in-c-sharp
//                      Joystix font used on form: https://www.classicgaming.cc/classics/pac-man/fonts
//
//                      Additional help from Irving and Isaac received on specific issues in my code to do with ghost movement, collision
//                      and pacman animation that I needed assistance with troubleshooting and fixing during tutorial times
//                      Advice and troubleshooting from Joy during class time was also provided

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;

namespace PacMan
{
    public partial class Form1 : Form //Form1.cs is the class that is directly associated with the form that is drawn onto the screen,
                                      //and contains the code for when the user interacts with the form to directly control the game
    {
        //code to add external font to program adapted from:
        //https://stackoverflow.com/questions/556147/how-to-quickly-and-easily-embed-fonts-in-winforms-app-in-c-sharp
        //Joystix Font retrieved from: https://www.classicgaming.cc/classics/pac-man/fonts

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
        IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();
        Font pacmanFont;

        private const int FORMHEIGHT = 640; //set the width and height of the form
        private const int FORMWIDTH = 800;

        //declare the Board object so it can be used throughout the form
        private Board board;
        private Random random;
        private Controller controller;

        public Form1() //initializes the form and the fields required to be displayed and passed to the rest of the program so the user can control the game from the form.
        {
            InitializeComponent();

            //code to add external font to program adapted from:
            //https://stackoverflow.com/questions/556147/how-to-quickly-and-easily-embed-fonts-in-winforms-app-in-c-sharp
            //Joystix Font retrieved from: https://www.classicgaming.cc/classics/pac-man/fonts

            byte[] fontData = Properties.Resources.Joystix;                                             
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);    
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Joystix.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Joystix.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            pacmanFont = new Font(fonts.Families[0], 25.0F);

            // set the Properties of the form:
            Height = FORMHEIGHT;
            Width = FORMWIDTH;
            KeyPreview = true;

            // create a Bitmap object for each image you want to display
            Bitmap p = Properties.Resources.kibble;
            Bitmap w = Properties.Resources.wallb;
            Bitmap b = Properties.Resources.blank;
            Bitmap o = Properties.Resources.blank;
            Bitmap h = Properties.Resources.cherry;

            // create an instance of a Board:
            board = new Board(p, w, b, o, h);

            // important, need to add the board object to the list of controls on the form
            Controls.Add(board);

            random = new Random();
            controller = new Controller(board, random);

            // remember the Timer Enabled Property is set to false as a default
            timer1.Enabled = false;
            timer1.Interval = 250;
        }
        private void Form1_Load(object sender, EventArgs e) //Additional method that is run on form load after initialisation, this is required to pass in the embedded font and set the correct panel visibilty for gameplay
        {
            label1.Font = pacmanFont;   //set all labels to use the font declared above
            label2.Font = pacmanFont;
            label3.Font = pacmanFont;
            label4.Font = pacmanFont;
            label5.Font = pacmanFont;
            label6.Font = pacmanFont;
            label7.Font = pacmanFont;
            label8.Font = pacmanFont;
            label9.Font = pacmanFont;
            label10.Font = pacmanFont;
            label11.Font = pacmanFont;

            panel3.Enabled = false;     //set up environment so that "how to play" is shown and end screens are hidden on launch
            panel3.Visible = false;
            panel2.Enabled = true;
            label6.Enabled = false;
            timer1.Enabled = false;
        }
        private void timer1_Tick(object sender, EventArgs e) //The game is controlled by the timer, as it executes all of the functions in this method on every tick.
        {
            label2.Text = controller.HighScore.ToString();  //updates the score labels on every timer tick
            label4.Text = controller.TotalScore.ToString();

            switch (controller.Run())
            {
                case ErrorMessage.pacmanEaten:  //code to execute on game lose state
                    {
                        timer1.Enabled = false;     //stop the game running
                        label6.Enabled = false;     //disable pause/resume label so that player cannot resume timer after game ends
                        label10.Text = "You Lose..";
                        label11.Text = "Play Again ?";
                        panel3.Enabled = true;      //enable end screen panel
                        panel3.Visible = true;
                        System.Media.SoundPlayer gameLose = new System.Media.SoundPlayer(Properties.Resources.pacman_death);
                        gameLose.Play();
                        break;
                    }
                case ErrorMessage.pacmanWin:    //code to execute on game win state
                    {
                        timer1.Enabled = false;
                        label6.Enabled = false;
                        label10.Text = "You Win !";
                        label11.Text = "Nice Work !";
                        panel3.Enabled = true;
                        panel3.Visible = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e) //Allows the form to take key inputs from the user during gameplay
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    controller.SetPacmanDirection(Direction.Left); //if the user presses the corresponding key, then send that direction to the controller using the applicable enum
                    break;

                case Keys.Right:
                    controller.SetPacmanDirection(Direction.Right);
                    break;

                case Keys.Up:
                    controller.SetPacmanDirection(Direction.Up);
                    break;

                case Keys.Down:
                    controller.SetPacmanDirection(Direction.Down);
                    break;

                default:
                    break;
            }
        }

        private void label5_Click(object sender, EventArgs e) //The “Start/Restart” button, starts the stopwatch timer for the game and toggles all the relevant buttons, panels and controls that are needed to play the game.
        {
            panel3.Enabled = false;
            panel3.Visible = false; //disable and hide all panels on the game screen
            panel2.Enabled = false;
            panel2.Visible = false;
            timer1.Enabled = true;
            controller.Restart(board, random);  //executes the controller restart method to reinitialize the game
            label5.Text = "Restart";            //changes the label from "start" to restart on the first instance of pressing the label
            label6.Enabled = true;              //enable pause/resume label
        }

        private void label6_Click(object sender, EventArgs e) //The “Pause/Resume” button, disables the timer which in-turn pauses the game and displays the how to play panel. 
        {                                                     //Disables itself on use and enables the resume button to un-pause the game.
            if (timer1.Enabled) //if timer is enabled..
            {
                timer1.Enabled = false; //..pause the timer
                panel2.Enabled = true;  //enable and show the help panel
                panel2.Visible = true;
                label6.Text = "Resume"; //and change label to "resume" to resume the game
            }
            else if (!timer1.Enabled)   //if timer is disabled
            {
                timer1.Enabled = true;  //unpause the game
                panel2.Enabled = false;
                panel2.Visible = false;
                label6.Text = "Pause";  //and switch label back to pause
            }
        }
        private void label7_Click(object sender, EventArgs e) //The “Exit” button, uses application.exit to close the form.
        {
            Application.Exit();
        }
    }
}
