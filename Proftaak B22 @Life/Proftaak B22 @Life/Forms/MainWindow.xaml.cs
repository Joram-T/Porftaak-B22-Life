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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proftaak_B22__Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccountSQLContext accountContext = new AccountSQLContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (accountContext.Login(tbEmail.Text, tbWachtwoord.Password) != null)
            {
                Proftaak_B22__Life.Forms.MenuForm mf = new Proftaak_B22__Life.Forms.MenuForm ();
                mf.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Onjuist Email of Wachtwoord");
            }
        }
    }
}
