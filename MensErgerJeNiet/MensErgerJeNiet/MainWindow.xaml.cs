using MensErgerJeNiet.ModelView;
using MensErgerJeNiet.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
        private ModelView.Game theGame;
        private PreGameScreenv2 pgs;
        private PreGameScreen pg;
        private Model.Spawn[] spawns;

        public MainWindow()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
            theGame = new Game(this);
            dice.MouseLeftButtonUp += Button_Click;
            rollButton.IsEnabled = false;
        }

        private void colorEllipses()
        {

            //color all the spawns
            int i = 0;
            if(spawns!=null)
            while (i < spawns.Length)
            {
                if (spawns[i].fieldCode.StartsWith("p1") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.LawnGreen);
                }
                else if(spawns[i].fieldCode.StartsWith("p1") && spawns[i].pawn == null) 
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Green);
                }
                else if (spawns[i].fieldCode.StartsWith("p2") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Red);
                }
                else if (spawns[i].fieldCode.StartsWith("p2") && spawns[i].pawn == null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.DarkRed);
                }
                else if (spawns[i].fieldCode.StartsWith("p3") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Blue);
                }
                else if (spawns[i].fieldCode.StartsWith("p3") && spawns[i].pawn == null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.DarkBlue);
                }
                else if (spawns[i].fieldCode.StartsWith("p4") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Yellow);
                }
                else if (spawns[i].fieldCode.StartsWith("p4") && spawns[i].pawn == null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Goldenrod);
                }
                i++;
            }
        }

        public void fillField(String fieldCode, Color col)
        {
            colorEllipses();
            getFieldEllipse(fieldCode).Fill = new SolidColorBrush(col);
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void startGame(int players, int humans)
        {
            ALEX_STARTUP(players, humans);



        }

        private void ALEX_STARTUP(int players, int humans)
        {
            theGame.startGame(players, humans);
           
            spawns = theGame.board.Spawns;
            colorEllipses();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = new string[8];
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\"; //fix
            dialog.DefaultExt = "MEJN files |*.mejn";
            dialog.Title = "Mens Erger Je Niet Loadgame";

            if(dialog.ShowDialog() == DialogResult)
            {
                StreamReader stream = new StreamReader(dialog.FileName);
                string line;

                for (int i = 0; i < 8; i++)
                {
                    line = stream.ReadLine();
                    lines[i] = line;
                    Console.WriteLine(line);
                }
               
            }



         //   throw new NotImplementedException();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void enableRollButton()
        {
            rollButton.IsEnabled = true;
        }

        public void changeDice(int value)
        {
            theGame.diceRoll = value;
            System.Reflection.Assembly thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            string path = thisExe.Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string folderName = dirInfo.Parent.FullName;
            Uri uri = new Uri(folderName + "/Image" + value + ".jpg");
            BitmapImage img = new BitmapImage(uri);
            dice.Source = img;
            SoundPlayer player = new SoundPlayer(Properties.Resources.dice_throw);
            player.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (theGame.playersTurn.isHuman)
            {
                changeDice(theGame.rollDice());
                theGame.playerPrep(theGame.playersTurn);
            }
        }

        public void changePlayerTurn(String c)
        {
            switch (c)
            {
                case PlayerColor.GREEN:
                    playerturn.Content = "Player 1";
                    break;
                case PlayerColor.RED:
                    playerturn.Content = "Player 2";
                    break;
                case PlayerColor.BLUE:
                    playerturn.Content = "Player 3";
                    break;
                case PlayerColor.YELLOW:
                    playerturn.Content = "Player 4";
                    break;
            }
        }

        private Ellipse getFieldEllipse(String field)
        {
            switch (field)
            {
                case "p1spawn1":
                    return p1spawn1;
                case "p1spawn2":
                    return p1spawn2;
                case "p1spawn3":
                    return p1spawn3;
                case "p1spawn4":
                    return p1spawn4;
                case "p2spawn1":
                    return p2spawn1;
                case "p2spawn2":
                    return p2spawn2;
                case "p2spawn3":
                    return p2spawn3;
                case "p2spawn4":
                    return p2spawn4;
                case "p3spawn1":
                    return p3spawn1;
                case "p3spawn2":
                    return p3spawn2;
                case "p3spawn3":
                    return p3spawn3;
                case "p3spawn4":
                    return p3spawn4;
                case "p4spawn1":
                    return p4spawn1;
                case "p4spawn2":
                    return p4spawn2;
                case "p4spawn3":
                    return p4spawn3;
                case "p4spawn4":
                    return p4spawn4;
                case "field1":
                    return field1;
                case "field2":
                    return field2;
                case "field3":
                    return field3;
                case "field4":
                    return field4;
                case "field5":
                    return field5;
                case "field6":
                    return field6;
                case "field7":
                    return field7;
                case "field8":
                    return field8;
                case "field9":
                    return field9;
                case "field10":
                    return field10;
                case "field11":
                    return field11;
                case "field12":
                    return field12;
                case "field13":
                    return field13;
                case "field14":
                    return field14;
                case "field15":
                    return field15;
                case "field16":
                    return field16;
                case "field17":
                    return field17;
                case "field18":
                    return field18;
                case "field19":
                    return field19;
                case "field20":
                    return field20;
                case "field21":
                    return field21;
                case "field22":
                    return field22;
                case "field23":
                    return field23;
                case "field24":
                    return field24;
                case "field25":
                    return field25;
                case "field26":
                    return field26;
                case "field27":
                    return field27;
                case "field28":
                    return field28;
                case "field29":
                    return field29;
                case "field30":
                    return field30;
                case "field31":
                    return field31;
                case "field32":
                    return field32;
                case "p1end1":
                    return p1end1;
                case "p1end2":
                    return p1end2;
                case "p1end3":
                    return p1end3;
                case "p1end4":
                    return p1end4;
                case "p2end1":
                    return p2end1;
                case "p2end2":
                    return p2end2;
                case "p2end3":
                    return p2end3;
                case "p2end4":
                    return p2end4;
                case "p3end1":
                    return p3end1;
                case "p3end2":
                    return p3end2;
                case "p3end3":
                    return p3end3;
                case "p3end4":
                    return p3end4;
                case "p4end1":
                    return p4end1;
                case "p4end2":
                    return p4end2;
                case "p4end3":
                    return p4end3;
                case "p4end4":
                    return p4end4;
                default:
                    return new Ellipse();
            }
        }

        public void showEndMessage()
        {
            MessageBox.Show("The game has ended!");//to be edited
        }

        private void handleClick(object sender, MouseButtonEventArgs e)
        {
            Ellipse clicked = (Ellipse)sender;
            theGame.recieveClickedEllipse(clicked.Name);
        }
    }
}
