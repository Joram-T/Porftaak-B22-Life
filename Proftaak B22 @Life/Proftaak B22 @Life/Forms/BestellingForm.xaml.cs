using Proftaak_B22__Life.DatabaseContext;
using Proftaak_B22__Life.Class;
using Proftaak_B22__Life.Exception;
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
    /// Interaction logic for BestellingForm.xaml
    /// </summary>
    public partial class BestellingForm : Window
    {
        private BestellingSQLContext bestellingcontext = new BestellingSQLContext();
        private MedewerkerSQLContext medewerkercontext = new MedewerkerSQLContext();
        private KlantSQLContext klantcontext = new KlantSQLContext();
        private List<Window> actief;
        private int selectedid = -1;
        string[] selectedbestelling;

        public BestellingForm(List<Window> actief)
        {
            InitializeComponent();
            FillLb();
            this.actief = actief;
            btnEdit.IsEnabled = false;
            btnSluit.IsEnabled = false;
        }



        private void btnSluit_Click(object sender, RoutedEventArgs e)
        {
            SluitBestellingForm sbf = new SluitBestellingForm(this.selectedid);
            sbf.Show();
        }

        public void FillLb()
        {
            try
            {
                lbOpen.Items.Clear();
                lbGesloten.Items.Clear();
                foreach (Bestelling b in bestellingcontext.GetOpenBestellingen())
                {
                    lbOpen.Items.Add(new string[] { b.Id.ToString(), b.Besteldatum.ToString("dd/MM/yyyy") });
                }
                foreach (Bestelling b in bestellingcontext.GetGeslotenBestellingen())
                {
                    lbGesloten.Items.Add(new string[] { b.Id.ToString(), b.Besteldatum.ToString("dd/MM/yyyy") });
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            WijzigBestelling wb = new WijzigBestelling(this.selectedid);
            wb.Show();
        }

        //private void btnSave_Click_1(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (selectedid != -1)
        //        {
        //            bestellingcontext.UpdateBestelling(selectedid, Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text), Convert.ToDateTime(dpBetaal.Text));
        //        }
        //        else
        //        {
        //            if (dpBetaal.Text == "" && dpLever.Text == "")
        //            {
        //                bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text));
        //            }
        //            else if (dpBetaal.Text == "" && dpLever.Text != "")
        //            {
        //                bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text));
        //            }
        //            else if (dpBetaal.Text != "" && dpLever.Text != "")
        //            {
        //                bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text), betaaldatum: Convert.ToDateTime(dpBetaal.Text));
        //            }
        //            else
        //            {
        //                bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text), Convert.ToDateTime(dpBetaal.Text));
        //            }

        //        }
        //    }
        //    catch (System.Exception exception)
        //    {
        //        MessageBox.Show(exception.Message);
        //    }

        //    lbGesloten.Items.Clear();
        //    lbOpen.Items.Clear();
        //    FillLb();
        //    tbMedewerker.IsEnabled = false;
        //    dpBestel.IsEnabled = false;
        //    tbKlant.IsEnabled = false;
        //    dpLever.IsEnabled = false;
        //    dpBetaal.IsEnabled = false;
        //    btnEdit.IsEnabled = true;
        //    lblKlant.Content = "Klant:";
        //    lblMedewerker.Content = "Medewerker:";
        //}

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            BestellingToevoegenForm btf = new BestellingToevoegenForm(bestellingcontext.GetBestellingByID(this.selectedid));
            btf.Show();

        }

        private void lbGesloten_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (lbGesloten.SelectedIndex != -1)
            {
                try
                {
                    dpBetaal.Visibility = Visibility.Visible;
                    btnEdit.IsEnabled = true;
                    selectedbestelling = (string[])lbGesloten.SelectedItem;
                    int id = Convert.ToInt32(selectedbestelling[0]);
                    selectedid = id;
                    tbMedewerker.Text = medewerkercontext.GetMedewerkerByID(bestellingcontext.GetBestellingByID(id).Medewerker).LastName;
                    tbMedewerker.IsEnabled = false;
                    dpBestel.SelectedDate = bestellingcontext.GetBestellingByID(id).Besteldatum;
                    dpBestel.IsEnabled = false;
                    Klant klant = klantcontext.GetKlantByID(bestellingcontext.GetBestellingByID(id).Klant);
                    tbKlant.Text = klant.LastName + ", " + klant.FirstName;
                    tbKlant.IsEnabled = false;
                    dpLever.SelectedDate = bestellingcontext.GetBestellingByID(id).Leverdatum;
                    dpLever.IsEnabled = false;
                    dpBetaal.SelectedDate = bestellingcontext.GetBestellingByID(id).Betaaldatum;
                    dpBetaal.IsEnabled = false;
                    btnSluit.IsEnabled = false;
                }
                catch
                {
                    MessageBox.Show("Selecteer de juiste waarden!");
                }
            }
        }

        private void lbOpen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbOpen.SelectedIndex != -1)
            {
                dpBetaal.Visibility = Visibility.Hidden;
                btnEdit.IsEnabled = true;
                selectedbestelling = (string[])lbOpen.SelectedItem;
                int id = Convert.ToInt32(selectedbestelling[0]);
                selectedid = id;
                tbMedewerker.Text = medewerkercontext.GetMedewerkerByID(bestellingcontext.GetBestellingByID(id).Medewerker).LastName;
                tbMedewerker.IsEnabled = false;
                dpBestel.SelectedDate = bestellingcontext.GetBestellingByID(id).Besteldatum;
                dpBestel.IsEnabled = false;
                Klant klant = klantcontext.GetKlantByID(bestellingcontext.GetBestellingByID(id).Klant);
                tbKlant.Text = klant.LastName + ", " + klant.FirstName;
                tbKlant.IsEnabled = false;
                dpLever.SelectedDate = bestellingcontext.GetBestellingByID(id).Leverdatum;
                dpLever.IsEnabled = false;
                dpBetaal.IsEnabled = false;
                btnSluit.IsEnabled = true;
            }
        }
    }
}
