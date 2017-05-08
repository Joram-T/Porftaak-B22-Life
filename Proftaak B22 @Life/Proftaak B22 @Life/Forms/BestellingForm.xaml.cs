using Proftaak_B22__Life.DatabaseContext;
using Proftaak_B22__Life.Class;
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
        private int selectedid;

        public BestellingForm(List<Window> actief)
        {
            InitializeComponent();
            foreach(Bestelling b in bestellingcontext.GetOpenBestellingen())
            {
                lbOpen.Items.Add(b);
            }
            foreach (Bestelling b in bestellingcontext.GetGeslotenBestellingen())
            {
                lbGesloten.Items.Add(b);
            }
            this.actief = actief;
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
                tbBestel.Text = bestellingcontext.GetBestellingByID(id).Besteldatum.ToString("dd/MM/yyyy");
                tbBestel.IsEnabled = false;
                tbKlant.IsEnabled = false;
                tbLever.Text = bestellingcontext.GetBestellingByID(id).Leverdatum.ToString("dd/MM/yyyy");
                tbLever.IsEnabled = false;
                tbBetaal.Text = bestellingcontext.GetBestellingByID(id).Betaaldatum.ToString("dd/MM/yyyy");
                tbBetaal.IsEnabled = false;                       
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
                tbBestel.Text = bestellingcontext.GetBestellingByID(id).Besteldatum.ToString("dd/MM/yyyy");
                tbBestel.IsEnabled = false;
                tbKlant.IsEnabled = false;
            }
        }
    }
    }
