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
    /// Interaction logic for WijzigLeverancierGegevens.xaml
    /// </summary>
    public partial class WijzigLeverancierGegevens : Window
    {
        LeverancierSQLContext leverancierContext = new LeverancierSQLContext();
        Leverancier Leverancier;
        public WijzigLeverancierGegevens(Leverancier leverancier)
        {
            InitializeComponent();
            this.Leverancier = leverancier;
        }

        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            string levnaam = tbLevNaam.Text;
            string levadres = tbLevAdres.Text;
            string levstad = tbLevStad.Text;
            if (string.IsNullOrEmpty(tbLevNaam.Text))
            {
                levnaam = null;
            }
            if (string.IsNullOrEmpty(tbLevAdres.Text))
            {
                levadres = null;
            }
            if (string.IsNullOrEmpty(tbLevStad.Text))
            {
                levstad = null;
            }
            try
            {
                leverancierContext.UpdateLeverancier(this.Leverancier, levnaam, levadres, levstad);
                this.Close();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Oeps, er is iets fout gegaan!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbLevAdres.Clear();
            tbLevNaam.Clear();
            tbLevStad.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(LeverancierForm))
                {
                    (window as LeverancierForm).RefreshLeverancierData();
                }
            }
        }
    }
}
