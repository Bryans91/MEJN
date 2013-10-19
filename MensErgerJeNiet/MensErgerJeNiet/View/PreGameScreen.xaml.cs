using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MensErgerJeNiet.View
{
    /// <summary>
    /// Interaction logic for PreGameScreen.xaml
    /// </summary>
    public partial class PreGameScreen : Window
    {
        int players, humans;
        MainWindow main;

        public PreGameScreen(MainWindow m)
        {
            InitializeComponent();
            main = m;
            this.Topmost = true;
            this.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (humans != null || players != null)
            {
                if (humans > players)
                {
                    MessageBox.Show("The number of humans is higher than the number of players.");
                }
                else if (players <= 1 || players > 4)
                {
                    MessageBox.Show("The number of total players is not between 2 and 4 players. You can't play a game like this. Silly you!");
                }
                else if (humans == 0)
                {
                    MessageBox.Show("You are currently playing with less than 1 human player.");
                }
                else if (humans < players && players > 2 && players < 5 || humans == players && players > 2 && players < 5)
                {
                    main.startGame(players, humans);
                    this.Topmost = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("You haven't filled in all the blanks!");
            }
        }

        private void n_ofPlayers_TextChanged(object sender, TextChangedEventArgs e)
        {
            String temp = n_ofPlayers.Text;
            if(!Convert.ToInt32(temp).Equals(null))
                players = Convert.ToInt32(temp);
            else
                MessageBox.Show("This is not a number!");
        }

        private void n_ofHumans_TextChanged(object sender, TextChangedEventArgs e)
        {
            String temp = n_ofHumans.Text;
            if (!Convert.ToInt32(temp).Equals(null))
                humans = Convert.ToInt32(temp);
            else
                MessageBox.Show("This is not a number!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
