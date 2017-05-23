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
        private List<Window> actief;
        private int selectedid =-1;

        public BestellingForm(List<Window> actief)
        {
            InitializeComponent();
            FillLb();
            this.actief = actief;
            btnSave.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnSluit.IsEnabled = false;
        }

        private void lbGesloten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedbestelling;
            if (lbGesloten.SelectedIndex != -1)
            {
                selectedbestelling = lbGesloten.SelectedItem.ToString();
                int id = Convert.ToInt32(selectedbestelling.Split(',')[0]);
                selectedid = id;
                tbMedewerker.Text = medewerkercontext.GetMedewerkerByID(bestellingcontext.GetBestellingByID(id).Medewerker).LastName;
                tbMedewerker.IsEnabled = false;
                dpBestel.SelectedDate = bestellingcontext.GetBestellingByID(id).Besteldatum;
                dpBestel.IsEnabled = false;
                tbKlant.IsEnabled = false;
                dpLever.SelectedDate = bestellingcontext.GetBestellingByID(id).Leverdatum;
                dpLever.IsEnabled = false;
                dpBetaal.SelectedDate = bestellingcontext.GetBestellingByID(id).Betaaldatum;
                dpBetaal.IsEnabled = false;
                btnSave.IsEnabled = false;
                btnEdit.IsEnabled = true;
                btnSluit.IsEnabled = false;
            }
           }

        private void lbOpen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedbestelling;
            if (lbOpen.SelectedIndex != -1)
            {
                selectedbestelling = lbOpen.SelectedItem.ToString();
                int id = Convert.ToInt32(selectedbestelling.Split(',')[0]);
                selectedid = id;
                tbMedewerker.Text = medewerkercontext.GetMedewerkerByID(bestellingcontext.GetBestellingByID(id).Medewerker).LastName;
                tbMedewerker.IsEnabled = false;
                dpBestel.SelectedDate = bestellingcontext.GetBestellingByID(id).Besteldatum;
                dpBestel.IsEnabled = false;
                tbKlant.IsEnabled = false;
                dpLever.Text = "";
                dpLever.IsEnabled = true;
                dpBetaal.Text = "";
                dpBetaal.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnEdit.IsEnabled = true;
                btnSluit.IsEnabled = true;
            }
        }

        private void btnSluit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpLever.Text != "" && dpBetaal.Text != "")
                {
                    foreach (Bestelling b in bestellingcontext.GetOpenBestellingen())
                    {
                        if (b.Id == selectedid && dpLever.Text != "" && dpBetaal.Text != "")
                        {
                            bestellingcontext.SluitBestelling(b.Id, Convert.ToDateTime(dpLever.Text), Convert.ToDateTime(dpBetaal.Text));
                        }
                    }
                    FillLb();
                }
                else if (dpBetaal.Text == "" && dpLever.Text != "")
                {
                    throw new SluitBestellingException("Vul een betaaldatum in");
                }
                else if (dpBetaal.Text != "" && dpLever.Text == "")
                {
                    throw new SluitBestellingException("Vul een leverdatum in");
                }
                else
                {
                    throw new SluitBestellingException("Vul een lever- en betaaldatum in");
                }
            }
            catch(SluitBestellingException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void FillLb()
        {
            try
            {
                lbOpen.Items.Clear();
                lbGesloten.Items.Clear();
                foreach (Bestelling b in bestellingcontext.GetOpenBestellingen())
                {
                    lbOpen.Items.Add(b);
                }
                foreach (Bestelling b in bestellingcontext.GetGeslotenBestellingen())
                {
                    lbGesloten.Items.Add(b);
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
            tbMedewerker.IsEnabled = true;
            dpBestel.IsEnabled = true;
            tbKlant.IsEnabled = true;
            dpLever.IsEnabled = true;
            dpBetaal.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnSluit.IsEnabled = false;  
        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {               
                if (selectedid != -1)
                {
                   bestellingcontext.UpdateBestelling(selectedid, Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text), Convert.ToDateTime(dpBetaal.Text));
                }
                else
                {
                    if (dpBetaal.Text == "" && dpLever.Text == "")
                    {
                        bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text));
                    }
                    else if (dpBetaal.Text == "" && dpLever.Text != "")
                    {
                        bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text));
                    }
                    else if (dpBetaal.Text != "" && dpLever.Text != "")
                    {
                        bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text), betaaldatum: Convert.ToDateTime(dpBetaal.Text));
                    }
                    else
                    {
                        bestellingcontext.InsertBestelling(Convert.ToInt32(tbKlant.Text), Convert.ToInt32(tbMedewerker.Text), Convert.ToDateTime(dpBestel.Text), Convert.ToDateTime(dpLever.Text), Convert.ToDateTime(dpBetaal.Text));
                    }
                    
                }
            }
            catch (System.Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            lbGesloten.Items.Clear();
            lbOpen.Items.Clear();
            FillLb();
            tbMedewerker.IsEnabled = false;
            dpBestel.IsEnabled = false;
            tbKlant.IsEnabled = false;
            dpLever.IsEnabled = false;
            dpBetaal.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnEdit.IsEnabled = true;
            lblKlant.Content = "Klant:";
            lblMedewerker.Content = "Medewerker:";
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbGesloten.SelectedIndex = -1;
                lbOpen.SelectedIndex = 1;
                selectedid = -1;
                lblKlant.Content = "Klant ID:";
                lblMedewerker.Content = "Medewerker ID:";
                tbKlant.Text = "";
                tbMedewerker.Text = "";
                dpBestel.ClearValue(DatePicker.SelectedDateProperty);
                dpLever.ClearValue(DatePicker.SelectedDateProperty);
                dpBetaal.ClearValue(DatePicker.SelectedDateProperty);
                tbMedewerker.IsEnabled = true;
                dpBestel.IsEnabled = true;
                tbKlant.IsEnabled = true;
                dpLever.IsEnabled = true;
                dpBetaal.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnEdit.IsEnabled = false;
                btnSluit.IsEnabled = false;
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
    }
    }
