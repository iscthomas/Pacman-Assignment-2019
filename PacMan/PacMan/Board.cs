//The board class is used to determine the layout of the game board, the number of items on the board (kibble, cherries etc) and draw the game board to the screen. 
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public class Board : DataGridView //Defines the fields and specifies the size and layout of the game board using a string constant of characters that plot the brick and pallet positions on the game board using the DataGridView
    {
        //constants
        private const int CELLSIZE = 27;
        private const int SPACESIZE = 4;

        private const string INITMAP = "wwwwwwwwwwwwwwwwwww" +
                                       "wppppppppwppppppppw" +
                                       "wpwwpwwwpwpwwwpwwpw" +
                                       "wpwwpwwwpwpwwwpwwpw" +
                                       "wpppppppppppppppppw" +
                                       "wpwwpwpwwwwwpwpwwpw" +
                                       "wppppwpppwpppwppppw" +
                                       "wwwwpwwwbwbwwwpwwww" +
                                       "ooowpwbbbbbbbwpwooo" +
                                       "wwwwpwbwbbbwbwpwwww" +
                                       "bbbbpbbwbbbwbbpbbbb" +
                                       "wwwwpwbwwwwwbwpwwww" +
                                       "ooowpwbbbhbbbwpwooo" +
                                       "wwwwpwbwwwwwbwpwwww" +
                                       "wppppppppwppppppppw" +
                                       "wpwwpwwwpwpwwwpwwpw" +
                                       "wppwpppppbpppppwppw" +
                                       "wwpwpwpwwwwwpwpwpww" +
                                       "wppppwpppwpppwppppw" +
                                       "wpwwwwwwpwpwwwwwwpw" +
                                       "wpppppppppppppppppw" +
                                       "wwwwwwwwwwwwwwwwwww";

        //fields
        private string map;
        private Bitmap wall;
        private Bitmap pallet;
        private Bitmap blank;
        private Bitmap blankO; //a second field for blank is required so that the cherry bonus does not appear outside the bounds of the game
        private Bitmap cherry;

        //constructor
        public Board(Bitmap p, Bitmap w, Bitmap b, Bitmap o, Bitmap h) //Initializes the fields and set up the string layout of the game board to be drawn to the screen as bitmap images
            : base()
        {
            //initialise fields
            map = INITMAP;
            wall = w;
            pallet = p;
            blank = b;
            blankO = o; 
            cherry = h;

            // set position of Board on the Form
            Top = 0;
            Left = 0;
            CellBorderStyle = DataGridViewCellBorderStyle.None;

            // setup the columns to display images. We want to display images, so we set 5 columns worth of Image columns
            for (int x = 0; x < (int)eBoard.NCOLUMNS; x++)
            {
                Columns.Add(new DataGridViewImageColumn());
            }
            // then we can tell the grid the number of rows we want to display
            RowCount = (int)eBoard.NROWS;

            // set the properties of the Board(which is a DataGridView object)
            Height = (int)eBoard.NROWS * CELLSIZE + SPACESIZE;
            Width = (int)eBoard.NCOLUMNS * CELLSIZE + SPACESIZE;
            ScrollBars = ScrollBars.None;
            ColumnHeadersVisible = false;
            RowHeadersVisible = false;

            // set size of cells:
            foreach (DataGridViewRow r in this.Rows)
                r.Height = CELLSIZE;

            foreach (DataGridViewColumn c in this.Columns)
                c.Width = CELLSIZE;

            // rows and columns should never resize themselves to fit cell contents
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // prevent user from resizing rows or columns
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
        }

        //to draw the Board, the string character is used to load the corresponding image into the DataGridView cell
        public void Draw() //Draws the game board to the screen using the specified parameters above.
        {
            int totalCells = (int)eBoard.NROWS * (int)eBoard.NCOLUMNS;

            for (int i = 0; i < totalCells; i++)
            {
                int nRow = i / (int)eBoard.NCOLUMNS;
                int nColumn = i % (int)eBoard.NCOLUMNS;

                switch (map.Substring(i, 1))
                {
                    case "w":
                        Rows[nRow].Cells[nColumn].Value = wall;
                        break;
                    case "p":
                        Rows[nRow].Cells[nColumn].Value = pallet;
                        break;
                    case "b":
                        Rows[nRow].Cells[nColumn].Value = blank;
                        break;
                    case "o":
                        Rows[nRow].Cells[nColumn].Value = blank;
                        break;
                    case "h":
                        Rows[nRow].Cells[nColumn].Value = cherry;
                        break;
                    default:
                        MessageBox.Show("Unidentified value in string");
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // Board
            // 
            this.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        public string Map { get => map; set => map = value; }
    }
}
