using Proftaak_B22__Life.DatabaseContext;
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

namespace Proftaak_B22__Life.Forms
{
    /// <summary>
    /// Interaction logic for WijzigGegevens.xaml
    /// </summary>
    public partial class WijzigGegevens : Window
    {
        Medewerker currentMedewerker;
        MedewerkerSQLContext medewerkerContext = new MedewerkerSQLContext();
        public WijzigGegevens(Medewerker medewerker)
        {
            InitializeComponent();
            this.currentMedewerker = medewerker;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            string voornaam = tbVoornaam.Text;
            string achternaam = tbAchternaam.Text;
            string tussenvoegsel = tbTussenvoegsel.Text;
            string adres = tbAdres.Text;
            string woonplaats = tbWoonplaats.Text;
            if (string.IsNullOrEmpty(tbVoornaam.Text))
            {
                voornaam = null;
            }
            if (string.IsNullOrEmpty(tbAchternaam.Text))
            {
                achternaam = null;
            }
            if (string.IsNullOrEmpty(tbTussenvoegsel.Text))
            {
                tussenvoegsel = null;
            }
            if (string.IsNullOrEmpty(tbAdres.Text))
            {
                adres = null;
            }
            if (string.IsNullOrEmpty(tbWoonplaats.Text))
            {
                woonplaats = null;
            }
            medewerkerContext.UpdateMedewerker(this.currentMedewerker, voornaam, achternaam, tussenvoegsel, woonplaats,adres);
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MedewerkerForm))
                {
                    (window as MedewerkerForm).lb_Werknemers.Items.Clear();
                    foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
                    {
                        (window as MedewerkerForm).lb_Werknemers.Items.Add(m.ToString());
                    }
                }
            }
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbVoornaam.Clear();
            tbAchternaam.Clear();
            tbTussenvoegsel.Clear();
            tbAdres.Clear();
            tbWoonplaats.Clear();
        }
    }
}
