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

            //Harmadik feladat
            //legdrágább süti
            var max = sutik.MaxBy(x => x.Ar);
            int index = sutik.IndexOf(max);
            legdragabb.Content = $"{sutik[index].Nev} {sutik[index].Ar}ft/{sutik[index].Ertekesites}";

            //legolcsóbb süti
            var min = sutik.MinBy(x => x.Ar);
            index = sutik.IndexOf(min);
            legolcsobb.Content = $"{sutik[index].Nev} {sutik[index].Ar}ft/{sutik[index].Ertekesites}";

            //Negyedik feladat
            int dijnyertesSzam = 0;
            for (int i = 0; i < sutik.Count; i++)
            {
                if (sutik[i].Verseny)
                {
                    dijnyertesSzam++;
                }
            }
            dijnyertes.Content = $"{dijnyertesSzam} féle díjnyertes édességből választhat.";

            //Ötödik feladat
            List<string> desszertekLista = new();
            string path = @"..\..\..\src\lista.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var x in sutik)
                {
                    if (!desszertekLista.Contains(x.Nev))
                    {
                        sw.WriteLine($"{x.Nev} – {x.Tipus}");
                        desszertekLista.Add(x.Nev);
                    }
                }
            }

            //Hatodik feladat
            Dictionary<string, int> tipusSzamlalo = new Dictionary<string, int>();
            foreach (var x in sutik)
            {
                if (tipusSzamlalo.ContainsKey(x.Tipus))
                {
                    tipusSzamlalo[x.Tipus]++;
                }
                else
                {
                    tipusSzamlalo[x.Tipus] = 1;
                }
            }

            path = @"..\..\..\src\stat.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var x in tipusSzamlalo)

                    sw.WriteLine($"{x.Key} - {x.Value}");
            }
           

    }

        private void arajanlatButton_Click(object sender, RoutedEventArgs e)
        {
            //Hetes feladat
            string keresetSuti = tipusTextBox.Text;
            bool hiba = false;
            double atlag = 0;
            List<string> lista = new();
            string path = @"..\..\..\src\ajanlat.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var x in sutik)
                {
                    if (x.Tipus.ToLower() == keresetSuti.ToLower())
                    {
                        sw.WriteLine($"{x.Nev} - {x.Ar} – {x.Ertekesites}");
                        hiba = true;
                        atlag += x.Ar;
                        if (!lista.Contains(x.Nev))
                        {
                            lista.Add(x.Nev);
                        }
                    }
                }
            }
            if (!hiba)
            {
                MessageBox.Show("Nincs ilyen típusú desszertünk. Kérjük,válasszon mást");
            }
            else
            {
                MessageBox.Show($"{lista.Count} db desszert van, ezeknek az átlaga {Math.Round( atlag/lista.Count, 0)}Ft");
            }

        }
    }
}
