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
        private List<Window> actief = new List<Window>();
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnRooster_Click(object sender, RoutedEventArgs e)
        {
            RoosterForm rf = new RoosterForm(actief);
            if (CheckActive(rf))
            {
                actief.Add(rf);
                rf.Show();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            string messageBoxText = "Wilt u terug naar de login pagina?";
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    foreach(Window w in actief){
                        w.Close();
                    }
                    break;
                case MessageBoxResult.No:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
            
        }

        private bool CheckActive(Window w)
        {
            foreach(Window wi in actief)
            {
                if(wi.GetType() == w.GetType())
                {
                    return false;
                }
            }
            return true;
        }

        private void btnWerknemers_Click(object sender, RoutedEventArgs e)
        {
            MedewerkerForm mf = new MedewerkerForm(actief);
            if (CheckActive(mf))
            {
                actief.Add(mf);
                mf.Show();
            }
        }

        private void btnBestellingen_Click(object sender, RoutedEventArgs e)
        {
            BestellingForm bf = new BestellingForm(actief);
            if (CheckActive(bf))
            {
                actief.Add(bf);
                bf.Show();
            }
        }

        private void btnKlanten_Click(object sender, RoutedEventArgs e)
        {
            KlantForm kf = new KlantForm(actief);
            if (CheckActive(kf))
            {
                actief.Add(kf);
                kf.Show();
            }
        }
    }
}
