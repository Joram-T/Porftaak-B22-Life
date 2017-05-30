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
    /// Interaction logic for KlantForm.xaml
    /// </summary>
    public partial class KlantForm : Window
    {
        KlantSQLContext klantContext = new KlantSQLContext();
        BestellingSQLContext bestellingContext = new BestellingSQLContext();
        ProductSQLContext productContext = new ProductSQLContext();
        string selectedklant;
        private List<Window> actief;
        Klant currentKlant;
        public KlantForm(List<Window> actief)
        {
            InitializeComponent();
            btnWijzigGegevensKlant.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;

            this.actief = actief;
            foreach (Klant klant in klantContext.GetAllKlanten())
            {
                lb_Klanten.Items.Add(klant.ToString());
            }
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

        private void tb_SearchKlant_GotFocus(object sender, RoutedEventArgs e)
        {
            lb_Klanten.SelectedIndex = -1;
            if (tb_SearchKlant.Text == "Search")
            {
                tb_SearchKlant.Text = "";
            }
        }

        private void tb_SearchKlant_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Klant> zoekopdrachtklanten = new List<Klant>();
            if (tb_SearchKlant.Text != "")
            {
                foreach (Klant k in klantContext.GetAllKlanten())
                {
                    if (k.ToString().IndexOf(tb_SearchKlant.Text, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        zoekopdrachtklanten.Add(k);
                    }
                }
                lb_Klanten.Items.Clear();
                foreach (Klant k in zoekopdrachtklanten)
                {
                    lb_Klanten.Items.Add(k.ToString());
                }

            }
            else
            {
                lb_Klanten.Items.Clear();
                foreach (Klant k in klantContext.GetAllKlanten())
                {
                    lb_Klanten.Items.Add(k.ToString());
                }
            }
        }

        private void btnKlantToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (tbInserKlantVoornaam.Text == "" || tbInsertKlantAchternaam.Text == "" || tbInsertKlantAdres.Text == "" || tbInsertKlantPostcode.Text == "" || tbInsertKlantWoonplaats.Text == "")
            {
                MessageBox.Show("Gelieve alle velden in te vullen!");
            }
            else
            {
                Klant klant = new Klant(tbInserKlantVoornaam.Text, tbInsertKlantTussenvoegsel.Text, tbInsertKlantAchternaam.Text, tbInsertKlantAdres.Text, tbInsertKlantWoonplaats.Text, tbInsertKlantPostcode.Text);
                klantContext.InsertKlant(klant);
                MessageBox.Show("Klant is toegevoegd!");
                lb_Klanten.Items.Clear();
                foreach (Klant k in klantContext.GetAllKlanten())
                {
                    lb_Klanten.Items.Add(k.ToString());
                }
            }

        }

        private void btnKlantToevoegenReset_Click(object sender, RoutedEventArgs e)
        {
            tbInserKlantVoornaam.Clear();
            tbInsertKlantAchternaam.Clear();
            tbInsertKlantAdres.Clear();
            tbInsertKlantPostcode.Clear();
            tbInsertKlantTussenvoegsel.Clear();
            tbInsertKlantWoonplaats.Clear();
        }

        private void lb_Klanten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Klanten.SelectedIndex != -1)
            {
                btnDelete.Visibility = Visibility.Visible;
                btnWijzigGegevensKlant.Visibility = Visibility.Visible;
                selectedklant = lb_Klanten.SelectedItem.ToString();
                int id = Convert.ToInt32(selectedklant.Split(' ')[0]);
                lblKlantNaam.Content = klantContext.GetKlantByID(id).Insertion + " " + klantContext.GetKlantByID(id).LastName + ", " + klantContext.GetKlantByID(id).FirstName;
                lblAdresKlant.Content = klantContext.GetKlantByID(id).Address;
                lblWoonplaatsKlant.Content = klantContext.GetKlantByID(id).City;
                lblPostcodeKlant.Content = klantContext.GetKlantByID(id).Zip;


                currentKlant = klantContext.GetKlantByID(id);
                List<Bestelling> openbestellingen = new List<Bestelling>();
                List<Bestelling> geslotenbestellingen = new List<Bestelling>();
                openbestellingen = klantContext.GetOpenBestellingenForKlant(currentKlant);
                geslotenbestellingen = klantContext.GetGeslotenBestellingenForKlant(currentKlant);
                lbOpenBestellingenKlant.Items.Clear();
                lbGeslotenBestellingenKlant.Items.Clear();
                foreach (Bestelling b in openbestellingen)
                {
                    lbOpenBestellingenKlant.Items.Add(b.ToString());
                }
                foreach (Bestelling b2 in geslotenbestellingen)
                {
                    lbGeslotenBestellingenKlant.Items.Add(b2.ToString());
                }
            }
        }

        private void lbOpenBestellingenKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbOpenBestellingenKlant.SelectedIndex != -1)
            {
                string selectedbestelling = lbOpenBestellingenKlant.SelectedItem.ToString();
                int id = Convert.ToInt32(string.Join("", selectedbestelling.Split(',')[0].ToCharArray().Where(Char.IsDigit)));
                lvBestellingArtikelen.Items.Clear();

                foreach (Artikel a in bestellingContext.GetArtikelenForBestelling(bestellingContext.GetBestellingByID(id)))
                {
                    Product artikelproduct = productContext.GetProductByID(a.Productid);
                    lvBestellingArtikelen.Items.Add(new string[] { a.Artikelid.ToString(), artikelproduct.Name, "€" + artikelproduct.Price.ToString() });
                }
            }
        }

        private void lbGeslotenBestellingenKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvBestellingArtikelen.Items.Clear();
            if (lbGeslotenBestellingenKlant.SelectedIndex != -1)
            {
                string selectedbestelling = lbGeslotenBestellingenKlant.SelectedItem.ToString();
                int id = Convert.ToInt32(string.Join("", selectedbestelling.Split(',')[0].ToCharArray().Where(Char.IsDigit)));
                lvBestellingArtikelen.Items.Clear();

                foreach (Artikel a in bestellingContext.GetArtikelenForBestelling(bestellingContext.GetBestellingByID(id)))
                {
                    Product artikelproduct = productContext.GetProductByID(a.Productid);
                    lvBestellingArtikelen.Items.Add(new string[] { a.Artikelid.ToString(), artikelproduct.Name, "€" + artikelproduct.Price.ToString() });
                }
            }
        }

        private void btnWijzigGegevensKlant_Click(object sender, RoutedEventArgs e)
        {
            WijzigGegevens wg = new WijzigGegevens(this.currentKlant);
            wg.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Klant met alle bestellingen verwijderen?";
            string caption = "Klant verwijderen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        klantContext.DeleteKlantEnBestellingen(this.currentKlant);
                        lb_Klanten.Items.Clear();
                        foreach (Klant k in klantContext.GetAllKlanten())
                        {
                            lb_Klanten.Items.Add(k.ToString());
                        }
                    }
                    catch (System.Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        throw;
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
