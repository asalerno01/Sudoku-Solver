using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class SudokuSolver
    {
        public static void Main()
        {
            List<List<char>> board = new()
            {
                new() { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new() { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new() { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new() { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new() { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new() { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new() { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new() { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new() { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };
            List<char> sudokuDigits = new()
            {
                '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            List<List<List<char>>> newBoard = new();

            for (int i = 0; i < board.Count; i++)
            {
                List<List<char>> newRow = new();
                for (int j = 0; j < board[i].Count; j++)
                {
                    if (board[i][j] != '.')
                    {
                        newRow.Add(new()
                        {
                            board[i][j]
                        });
                    }
                    else
                    {
                        List<char> x = board[i];
                        List<char> thisRowMissingDigits = sudokuDigits.Where(r => !board[i].Contains(r)).ToList();
                        List<char> thisColumnMissingDigits = new();
                        List<char> thisColumnCurrentDigits = new();
                        for (int k = 0; k < board.Count; k++)
                        {
                            if (board[k][j] != '.') thisColumnCurrentDigits.Add(board[k][j]);
                        }
                        thisColumnMissingDigits = sudokuDigits.Where(r => !thisColumnCurrentDigits.Contains(r)).ToList();
                        newRow.Add(thisRowMissingDigits.Where(r => thisColumnMissingDigits.Contains(r)).ToList());
                    }
                }
                newBoard.Add(newRow);
            }
            PrintBoard(newBoard);

            var tempNewBoard = newBoard;
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    List<char> chars = new();
                    for (int x = i; x < i + 3; x++)
                    {
                        for (int y = j; y < j + 3; y++)
                        {
                            if (newBoard[x][y].Count == 1)
                            {
                                chars.Add(newBoard[x][y][0]);
                            }
                        }
                    }
                    for (int x = i; x < i + 3; x++)
                    {
                        for (int y = j; y < j + 3; y++)
                        {
                            if (newBoard[x][y].Count > 1) newBoard[x][y] = tempNewBoard[x][y].Where(r => !chars.Contains(r)).ToList();
                        }
                    }
                }
            }
            PrintBoard(newBoard);
        }
        public static void PrintBoard(List<List<List<char>>> board)
        {
            foreach (List<List<char>> row in board)
            {
                Console.Write("[ ");
                foreach (List<char> column in row)
                {
                    Console.Write("[ ");
                    string spacer = "";
                    foreach (char c in column)
                    {
                        Console.Write($"{spacer}{c}");
                        spacer = ", ";
                    }
                    Console.Write(" ], ");
                }
                Console.Write(" ]\n");
            }
            Console.WriteLine();
        }
    }
}