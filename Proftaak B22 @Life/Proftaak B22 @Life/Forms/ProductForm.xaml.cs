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
using Proftaak_B22__Life.DatabaseContext;

namespace Proftaak_B22__Life.Forms
{
    /// <summary>
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductForm : Window
    {
        private LeverancierSQLContext leverancierContext = new LeverancierSQLContext();
        private ProductSQLContext productContext = new ProductSQLContext();
        private List<Window> actief;
        string selectedArtikel = "";

        public ProductForm(List<Window> actief)
        {
            InitializeComponent();
            foreach (Artikel a in productContext.GetAllArtikelen())
            {
                lb_Artikelen.Items.Add(a.ToString());
            }
            this.actief = actief;
            cbProduct.ItemsSource = productContext.GetAllProducten();
            cbLeverancier.ItemsSource = leverancierContext.GetAllLeveranciers();
            gbArtikelWijzigen.IsEnabled = false;
            btnWijzigingenOpslaan.IsEnabled = false;
            btnVerwijderArtikel.Visibility = Visibility.Hidden;
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

        private void lb_Artikelen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Artikelen.SelectedIndex != -1)
            {
                btnVerwijderArtikel.Visibility = Visibility.Visible;
                gbArtikelWijzigen.IsEnabled = true;
                btnWijzigingenOpslaan.IsEnabled = true;
                cbWijzigenProduct.ItemsSource = productContext.GetAllProducten();
                cbWijzigenLeverancier.ItemsSource = leverancierContext.GetAllLeveranciers();
                selectedArtikel = lb_Artikelen.SelectedItem.ToString();
                string leverancier = selectedArtikel.Split(' ')[1];
                string product = selectedArtikel.Split(' ')[2];
                lblLeverancier.Content = leverancierContext.GetLeverancierByID(Convert.ToInt32(leverancier)).Name;
                lblProduct.Content = productContext.GetProductByID(Convert.ToInt32(product)).Name;
            }
        }

        private void tb_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            lb_Artikelen.SelectedIndex = -1;
            if (tb_Search.Text == "Search")
            {
                tb_Search.Text = "";
            }
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {

            List<Artikel> zoekopdrachtArtikelen = new List<Artikel>();
            if (tb_Search.Text != "")
            {
                try
                {
                    foreach (Artikel a in productContext.GetAllArtikelen())
                    {
                        if (a.ToString().IndexOf(tb_Search.Text, StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            zoekopdrachtArtikelen.Add(a);
                        }
                    }
                    if (lb_Artikelen != null)
                    {
                        lb_Artikelen.Items.Clear();
                        foreach (Artikel a in zoekopdrachtArtikelen)
                        {
                            lb_Artikelen.Items.Add(a.ToString());
                        }
                    }
                }

                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                lb_Artikelen.Items.Clear();
                foreach (Artikel a in productContext.GetAllArtikelen())
                {
                    lb_Artikelen.Items.Add(a.ToString());
                }
            }
        }

        private void btnArtToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productContext.InsertArtikel(new Artikel(Convert.ToInt32(cbProduct.Text.Split(' ')[0]), Convert.ToInt32(cbLeverancier.Text.Split(' ')[0])));
                MessageBox.Show("Artikel is toegevoegd!");
                lb_Artikelen.Items.Clear();
                foreach (Artikel a in productContext.GetAllArtikelen())
                {
                    lb_Artikelen.Items.Add(a.ToString());
                }
            }

            catch (System.Exception ex)
            {
                MessageBox.Show("Oeps! Er is iets verkeerd gegaan.");
                Console.WriteLine(ex.Message);
            }
        }

        private void btnArtReset_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.Text = "";
            cbLeverancier.Text = "";
        }

        private void btnWijzigingenOpslaan_Click(object sender, RoutedEventArgs e)
        {
            string leverancier = cbWijzigenLeverancier.Text;
            string product = cbWijzigenProduct.Text;
            if (!string.IsNullOrEmpty(cbWijzigenLeverancier.Text) && !string.IsNullOrEmpty(cbWijzigenProduct.Text))
            {
                productContext.UpdateArtikel(Convert.ToInt32(this.selectedArtikel.Split(' ')[0]), Convert.ToInt32(leverancier.Split(' ')[0]), Convert.ToInt32(product.Split(' ')[0]));
            }
            else
            {
                if (!string.IsNullOrEmpty(cbWijzigenLeverancier.Text))
                {
                    productContext.UpdateArtikel(Convert.ToInt32(this.selectedArtikel.Split(' ')[0]), Convert.ToInt32(leverancier.Split(' ')[0]), -1);
                }
                else if(!string.IsNullOrEmpty(cbWijzigenProduct.Text))
                {
                    productContext.UpdateArtikel(Convert.ToInt32(this.selectedArtikel.Split(' ')[0]), -1, Convert.ToInt32(product.Split(' ')[0]));
                }
                else
                {
                    MessageBox.Show("Vul gegevens in om aan te passen!");
                    return;
                }
            }
            MessageBox.Show("Artikel gewijzigd!");
            lb_Artikelen.Items.Clear();
            foreach (Artikel a in productContext.GetAllArtikelen())
            {
                lb_Artikelen.Items.Add(a.ToString());
            }
            cbWijzigenProduct.Text = "";
            cbWijzigenLeverancier.Text = "";
        }

        private void btnVerwijderArtikel_Click(object sender, RoutedEventArgs e)
        {
            productContext.VerwijderArtikelEnOrderregel(Convert.ToInt32(this.selectedArtikel.Split(' ')[0]));
            MessageBox.Show("Artikel verwijderd!");
            lb_Artikelen.Items.Clear();
            foreach (Artikel a in productContext.GetAllArtikelen())
            {
                lb_Artikelen.Items.Add(a.ToString());
            }
        }
    }
}
