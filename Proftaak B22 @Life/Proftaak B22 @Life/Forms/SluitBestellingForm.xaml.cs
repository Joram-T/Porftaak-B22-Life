using Proftaak_B22__Life.Class;
using Proftaak_B22__Life.DatabaseContext;
using Proftaak_B22__Life.Exception;
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
    /// Interaction logic for SluitBestellingForm.xaml
    /// </summary>
    public partial class SluitBestellingForm : Window
    {
        int selectedid;
        BestellingSQLContext bestellingcontext = new BestellingSQLContext();
        public SluitBestellingForm(int selectedid)
        {
            InitializeComponent();
            this.selectedid = selectedid;
        }

        private void btnSluitBestelling_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpLever.Text != "" && dpBetaal.Text != "")
                {
                    foreach (Bestelling b in bestellingcontext.GetOpenBestellingen())
                    {
                        if (b.Id == selectedid && dpLever.Text != "" && dpBetaal.Text != "")
                        {
                            bestellingcontext.SluitBestelling(b.Id, Convert.ToDateTime(dpLever.Text), Convert.ToDateTime(dpBetaal.Text));
                        }
                    }

                    foreach (Window w in Application.Current.Windows)
                    {
                        if (w.GetType() == typeof(BestellingForm))
                        {
                            (w as BestellingForm).FillLb();
                        }
                    }
                    this.Close();               
                }
                else if (dpBetaal.Text == "" && dpLever.Text != "")
                {
                    throw new SluitBestellingException("Vul een betaaldatum in");
                }
                else if (dpBetaal.Text != "" && dpLever.Text == "")
                {
                    throw new SluitBestellingException("Vul een leverdatum in");
                }
                else
                {
                    throw new SluitBestellingException("Vul een lever- en betaaldatum in");
                }
            }
            catch (SluitBestellingException se)
            {
                MessageBox.Show(se.Message);
            }
        }
    }
}
