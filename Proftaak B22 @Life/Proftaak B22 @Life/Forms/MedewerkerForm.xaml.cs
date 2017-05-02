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
    /// Interaction logic for MedewerkerForm.xaml
    /// </summary>
    public partial class MedewerkerForm : Window
    {
        private MedewerkerSQLContext medewerkerContext = new MedewerkerSQLContext();
        private List<Window> actief;
        public MedewerkerForm(List<Window> actief)
        {
            InitializeComponent();
            foreach (Medewerker m in medewerkerContext.GetAllMedewerkers())
            {
                lb_Werknemers.Items.Add(m.ToString());
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
    }
}
