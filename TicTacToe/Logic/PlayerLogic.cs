using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TicTacToe.Logic
{
    public class PlayerLogic
    {
        public SolidColorBrush ChangePlayer(SolidColorBrush currentPlayer)
        {
            if (currentPlayer == Brushes.Green)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.Green;
            }
        }
    }
}
