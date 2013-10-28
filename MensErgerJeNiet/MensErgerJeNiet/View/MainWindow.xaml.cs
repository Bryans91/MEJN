using MensErgerJeNiet.Model;
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
        private Spawn[] spawns = new Spawn[16];
        private PreGameScreen pgs;

        public MainWindow(PreGameScreen pregs)
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
            TheGame = new Game(this);
            dice.MouseLeftButtonUp += Button_Click;
            rollButton.IsEnabled = false;
            pgs = pregs;
        }

        public void colorEllipses(Player[] players)
        {
            int g = 0;
            foreach(Player p in players) 
            {
                if(p!=null)
                    foreach(Spawn sp in p.spawns) 
                    {
                        spawns[g] = sp;
                        g++;
                    }
            }
            //color all the spawns
            int i = 0;
            if(spawns!=null)
            while (i < spawns.Length)
            {
                if (spawns[i].fieldCode.StartsWith("pgreen") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.LawnGreen);
                }
                else if (spawns[i].fieldCode.StartsWith("pgreen") && spawns[i].pawn == null) 
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Green);
                }
                else if (spawns[i].fieldCode.StartsWith("pred") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Red);
                }
                else if (spawns[i].fieldCode.StartsWith("pred") && spawns[i].pawn == null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.DarkRed);
                }
                else if (spawns[i].fieldCode.StartsWith("pblue") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Blue);
                }
                else if (spawns[i].fieldCode.StartsWith("pblue") && spawns[i].pawn == null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.DarkBlue);
                }
                else if (spawns[i].fieldCode.StartsWith("pyellow") && spawns[i].pawn != null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Yellow);
                }
                else if (spawns[i].fieldCode.StartsWith("pyellow") && spawns[i].pawn == null)
                {
                    getFieldEllipse(spawns[i].fieldCode).Fill = new SolidColorBrush(Colors.Goldenrod);
                }
                i++;
            }
        }

        public void fillField(String fieldCode, Color col)
        {
            getFieldEllipse(fieldCode).Fill = new SolidColorBrush(col);
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select a Text File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == true)
            {
                
                // Assign the cursor in the Stream to the Form's Cursor property.
                System.IO.StreamReader sr = new
                System.IO.StreamReader(openFileDialog1.FileName);
                List<string> lines = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);
                string[] strarray = lines.ToArray();
                pgs.Main.TheGame = null;
                pgs.Main.TheGame = new Game(pgs.Main);
                pgs.Main.TheGame.startFromFile(strarray);
                sr.Close();
            }
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
                case "pgreenspawn1":
                    return pgreenspawn1;
                case "pgreenspawn2":
                    return pgreenspawn2;
                case "pgreenspawn3":
                    return pgreenspawn3;
                case "pgreenspawn0":
                    return pgreenspawn0;
                case "predspawn1":
                    return predspawn1;
                case "predspawn2":
                    return predspawn2;
                case "predspawn3":
                    return predspawn3;
                case "predspawn0":
                    return predspawn0;
                case "pbluespawn1":
                    return pbluespawn1;
                case "pbluespawn2":
                    return pbluespawn2;
                case "pbluespawn3":
                    return pbluespawn3;
                case "pbluespawn0":
                    return pbluespawn0;
                case "pyellowspawn1":
                    return pyellowspawn1;
                case "pyellowspawn2":
                    return pyellowspawn2;
                case "pyellowspawn3":
                    return pyellowspawn3;
                case "pyellowspawn0":
                    return pyellowspawn0;
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
                case "field33":
                    return field33;
                case "field34":
                    return field34;
                case "field35":
                    return field35;
                case "field36":
                    return field36;
                case "field37":
                    return field37;
                case "field38":
                    return field38;
                case "field39":
                    return field39;
                case "field0":
                    return field0;
                case "goalgreen1":
                    return goalgreen1;
                case "goalgreen2":
                    return goalgreen2;
                case "goalgreen3":
                    return goalgreen3;
                case "goalgreen0":
                    return goalgreen0;
                case "goalred1":
                    return goalred1;
                case "goalred2":
                    return goalred2;
                case "goalred3":
                    return goalred3;
                case "goalred0":
                    return goalred0;
                case "goalblue1":
                    return goalblue1;
                case "goalblue2":
                    return goalblue2;
                case "goalblue3":
                    return goalblue3;
                case "goalblue0":
                    return goalblue0;
                case "goalyellow1":
                    return goalyellow1;
                case "goalyellow2":
                    return goalyellow2;
                case "goalyellow3":
                    return goalyellow3;
                case "goalyellow0":
                    return goalyellow0;
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

        private void playercheat_Click(object sender, RoutedEventArgs e)
        {
            MenuItem clicked = (MenuItem)sender;
            String str = clicked.Name;
            String[] strarray = str.Split('_');
            int hi = -1;
            Int32.TryParse(strarray[1], out hi);
            if (hi > -1)
                theGame.cheatPlayer(hi);
        }

        private void throwcheat_Click(object sender, RoutedEventArgs e)
        {
            MenuItem clicked = (MenuItem)sender;
            String str = clicked.Name;
            String[] strarray = str.Split('_');
            int hi = -1;
            Int32.TryParse(strarray[1], out hi);
            if (hi > -1)
            {
                theGame.cheatThrow(hi);
                changeDice(hi);
            }
        }

        public Game TheGame
        {
            get { return theGame; }
            private set { theGame = value; }
        }

        public Spawn[] Spawns
        {
            get { return spawns; }
            set { spawns = value; }
        }
    }
}
