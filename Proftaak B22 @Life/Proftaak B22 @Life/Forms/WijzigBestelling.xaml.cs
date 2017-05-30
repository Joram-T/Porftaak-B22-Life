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
    /// Interaction logic for WijzigBestelling.xaml
    /// </summary>
    public partial class WijzigBestelling : Window
    {
        MedewerkerSQLContext medewerkercontext = new MedewerkerSQLContext();
        KlantSQLContext klantcontext = new KlantSQLContext();
        BestellingSQLContext bestellingcontext = new BestellingSQLContext();
        int selectedid;
        public WijzigBestelling(int selectedid)
        {
            InitializeComponent();
            this.selectedid = selectedid;
            if (bestellingcontext.GetBestellingByID(selectedid).Betaaldatum == default(DateTime))
            {
                dpBetaal.Visibility = Visibility.Hidden;
            }
            else
            {
                dpBetaal.Visibility = Visibility.Visible;
            }
        }

        private void tbMedewerker_GotFocus(object sender, RoutedEventArgs e)
        {
            lblKies.Content = "Kies een medewerker";
            lbMedewerkersKlanten.Visibility = Visibility.Visible;
            lbMedewerkersKlanten.Items.Clear();
            foreach (Medewerker m in medewerkercontext.GetAllMedewerkers())
            {
                lbMedewerkersKlanten.Items.Add(m.ToString());
            }
        }

        private void tbMedewerker_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Medewerker> zoekopdrachtmedewerkers = new List<Medewerker>();
            if (tbMedewerker.Text != "")
            {
                foreach (Medewerker m in medewerkercontext.GetAllMedewerkers())
                {
                    if (m.ToString().IndexOf(tbMedewerker.Text, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        zoekopdrachtmedewerkers.Add(m);
                    }
                }
                lbMedewerkersKlanten.Items.Clear();
                foreach (Medewerker m in zoekopdrachtmedewerkers)
                {
                    lbMedewerkersKlanten.Items.Add(m.ToString());
                }
            }

            else
            {
                lbMedewerkersKlanten.Items.Clear();
                foreach (Medewerker m in medewerkercontext.GetAllMedewerkers())
                {
                    lbMedewerkersKlanten.Items.Add(m.ToString());
                }
            }
        }

        private void tbKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            lblKies.Content = "Kies een klant";
            lbMedewerkersKlanten.Visibility = Visibility.Visible;
            lbMedewerkersKlanten.Items.Clear();
            foreach (Klant k in klantcontext.GetAllKlanten())
            {
                lbMedewerkersKlanten.Items.Add(k.ToString());
            }
        }

        private void tbKlant_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Klant> zoekopdrachtklanten = new List<Klant>();
            if (tbKlant.Text != "")
            {
                foreach (Klant k in klantcontext.GetAllKlanten())
                {
                    if (k.ToString().IndexOf(tbKlant.Text, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        zoekopdrachtklanten.Add(k);
                    }
                }
                lbMedewerkersKlanten.Items.Clear();
                foreach (Klant k in zoekopdrachtklanten)
                {
                    lbMedewerkersKlanten.Items.Add(k.ToString());
                }

            }
            else
            {
                lbMedewerkersKlanten.Items.Clear();
                foreach (Klant k in klantcontext.GetAllKlanten())
                {
                    lbMedewerkersKlanten.Items.Add(k.ToString());
                }
            }
        }

        private void lbMedewerkersKlanten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMedewerkersKlanten.SelectedIndex != -1)
            {
                string selecteditemID = lbMedewerkersKlanten.SelectedItem.ToString().Split(' ')[0];
                if (lblKies.Content.ToString() == "Kies een medewerker")
                {
                    Medewerker medewerker = medewerkercontext.GetMedewerkerByID(Convert.ToInt32(selecteditemID));
                    tbMedewerker.Text = medewerker.ToString();
                    tbMedewerker.IsEnabled = false;
                    lbMedewerkersKlanten.Visibility = Visibility.Hidden;
                    lblKies.Content = "";

                }
                else if (lblKies.Content.ToString() == "Kies een klant")
                {
                    Klant klant = klantcontext.GetKlantByID(Convert.ToInt32(selecteditemID));
                    tbKlant.Text = klant.ToString();
                    tbKlant.IsEnabled = false;
                    lbMedewerkersKlanten.Visibility = Visibility.Hidden;
                    lblKies.Content = "";
                }
            }
        }

        private void btnWijzigingenOpslaan_Click(object sender, RoutedEventArgs e)
        {
            
                Klant klant = null;
                Medewerker medewerker = null;
                string bestel = dpBestel.Text;
                string lever = dpLever.Text;
                string betaal = dpBetaal.Text;
    
                if (!string.IsNullOrEmpty(tbKlant.Text))
                {
                    klant = klantcontext.GetKlantByID(Convert.ToInt32(tbKlant.Text.Split(' ')[0]));
                }
                if (!string.IsNullOrEmpty(tbMedewerker.Text))
                {
                    medewerker = medewerkercontext.GetMedewerkerByID(Convert.ToInt32(tbMedewerker.Text.Split(' ')[0]));
                }


                if (string.IsNullOrEmpty(bestel) && string.IsNullOrEmpty(lever) && string.IsNullOrEmpty(betaal))
                {
                    bestellingcontext.UpdateBestelling(selectedid, klant, medewerker, null, null, null);
                    MessageBox.Show("Bestelling gewijzigd!");
                    this.Close();
                return;
                }
                if (string.IsNullOrEmpty(bestel) && string.IsNullOrEmpty(lever))
                {
                    bestellingcontext.UpdateBestelling(selectedid, klant, medewerker, null, null, Convert.ToDateTime(betaal));
                    MessageBox.Show("Bestelling gewijzigd!");
                    this.Close();
                return;
            }
                if (string.IsNullOrEmpty(bestel) && string.IsNullOrEmpty(betaal))
                {
                    bestellingcontext.UpdateBestelling(selectedid, klant, medewerker, null, Convert.ToDateTime(lever), null);
                    MessageBox.Show("Bestelling gewijzigd!");
                    this.Close();
                return;
            }
                if (string.IsNullOrEmpty(lever) && string.IsNullOrEmpty(betaal))
                {
                    bestellingcontext.UpdateBestelling(selectedid, klant, medewerker, Convert.ToDateTime(bestel), null, null);
                    MessageBox.Show("Bestelling gewijzigd!");
                    this.Close();
                return;
            }
                if (string.IsNullOrEmpty(bestel))
                {
                    bestellingcontext.UpdateBestelling(selectedid, klant, medewerker, null, Convert.ToDateTime(lever), Convert.ToDateTime(betaal));
                    MessageBox.Show("Bestelling gewijzigd!");
                    this.Close();
                return;
            }
                if (string.IsNullOrEmpty(lever))
                {
                    bestellingcontext.UpdateBestelling(selectedid, klant, medewerker, Convert.ToDateTime(bestel), null, Convert.ToDateTime(betaal));
                    MessageBox.Show("Bestelling gewijzigd!");
                    this.Close();
                return;
            }
                if (string.IsNullOrEmpty(betaal))
                {
                    bestellingcontext.UpdateBestelling(selectedid, klant, medewerker, Convert.ToDateTime(bestel), Convert.ToDateTime(lever), null);
                    MessageBox.Show("Bestelling gewijzigd!");
                    this.Close();
                return;
            }

            
           
        }
    }
}
