﻿using Proftaak_B22__Life.Class;
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
        private OrderSQLContext orderContext = new OrderSQLContext();
        private List<Window> actief;
        string selectedwerknemer = "";
        string selectedbestelling = "";
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
                Medewerker selectedmedewerker = medewerkerContext.GetMedewerkerByID(id);
                lblNaamWerknemer.Content = selectedmedewerker.Insertion + " " + selectedmedewerker.LastName + ", " + selectedmedewerker.FirstName;

                gbBestellingen.Header = "Bestellingen van " + selectedmedewerker.FirstName;
                List<Order> orders = new List<Order>();
                orders = orderContext.GetAllOrdersForMedewerker(selectedmedewerker);
                lb_Bestellingen.Items.Clear();
                foreach (Order o in orders)
                {
                    lb_Bestellingen.Items.Add(o.ToString());
                }

                lblAdreswerknemer.Content = selectedmedewerker.Address;
                lblStadWerknemer.Content = selectedmedewerker.City;
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
                try
                {
                    Account account = new Account(tbInsertMedEmail.Text, tbInsertMedWachtwoord.Text);
                    accountContext.InsertAccount(account);
                    Medewerker medewerker = new Medewerker(account, tbInsertMedNaam.Text, tbInsertMedTussenvoegsel.Text, tbInsertMedAchternaam.Text, tbInsertMedAdres.Text, tbInsertMedStad.Text);
                    medewerkerContext.InsertMedewerker(medewerker);
                    MessageBox.Show("Medewerker is toegevoegd!");
                    lb_Werknemers.Items.Clear();
                    foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
                    {
                        lb_Werknemers.Items.Add(m.ToString());

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Er is al een medewerker met deze inloggegevens!");
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
 
        private void lb_Bestellingen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Bestellingen.SelectedIndex != -1)
            {
                selectedbestelling = lb_Bestellingen.SelectedItem.ToString();
                int id = Convert.ToInt32(string.Join("", selectedbestelling.Split('-')[0].ToCharArray().Where(Char.IsDigit)));
                lb_BestellingArtikelen.Items.Clear();

                foreach (Product p in orderContext.GetArtikelenForOrder(orderContext.GetOrderByID(id)))
                {
                    lb_BestellingArtikelen.Items.Add(new string[] { p.ID.ToString(), p.Name, "€"+p.Price.ToString() });
                }
            }
        }
    }
}
