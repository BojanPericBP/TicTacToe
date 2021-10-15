using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }


        #region Private Members
        /// <summary>
        /// Array with values of all cells at game
        /// </summary>
        private Marker[] markers;

        /// <summary>
        /// True if it is player1's turn (X) or 0 if it is player2's turn (O), PC is always player 2
        /// </summary>
        private bool player1Turn;
        private bool gameEnded;
        #endregion

        /// <summary>
        /// Start a new Game and init all values of board
        /// </summary>
        private void NewGame()
        {
            markers = new Marker[9];
            System.Array.Fill(markers, Marker.Free);

            player1Turn = true;
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
                gameEnded = false;

            });

        }

        /// <summary>
        /// Handle button click event
        /// </summary>
        /// <param name="sender">Button that was clicked</param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if(gameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + row * 3;

            //if cell is not free just ignore click
            if (markers[index] != Marker.Free)
                return;

            //set cell 
            markers[index] = player1Turn ? Marker.Cross : Marker.Nought;
            button.Content = player1Turn ? "X" : "O";

            if (!player1Turn)
                button.Foreground = Brushes.Red;

            //change player turn
            player1Turn = !player1Turn;

            CheckForWinner();
        }

        /// <summary>
        /// check if player win
        /// </summary>
        private void CheckForWinner()
        {

            //row 1
            if(markers[0] != Marker.Free && (markers[0] & markers[1] & markers[2]) == markers[0])
            {
                gameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                MessageBoxResult messageBox = MessageBox.Show("Igra je gotova pobjedio je neki igrac vi se dogovorite koji ^_^","Gotovo");
                return;
            }

            //row 2
            else if (markers[3] != Marker.Free && (markers[3] & markers[4] & markers[5]) == markers[3])
            {
                gameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                return;
            }

            //row 3
            else if (markers[6] != Marker.Free && (markers[6] & markers[7] & markers[8]) == markers[6])
            {
                gameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //column 1
            else if (markers[0] != Marker.Free && (markers[0] & markers[3] & markers[6]) == markers[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                return;
            }
            //column 2
            else if (markers[1] != Marker.Free && (markers[1] & markers[4] & markers[7]) == markers[1])
            {
                gameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //column 3
            else if (markers[2] != Marker.Free && (markers[2] & markers[5] & markers[8]) == markers[2])
            {
                gameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                return;
            }

            //diagonal 1
            else if (markers[0] != Marker.Free && (markers[0] & markers[4] & markers[8]) == markers[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                return;
            }

            //diagonal 2
            else if (markers[2] != Marker.Free && (markers[2] & markers[4] & markers[6]) == markers[2])
            {
                gameEnded = true;
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                return;
            }

            if (!markers.Any(e => e == Marker.Free))
            {
                gameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Yellow;
                });
                return;
            }
        }
    }
}
