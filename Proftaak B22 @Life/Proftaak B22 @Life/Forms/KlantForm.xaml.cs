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
        private List<Window> actief;
        public KlantForm(List<Window> actief)
        {
            InitializeComponent();
            this.actief = actief;
            foreach (Klant klant in klantContext.GetAllKlanten())
            {
                lb_Klanten.Items.Add(klant.ToString());
            }
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

        }
    }
}
