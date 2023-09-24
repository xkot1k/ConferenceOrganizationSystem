using ConferenceOrganizationSystem.Model;
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
    /// Логика взаимодействия для wndEvents.xaml
    /// </summary>
    public partial class wndEvents : Window
    {
        public wndEvents()
        {
            InitializeComponent();
            listEvents.ItemsSource = Model.ConferenceOrganizationSystemEntities.GetContext().Events.ToList();
            var directions = Model.ConferenceOrganizationSystemEntities.GetContext().Directions.ToList();
            directions.Insert(0, new Model.Direction { Name = "Все направления" });
            comboDirection.SelectedIndex = 0;
            comboDirection.ItemsSource = directions;
        }

        private void comboDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var events = Model.ConferenceOrganizationSystemEntities.GetContext().Events.ToList();
            if (comboDirection.SelectedIndex > 0)
            {
                var direction = (Model.Direction)comboDirection.SelectedItem;
                events = events.Where(p => p.IdDirection == direction.Id).ToList();
            }
            if (dateDateSelect.SelectedDate != null)
            {
                events = events.Where(p => p.Date == dateDateSelect.SelectedDate).ToList();
            }
            listEvents.ItemsSource = events;
            if (events.Count == 0)
            {
                txtResult.Visibility = Visibility.Visible;
                listEvents.Visibility = Visibility.Collapsed;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            comboDirection.SelectedIndex = 0;
            dateDateSelect.SelectedDate = null;
            listEvents.ItemsSource = Model.ConferenceOrganizationSystemEntities.GetContext().Events.ToList();
            txtResult.Visibility = Visibility.Collapsed;
            listEvents.Visibility = Visibility.Visible;
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            wndAuthorization wndAuthorization = new wndAuthorization();
            wndAuthorization.Show();
            this.Close();
        }
    }
}
