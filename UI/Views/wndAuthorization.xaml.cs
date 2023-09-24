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
    /// Логика взаимодействия для wndAuthorization.xaml
    /// </summary>
    public partial class wndAuthorization : Window
    {
        public wndAuthorization()
        {
            InitializeComponent();
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            wndEvents wndEvents = new wndEvents();
            wndEvents.Show();
            this.Close();
        }
    }
}
