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
    /// Логика взаимодействия для wndOrganizator.xaml
    /// </summary>
    public partial class wndOrganizator : Window
    {
        private Model.User currentUser;
        public wndOrganizator(Model.User user)
        {
            InitializeComponent();
            if (user != null)
            {
                currentUser = user;
                DataContext = currentUser;
            }
            showHelloToOrganizer();
        }

        private void showHelloToOrganizer()
        {
            string FLP = Classes.User.user.FLP;

            if (DateTime.Now >= DateTime.Parse("06:00:00") && DateTime.Now <= DateTime.Parse("10:00:00"))
            {
                txtPartOfDay.Text = "Доброе утро!";
                txtHello.Text = FLP;
            }
            else if (DateTime.Now >= DateTime.Parse("10:01:00") && DateTime.Now <= DateTime.Parse("18:00:00"))
            {
                txtPartOfDay.Text = "Добрый день!";
                txtHello.Text = FLP;
            }
            else if (DateTime.Now >= DateTime.Parse("18:01:00") && DateTime.Now <= DateTime.Parse("24:00:00"))
            {
                txtPartOfDay.Text = "Добрый вечер!";
                txtHello.Text = FLP;
            }
            else
            {
                txtPartOfDay.Text = "Здравствуйте!";
                txtHello.Text = FLP;
            }
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            UI.Views.wndAuthorization wnd = new UI.Views.wndAuthorization();
            wnd.Show();
            this.Close();
        }

        private void btnJurie_Click(object sender, RoutedEventArgs e)
        {
            UI.Views.wndAddPerson wndAddPerson = new UI.Views.wndAddPerson();
            wndAddPerson.Show();
            this.Close();
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            UI.Views.wndEvents wndEvents = new UI.Views.wndEvents();
            wndEvents.Show();
            this.Close();
        }

        private void btnParticipants_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная функция находится в разработке!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная функция находится в разработке!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
