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
    /// Interaction logic for MenuForm.xaml
    /// </summary>
    public partial class MenuForm : Window
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnRooster_Click(object sender, RoutedEventArgs e)
        {
            RoosterForm rf = new RoosterForm();
            rf.Show();
            this.Close();
        }

        private void btnWerknemers_Click(object sender, RoutedEventArgs e)
        {
            MedewerkerForm wf = new MedewerkerForm();
            wf.Show();
            this.Close();
        }
    }
}
