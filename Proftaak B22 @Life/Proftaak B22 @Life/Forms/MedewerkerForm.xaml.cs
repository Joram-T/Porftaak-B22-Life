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
    /// Interaction logic for MedewerkerForm.xaml
    /// </summary>
    public partial class MedewerkerForm : Window
    {
        private MedewerkerSQLContext medewerkerContext = new MedewerkerSQLContext();
        private AccountSQLContext accountContext = new AccountSQLContext();
        private List<Window> actief;
        string selectedwerknemer = "";
        public MedewerkerForm(List<Window> actief)
        {
            InitializeComponent();
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
                selectedwerknemer = lb_Werknemers.SelectedItem.ToString();
                int id = Convert.ToInt32(selectedwerknemer.Split(' ')[0]);
                lblNaamWerknemer.Content = medewerkerContext.GetMedewerkerByID(id).Insertion + " " + medewerkerContext.GetMedewerkerByID(id).LastName + ", " + medewerkerContext.GetMedewerkerByID(id).FirstName;
                lblAdreswerknemer.Content = medewerkerContext.GetMedewerkerByID(id).Address;
                lblStadWerknemer.Content = medewerkerContext.GetMedewerkerByID(id).City;
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
            if (accountContext.Login(tbInsertMedEmail.Text, tbInsertMedWachtwoord.Text) != null)
            {
                MessageBox.Show("Er is al een medewerker met deze inloggegevens!");
            }
            else
            {
                Account account = new Account(tbInsertMedEmail.Text, tbInsertMedWachtwoord.Text);
                accountContext.InsertAccount(account);
                medewerkerContext.InsertMedewerker(new Medewerker(account, tbInsertMedNaam.Text, tbInsertMedTussenvoegsel.Text, tbInsertMedAchternaam.Text, tbInsertMedAdres.Text, tbInsertMedStad.Text));
                MessageBox.Show("Medewerker is toegevoegd!");
                foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
                {
                    lb_Werknemers.Items.Add(m.ToString());
                }
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
    }
}
