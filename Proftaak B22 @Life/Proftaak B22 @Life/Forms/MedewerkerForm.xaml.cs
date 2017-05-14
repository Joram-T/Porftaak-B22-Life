using Microsoft.Win32;
using Proftaak_B22__Life.Class;
using Proftaak_B22__Life.DatabaseContext;
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
using System.Windows.Shapes;

namespace Proftaak_B22__Life.Forms
{
    /// <summary>
    /// Interaction logic for MedewerkerForm.xaml
    /// </summary>
    public partial class MedewerkerForm : Window
    {
        private MedewerkerSQLContext medewerkerContext = new MedewerkerSQLContext();
        private AccountSQLContext accountContext = new AccountSQLContext();
        private BestellingSQLContext bestellingContext = new BestellingSQLContext();
        ProductSQLContext productContext = new ProductSQLContext();
        private List<Window> actief;
        string selectedwerknemer = "";
        Medewerker currentMedewerker;
        public MedewerkerForm(List<Window> actief)
        {
            InitializeComponent();
            btnVeranderProfielfoto.Visibility = Visibility.Hidden;
            foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
            {
                lb_Werknemers.Items.Add(m.ToString());
            }
            this.actief = actief;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Window wi = new Window();
            foreach (Window w in actief)
            {
                if (w.GetType() == this.GetType())
                {
                    wi = w;
                }
            }
            actief.Remove(wi);
        }

        private void lb_Werknemers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Werknemers.SelectedIndex != -1)
            {
                btnVeranderProfielfoto.Visibility = Visibility.Visible;     
                selectedwerknemer = lb_Werknemers.SelectedItem.ToString();
                int id = Convert.ToInt32(selectedwerknemer.Split(' ')[0]);
                lblNaamWerknemer.Content = medewerkerContext.GetMedewerkerByID(id).Insertion + " " + medewerkerContext.GetMedewerkerByID(id).LastName + ", " + medewerkerContext.GetMedewerkerByID(id).FirstName;
                lblAdreswerknemer.Content = medewerkerContext.GetMedewerkerByID(id).Address;
                lblStadWerknemer.Content = medewerkerContext.GetMedewerkerByID(id).City;

                this.currentMedewerker = medewerkerContext.GetMedewerkerByID(id);
                gbBestellingen.Header = "Bestellingen van " + currentMedewerker.FirstName;
                List<Bestelling> bestellingen = new List<Bestelling>();
                bestellingen = medewerkerContext.GetBestellingenForMedewerker(currentMedewerker);
                lbBestellingen.Items.Clear();
                foreach (Bestelling b in bestellingen)
                {
                    lbBestellingen.Items.Add(b.ToString());
                }

                img_Profielfoto.Source = null;
                if (medewerkerContext.GetProfielfotoForMedewerker(this.currentMedewerker) != null)
                {
                    img_Profielfoto.Source = medewerkerContext.GetProfielfotoForMedewerker(currentMedewerker);
                }
            }
        }

        private void tb_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            lb_Werknemers.SelectedIndex = -1;
            if (tb_Search.Text == "Search")
            {
                tb_Search.Text = "";
            }
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {

            List<Medewerker> zoekopdrachtmedewerkers = new List<Medewerker>();
            if (tb_Search.Text != "")
            {
                foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
                {
                    if (m.ToString().IndexOf(tb_Search.Text, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        zoekopdrachtmedewerkers.Add(m);
                    }
                }
                lb_Werknemers.Items.Clear();
                foreach (Medewerker m in zoekopdrachtmedewerkers)
                {
                    lb_Werknemers.Items.Add(m.ToString());
                }

            }
            else
            {
                lb_Werknemers.Items.Clear();
                foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
                {
                    lb_Werknemers.Items.Add(m.ToString());
                }
            }
        }

        private void btnMedToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account account = new Account(tbInsertMedEmail.Text, tbInsertMedWachtwoord.Text);
                if (accountContext.Login(account) != null)
                {
                    MessageBox.Show("Er is al een medewerker met deze inloggegevens!");
                }
                else
                {
                    accountContext.InsertAccount(account);
                    medewerkerContext.InsertMedewerker(new Medewerker(account, tbInsertMedNaam.Text,
                        tbInsertMedTussenvoegsel.Text, tbInsertMedAchternaam.Text, tbInsertMedAdres.Text,
                        tbInsertMedStad.Text));
                    MessageBox.Show("Medewerker is toegevoegd!");
                    lb_Werknemers.Items.Clear();
                    foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
                    {
                        lb_Werknemers.Items.Add(m.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oeps! Kijk of de gegevens goed ingevuld zijn.");
                Console.WriteLine(ex.Message);
            }


        }

        private void btnMedToevoegenReset_Click(object sender, RoutedEventArgs e)
        {
            tbInsertMedAchternaam.Clear();
            tbInsertMedAdres.Clear();
            tbInsertMedEmail.Clear();
            tbInsertMedNaam.Clear();
            tbInsertMedWachtwoord.Clear();
            tbInsertMedTussenvoegsel.Clear();
            tbInsertMedStad.Clear();
        }

        private void lbBestellingen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbBestellingen.SelectedIndex != -1)
            { 
                string selectedbestelling = lbBestellingen.SelectedItem.ToString();
                int id = Convert.ToInt32(string.Join("", selectedbestelling.Split(',')[0].ToCharArray().Where(Char.IsDigit)));
                lv_BestellingArtikelen.Items.Clear();
                Bestelling currentBestelling = bestellingContext.GetBestellingByID(id);
                string status = "";
                foreach (Artikel a in bestellingContext.GetArtikelenForBestelling(currentBestelling))
                {
                    if (currentBestelling.Betaaldatum != Convert.ToDateTime("1-1-0001 00:00:00"))
                    {
                        status = "Betaald";
                    }
                    else
                    {
                        status = "In behandeling";
                    }
                    Product artikelproduct = productContext.GetProductByID(a.Productid);
                    lv_BestellingArtikelen.Items.Add(new string[] { a.Artikelid.ToString(), artikelproduct.Name, "€"+artikelproduct.Price.ToString(), status });
                }
            }
        }

        private void btnVeranderProfielfoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            
            if (open.ShowDialog() == true)
            {
                byte[] profielfoto = File.ReadAllBytes(open.FileName);
                try
                {
                    medewerkerContext.UpdateProfielfoto(this.currentMedewerker, profielfoto);
                    img_Profielfoto.Source = medewerkerContext.GetProfielfotoForMedewerker(this.currentMedewerker);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}
