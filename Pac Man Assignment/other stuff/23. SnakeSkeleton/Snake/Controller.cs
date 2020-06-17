using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SnakeGame
{
    public class Controller
    {
        private Grid grid;
        private Random random;
        private Snake snake;
        private Frog frog;


        public Controller(Grid grid, Random random)
        {
            this.grid = grid;
            this.random = random;
            snake = new Snake(Properties.Resources.snakeEyes, grid, Properties.Resources.snakeSkin);
            frog = new Frog(Properties.Resources.frog, grid);
        }

        public void StartNewGame()
        {
            grid.Draw();
            snake = null;
            snake = new Snake(Properties.Resources.snakeEyes, grid, Properties.Resources.snakeSkin);

        }
        
        public ErrorMessage PlayGame()
        {
            if (!frog.Alive)
            {
                frog.Position = findFreeCell();
                frog.Alive = true;
            }

            grid.Draw();
            frog.Draw();
            snake.Draw();
            snake.Move();

            ErrorMessage message = ErrorMessage.noError;

            if (snake.HitWall() == true)
            {
                message = ErrorMessage.snakeHitWall;
            }

            if (snake.HitSelf() == true)
            {
                message = ErrorMessage.snakeHitSelf;
            }

            if(snake.Eat(frog.Position))
            {
                frog.Alive = false;
                snake.Grow();
                message = ErrorMessage.snakeEatenFrog;
            }
            return message; 
        }

        private Point findFreeCell()
        {
            Point target = Point.Empty;

            while (target == Point.Empty)
            {
                int i = random.Next(30);
                int j = random.Next(30);

                if (grid.Rows[i].Cells[j].Value == grid.Blank)
                {
                    target = new Point(i, j);
                }
            }
            return target;
        }

        public void SetSnakeDirection(Direction direction)
        {
            snake.Direction = direction;
        }
  }
}
