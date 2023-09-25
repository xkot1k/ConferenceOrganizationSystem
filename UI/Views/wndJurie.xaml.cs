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

namespace ConferenceOrganizationSystem.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для wndJurie.xaml
    /// </summary>
    public partial class wndJurie : Window
    {
        public wndJurie()
        {
            InitializeComponent();
        }
        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            UI.Views.wndAuthorization wnd = new UI.Views.wndAuthorization();
            wnd.Show();
            this.Close();
        }
    }
}
