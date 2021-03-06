﻿using Proftaak_B22__Life.DatabaseContext;
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
using Proftaak_B22__Life.Class;

namespace Proftaak_B22__Life.Forms
{
    /// <summary>
    /// Interaction logic for LeverancierForm.xaml
    /// </summary>
    public partial class LeverancierForm : Window
    {
        private LeverancierSQLContext leverancierContext = new LeverancierSQLContext();
        private List<Window> actief;
        Leverancier Leverancier;
        public LeverancierForm(List<Window> actief)
        {
            InitializeComponent();
            List<Leverancier> testlist = new List<Leverancier>();
            foreach (Leverancier l in leverancierContext.GetAllLeveranciers())
            {
                lb_Leveranciers.Items.Add(l.ToString());
            }
            this.actief = actief;
            btnGegevensWijzigen.Visibility = Visibility.Hidden;
            btnVerwijderenLeverancier.Visibility = Visibility.Hidden;
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

        private void lb_Leveranciers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Leveranciers.SelectedIndex != -1)
            {
                btnGegevensWijzigen.Visibility = Visibility.Visible;
                string selectedLeverancier = lb_Leveranciers.SelectedItem.ToString();
                int selectedLeverancierID = Convert.ToInt32(selectedLeverancier.Split(' ')[0]);
                this.Leverancier = leverancierContext.GetLeverancierByID(selectedLeverancierID);
                lblNaamLeverancier.Content = Leverancier.Name;
                lblAdresLeverancier.Content = Leverancier.Address;
                lblStadLeverancier.Content = Leverancier.City;
            }
        }

        private void tb_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            lb_Leveranciers.SelectedIndex = -1;
            if (tb_Search.Text == "Search")
            {
                tb_Search.Text = "";
            }
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {

            List<Leverancier> zoekopdrachtleveranciers = new List<Leverancier>();
            if (tb_Search.Text != "")
            {
                foreach (Leverancier l in leverancierContext.GetAllLeveranciers())
                {
                    if (l.ToString().IndexOf(tb_Search.Text, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        zoekopdrachtleveranciers.Add(l);
                    }
                }
                lb_Leveranciers.Items.Clear();
                foreach (Leverancier l in zoekopdrachtleveranciers)
                {
                    lb_Leveranciers.Items.Add(l.ToString());
                }

            }
            else
            {
                lb_Leveranciers.Items.Clear();
                foreach (Leverancier l in leverancierContext.GetAllLeveranciers())
                {
                    lb_Leveranciers.Items.Add(l.ToString());
                }
            }
        }

        private void btnLevToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbInsertLevNaam.Text != "" || tbInsertLevAdres.Text != "" || tbInsertLevStad.Text != "")
                {
                    leverancierContext.InsertLeverancier(new Leverancier(tbInsertLevNaam.Text, tbInsertLevAdres.Text, tbInsertLevStad.Text));
                    MessageBox.Show("Leverancier is toegevoegd!");
                    lb_Leveranciers.Items.Clear();
                    foreach (Leverancier l in leverancierContext.GetAllLeveranciers())
                    {
                        lb_Leveranciers.Items.Add(l.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Gelieve alle velden in te vullen!");
                }

            }

            catch (System.Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }

        private void btnLevToevoegenReset_Click(object sender, RoutedEventArgs e)
        {
            tbInsertLevAdres.Clear();
            tbInsertLevNaam.Clear();
            tbInsertLevStad.Clear();
        }

        private void btnGegevensWijzigen_Click(object sender, RoutedEventArgs e)
        {
            WijzigLeverancierGegevens wlg = new WijzigLeverancierGegevens(this.Leverancier);
            wlg.Show();
        }

        public void RefreshLeverancierData()
        {
            lb_Leveranciers.Items.Clear();
            foreach (Leverancier l in leverancierContext.GetAllLeveranciers())
            {
                lb_Leveranciers.Items.Add(l.ToString());
            }
            Leverancier updatedleverancier = leverancierContext.GetLeverancierByID(this.Leverancier.ID);
            lblNaamLeverancier.Content = updatedleverancier.Name;
            lblAdresLeverancier.Content = updatedleverancier.Address;
            lblStadLeverancier.Content = updatedleverancier.City;
        }

        private void btnVerwijderenLeverancier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
