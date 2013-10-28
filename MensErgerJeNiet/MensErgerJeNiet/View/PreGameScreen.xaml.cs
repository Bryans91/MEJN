using MensErgerJeNiet.ModelView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MensErgerJeNiet.View
{
    /// <summary>
    /// Interaction logic for PreGameScreen.xaml
    /// </summary>
    public partial class PreGameScreen : Window
    {
        public PreGameScreen()
        {
            InitializeComponent();

            this.Visibility = Visibility.Visible;
            getBackground();
        }
        private void getBackground()
        {
            System.Reflection.Assembly thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            string path = thisExe.Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string folderName = dirInfo.Parent.FullName;
            Console.WriteLine(folderName);
            Uri uri = new Uri(folderName + "/background.jpg");
            BitmapImage img = new BitmapImage(uri);
            this.Background = new ImageBrush(img);
        }

        private void Nieuw_Click(object sender, RoutedEventArgs e)
        {
            PreGameScreenv2 pgsv2 = new PreGameScreenv2(new MainWindow());
            this.Close();
        }

        private void Laad_Click(object sender, RoutedEventArgs e)
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
                MainWindow main = new MainWindow();
                List<string> lines = new List<string>();
                string line;
                while((line = sr.ReadLine())!= null)
                    lines.Add(line);
                string[] strarray = lines.ToArray();
                main.TheGame.startFromFile(strarray);
                main.Visibility = Visibility.Visible;
                sr.Close();
                this.Close();
            }
        }
    }
}
