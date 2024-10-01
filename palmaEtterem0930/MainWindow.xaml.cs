using System.IO;
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

namespace palmaEtterem0930
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Palma> sutik = new();
        public MainWindow()
        {
            InitializeComponent();

            using StreamReader sr = new(
                path: @"..\..\..\src\palma.txt",
                encoding: Encoding.UTF8
                );
            while (!sr.EndOfStream)
            {
                sutik.Add(new Palma(sr.ReadLine()));
            }
            sr.Close();
            Random rnd = new Random();

            if (sutik.Count > 0)
            {
                Ajanlat.Content = "Mai ajánlatunk: " + sutik[rnd.Next(0, sutik.Count)].Nev;
            }


        }
    }
}