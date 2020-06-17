using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private const int FORMHEIGHT = 600;
        private const int FORMWIDTH = 800; 
        
        private Grid grid;
        private Random random;
        private Controller controller;

        public Form1()
        {
            InitializeComponent();

            // set the Properties of the form
            Top = 0;
            Left = 0;
            Height = FORMHEIGHT;
            Width = FORMWIDTH;
            KeyPreview = true;

            random = new Random();
            grid = new Grid(Properties.Resources.blank);
            // important, need to add the grid object to the list of controls on the form
            Controls.Add(grid);

            controller = new Controller(grid, random);
            controller.StartNewGame();

            // remember the Timer Enabled Property is set to false as a default
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (controller.PlayGame())
            {
                case ErrorMessage.snakeHitSelf:
                {
                        timer1.Enabled = false;
                        MessageBox.Show("You Lost!");
                        break;
                }

                case ErrorMessage.snakeHitWall:
                {
                        timer1.Enabled = false;
                        MessageBox.Show("You Lost!");
                        break;
                }

                case ErrorMessage.snakeEatenFrog:
                {
                        textBox1.Text = Convert.ToString(Convert.ToInt16(textBox1.Text) + 1);
                        break;
                }

                default:
                {
                    break;
                }
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            timer1.Enabled = true;
            controller.StartNewGame();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = ! timer1.Enabled;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    controller.SetSnakeDirection(Direction.Left);
                    break;

                case Keys.Right:
                    controller.SetSnakeDirection(Direction.Right);
                    break;

                case Keys.Up:
                    controller.SetSnakeDirection(Direction.Up);
                    break;

                case Keys.Down:
                    controller.SetSnakeDirection(Direction.Down);
                    break;

                default:
                    break;
            }
        }
    }
}
  