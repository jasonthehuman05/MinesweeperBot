using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    internal class Minesweeper
    {
        ///<summary>
        ///This class is the host of all things related to making the actual game board.
        /// </summary>

        public long width;
        public long height;


        private string bomb = "💣";
        private string[] numbers = { ":orange_square:",":one:",":two:",":three:",":four:",":five:",":six:",":seven:",":eight:",":nine:",":keycap_ten:"};
        public Minesweeper(long width = 5, long height = 5)
        {
            this.width = width;
            this.height = height;
        }

        public string GenerateBoard()
        {
            int mineCount = (int)(Math.Sqrt(width*height));
            Console.WriteLine(mineCount);

            string[,] gameBoard = new string[width, height];

            Random random = new Random();

            for(int i = 0; i < mineCount; i++)
            {
                int x = random.Next(0, (int)width);
                int y = random.Next(0, (int)height);

                gameBoard[x, y] = "💣";
            }

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    //For each cell, count the number of neighbours that are bombs
                    int cellBombCount = 0;

                    if (gameBoard[x,y] == bomb) { continue; } //Skip if current cell is a bomb

                    else
                    {
                        //Is not a bomb, check neighbouring cells
                        for(int offsetX = -1; offsetX < 2; offsetX++)
                        {
                            for (int offsetY = -1; offsetY < 2; offsetY++)
                            {
                                if(offsetX == 0 && offsetY == 0) { continue; }//Is current cell, skip
                                
                                if(x + offsetX < 0 || x + offsetX >= width) { continue; }
                                if (y + offsetY < 0 || y + offsetY >= height) { continue; }

                                if (gameBoard[x+offsetX, y+offsetY] == bomb) { cellBombCount++; }
                            }
                        }
                    }
                    //All cells checked. insert correct symbol
                    gameBoard[x, y] = numbers[cellBombCount];
                }
            }

            //Convert array to a string that Discord can handle
            string board = "";
            for(int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    board += $"||{gameBoard[x, y]}||";
                    Console.WriteLine(gameBoard[x, y]);
                }
                if (y + 1 != height) { board += "\n"; }
            }
            board += "completed board!";
            if(board.Length > 2000)
            {
                board = $"Board too large to send! (size is {board.Length}, maximum is 2000)";
            }
            return board;
        }
    }
}
