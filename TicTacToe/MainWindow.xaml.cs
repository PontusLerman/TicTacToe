using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe.Logic;
using TicTacToe.Models;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SolidColorBrush currentPlayer = Brushes.Green;
        PlayerLogic playerLogic = new PlayerLogic();
        GameLogic gameLogic;

        public MainWindow()
        {
            InitializeComponent();
            gameLogic = new GameLogic(this);
            ScoreBlock.Text = gameLogic.UpdateScoreBlock();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (clickedButton.Background == Brushes.Green || clickedButton.Background == Brushes.Red)
                return;

            gameLogic.PlayersTurn(clickedButton, currentPlayer);
            clickedButton.Background = currentPlayer;

            ScoreBlock.Text = gameLogic.CheckIfWinnerOrDraw(currentPlayer);

            currentPlayer = playerLogic.ChangePlayer(currentPlayer);

            gameLogic.ComputersTurn(currentPlayer);

            ScoreBlock.Text = gameLogic.CheckIfWinnerOrDraw(currentPlayer);

            currentPlayer = playerLogic.ChangePlayer(currentPlayer);
        }

        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            ResetBoard();
        }

        public Button? FindButton(int row, int column)
        {
            foreach (UIElement button in BoardGrid.Children)
            {
                if (button is Button)
                {
                    if (Grid.GetRow(button) == row && Grid.GetColumn(button) == column)
                    {
                        return (Button)button;
                    }
                }
            }

            return null;
        }

        public void ResetBoard()
        {
            gameLogic.board = new string[3, 3];

            foreach (UIElement element in BoardGrid.Children)
            {
                if (element is Button button)
                {
                    button.Background = Brushes.LightGray;
                }
            }
        }
    }
}