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
        string selectedLeverancier = "";
        string selectedArtikel = "";
        string selectedProduct = "";

        public ProductForm(List<Window> actief)
        {
            InitializeComponent();
            foreach (Artikel a in productContext.GetAllArtikelen())
            {
                lb_Artikelen.Items.Add(a.ToString());
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

        private void lb_Artikelen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Artikelen.SelectedIndex != -1)
            {
                selectedArtikel = lb_Artikelen.SelectedItem.ToString();
                int id = Convert.ToInt32(selectedLeverancier.Split(' ')[2]);
                lblLeverancier.Content = leverancierContext.GetLeverancierByID(id).Name;
                
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
                foreach (Artikel a in productContext.GetAllArtikelen())
                {
                    if (a.ToString().IndexOf(tb_Search.Text, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        zoekopdrachtArtikelen.Add(a);
                    }
                }
                lb_Artikelen.Items.Clear();
                foreach (Artikel a in zoekopdrachtArtikelen)
                {
                    lb_Artikelen.Items.Add(a.ToString());
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

        }

        private void btnArtReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
