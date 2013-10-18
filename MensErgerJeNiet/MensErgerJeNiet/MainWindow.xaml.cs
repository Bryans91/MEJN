using MensErgerJeNiet.ModelView;
using MensErgerJeNiet.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MensErgerJeNiet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game theGame = new Game();
        public MainWindow()
        {
            InitializeComponent();
            PreGameScreen pgs = new PreGameScreen(this);
        }

        public void startGame(int players, int humans)
        {
            theGame.startGame(players, humans);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
