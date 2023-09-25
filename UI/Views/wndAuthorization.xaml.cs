using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        int countOfAuthorization = 2;

        public wndAuthorization()
        {
            InitializeComponent();
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            if (countOfAuthorization == 0)
            {
                MessageBox.Show("Вы использовали 3 попытки входа, приложение будет заблокировано на 5 секунд!");
                Thread.Sleep(5000);
                countOfAuthorization = 2;
                UI.Views.wndCaptha wnd = new wndCaptha();
                wnd.Show();
                this.Close();
                return;
            }
            if (txtLogin.Text == "" || passPassword.Password == "")
            {
                MessageBox.Show("Некоторые поля не заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                countOfAuthorization--;
                return;
            }
            else
            {
                var currentUser = Model.ConferenceOrganizationSystemEntities.GetContext().Users.FirstOrDefault(p => p.Id.ToString() == txtLogin.Text && p.Password == passPassword.Password);
                if (currentUser != null)
                {
                    switch (currentUser.IdRole)
                    {
                        case 1:
                            UI.Views.wndModerator wnd = new wndModerator();
                            wnd.Show();
                            this.Close();
                            break;
                        case 2:
                            UI.Views.wndJurie wnd1 = new wndJurie();
                            wnd1.Show();
                            this.Close();
                            break;
                        case 3:
                            Classes.User.user = currentUser;
                            UI.Views.wndOrganizator wnd2 = new wndOrganizator(currentUser);
                            wnd2.Show();
                            this.Close();
                            break;
                        case 4:
                            UI.Views.wndParticipant wnd3 = new wndParticipant();
                            wnd3.Show();
                            this.Close();
                            break;
                    }
                    try
                    {
                        Model.HistoryOfUsing user = new Model.HistoryOfUsing();
                        user.IdUser = currentUser.Id;
                        user.DateStart = DateTime.Now;
                        Model.ConferenceOrganizationSystemEntities.GetContext().HistoryOfUsings.Add(user);
                        Model.ConferenceOrganizationSystemEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    MessageBox.Show("Авторизация успешна!");
                }
                else
                {
                    countOfAuthorization--;
                    MessageBox.Show("Неверный пароль или логин!");
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            wndEvents wndEvents = new wndEvents();
            wndEvents.Show();
            this.Close();
        }
    }
}
