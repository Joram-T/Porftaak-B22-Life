using Proftaak_B22__Life.Class;
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
    /// Interaction logic for BestellingToevoegenForm.xaml
    /// </summary>
    public partial class BestellingToevoegenForm : Window
    {
        Bestelling Bestelling;
        BestellingSQLContext bestellingcontext = new BestellingSQLContext();
        MedewerkerSQLContext medewerkercontext = new MedewerkerSQLContext();
        KlantSQLContext klantcontext = new KlantSQLContext();

        public BestellingToevoegenForm(Bestelling bestelling)
        {
            InitializeComponent();
            this.Bestelling = bestelling;
            lbMedewerkersKlanten.Visibility = Visibility.Hidden;
        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbKlant.Text))
            {
                if (tbKlant.IsEnabled == false && tbMedewerker.IsEnabled == false)
                {
                    try
                    {
                        if (dpBetaal.Text == "" && dpLever.Text == "")
                        {
                            bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text.Split(' ')[0]), Convert.ToInt32(tbMedewerker.Text.Split(' ')[0]), Convert.ToDateTime(dpBestel.Text));
                        }
                        else if (dpBetaal.Text == "" && dpLever.Text != "")
                        {
                            bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text.Split(' ')[0]), Convert.ToInt32(tbMedewerker.Text.Split(' ')[0]), Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text));
                        }
                        else if (dpBetaal.Text != "" && dpLever.Text != "")
                        {
                            bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text.Split(' ')[0]), Convert.ToInt32(tbMedewerker.Text.Split(' ')[0]), Convert.ToDateTime(dpBestel.Text), betaaldatum: Convert.ToDateTime(dpBetaal.Text));
                        }
                        else
                        {
                            bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text.Split(' ')[0]), Convert.ToInt32(tbMedewerker.Text.Split(' ')[0]), Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text), Convert.ToDateTime(dpBetaal.Text));
                        }
                        this.Close();
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Vul de juiste datums in");
                    }
                }
                else
                {
                    MessageBox.Show("Selecteer de betreffende klant en medewerker uit de lijst");
                }
            }
            else
            {
                MessageBox.Show("Vul een klant en medewerker in!");
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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbKlant.Clear();
            tbMedewerker.Clear();
            tbKlant.IsEnabled = true;
            tbMedewerker.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType() == typeof(BestellingForm))
                {
                    (w as BestellingForm).FillLb();
                }
            }
        }
    }
}
