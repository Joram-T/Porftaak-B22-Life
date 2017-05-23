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
    /// Interaction logic for WijzigGegevens.xaml
    /// </summary>
    public partial class WijzigGegevens : Window
    {
        public WijzigGegevens()
        {
            InitializeComponent();
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbVoornaam.Clear();
            tbAchternaam.Clear();
            tbTussenvoegsel.Clear();
            tbAdres.Clear();
            tbWoonplaats.Clear();
        }
    }
}
