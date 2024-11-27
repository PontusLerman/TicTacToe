using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using TicTacToe.Models;

namespace TicTacToe.Logic
{
    public class GameLogic
    {
        public string[,] board { get; set; } = new string[3, 3];

        int playerWinns = 0;
        int computerWinns = 0;

        private Random randomNumbers = new Random();
        private readonly MainWindow mainWindow;

        public GameLogic(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        public string UpdateScoreBlock()
        {
            return $"Player winns: {playerWinns} \nComputer winns: {computerWinns}";
        }

        public void PlayersTurn(Button clickedButton, SolidColorBrush currentPlayer)
        {
            var buttonIndex = new ButtonPosition()
            {
                x = Grid.GetRow(clickedButton),
                y = Grid.GetColumn(clickedButton)
            };

            board[buttonIndex.x, buttonIndex.y] = currentPlayer.ToString();
        }

        public void ComputersTurn(SolidColorBrush currentPlayer)
        {
            var randomX = randomNumbers.Next(3);
            var randomY = randomNumbers.Next(3);

            if (board[randomX, randomY] == Brushes.Green.ToString() || board[randomX, randomY] == Brushes.Red.ToString())
            {
                ComputersTurn(currentPlayer);
            }
            else
            {
                board[randomX, randomY] = Brushes.Red.ToString();

                var button = mainWindow.FindButton(randomX, randomY);

                button.Background = currentPlayer;

                CheckIfWinnerOrDraw(currentPlayer);
            }
        }

        public string CheckIfWinnerOrDraw(SolidColorBrush currentPlayer)
        {
            if (CheckWinner())
            {
                string winner = string.Empty;

                if (currentPlayer.ToString() == "#FF008000")
                {
                    playerWinns++;              
                    winner = "Player";

                }
                else if (currentPlayer.ToString() == "#FFFF0000")
                {
                    computerWinns++;
                    winner = "Computer";

                }

                MessageBox.Show($"{winner} wins!");

                mainWindow.ResetBoard();

                return UpdateScoreBlock();

            }
            else if (CheckDraw())
            {
                MessageBox.Show("It's a draw!");

                mainWindow.ResetBoard();

                return UpdateScoreBlock();
            }

            return UpdateScoreBlock();
        }

        private bool CheckWinner()
        {
            //Rows
            for (int i = 0; i < 3; i++)
            {
                if (!String.IsNullOrWhiteSpace(board[i, 0]))
                {
                    if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2])
                    {
                        return true;
                    }
                }
            }

            //Columns
            for (int i = 0; i < 3; i++)
            {
                if (!String.IsNullOrWhiteSpace(board[0, i]))
                {
                    if (board[0, i] == board[1, i] && board[0, i] == board[2, i])
                    {
                        return true;
                    }
                }
            }

            //Diagonals
            if (!String.IsNullOrWhiteSpace(board[1, 1]))
            {
                if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                {
                    return true;
                }
                if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckDraw()
        {
            foreach (var cell in board)
            {
                if (String.IsNullOrWhiteSpace(cell))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
