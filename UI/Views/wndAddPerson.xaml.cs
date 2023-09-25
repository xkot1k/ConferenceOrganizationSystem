using ConferenceOrganizationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для wndAddPerson.xaml
    /// </summary>
    public partial class wndAddPerson : Window
    {
        private string[] genders = new string[] { "Мужской", "Женский" };
        private string[] roles = new string[] { "Jurie", "Moderator" };
        public wndAddPerson()
        {
            InitializeComponent();
            comboDirection.ItemsSource = Model.ConferenceOrganizationSystemEntities.GetContext().Directions.ToList();
            comboRole.ItemsSource = roles;
            comboEvent.ItemsSource = Model.ConferenceOrganizationSystemEntities.GetContext().Events.ToList();
            comboGender.ItemsSource = genders;
            comboDirection.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Collapsed;
            txtRePassword.Visibility = Visibility.Collapsed;
            AddId();
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            UI.Views.wndAuthorization wndAuthorization = new UI.Views.wndAuthorization();
            wndAuthorization.Show();
            this.Close();
        }

        private void chkVisiblePass_Checked(object sender, RoutedEventArgs e)
        {
            if (chkVisiblePass.IsChecked == true)
            {
                txtPassword.Text = passPassword.Password;
                txtRePassword.Text = passRePassword.Password;
                txtPassword.Visibility = Visibility.Visible;
                txtRePassword.Visibility = Visibility.Visible;
                passPassword.Visibility = Visibility.Collapsed;
                passRePassword.Visibility = Visibility.Collapsed;
            }
            else if (chkVisiblePass.IsChecked == false)
            {
                passPassword.Password = txtPassword.Text;
                passRePassword.Password = txtRePassword.Text;
                passPassword.Visibility = Visibility.Visible;
                passRePassword.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Collapsed;
                txtRePassword.Visibility = Visibility.Collapsed;
            }
        }

        public void RemoveText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        public void AddText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void chkAddToEvent_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAddToEvent.IsChecked == true)
            {
                txtDirection.Visibility = Visibility.Collapsed;
                comboDirection.Visibility = Visibility.Visible;
            }
            else if (chkAddToEvent.IsChecked == false)
            {
                txtDirection.Visibility = Visibility.Visible;
                comboDirection.Visibility = Visibility.Collapsed;
            }
        }

        private void AddId()
        {
            var lastUser = Model.ConferenceOrganizationSystemEntities.GetContext().Users.ToList().LastOrDefault();
            if (lastUser != null)
            {
                txtId.Text = (lastUser.Id + 1).ToString();
            }
            else { txtId.Text = 1.ToString(); }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            UI.Views.wndOrganizator wndOrganizator = new UI.Views.wndOrganizator(Classes.User.user);
            wndOrganizator.Show();
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (txtPassword.Text != txtRePassword.Text)
            {
                stringBuilder.AppendLine("Пароли не совпадают!");
            }
            else if (passPassword.Password != passRePassword.Password)
            {
                stringBuilder.AppendLine("Пароли не совпадают!");
            }
            if (txtFLP.Text == "")
            {
                stringBuilder.AppendLine("ФИО должно быть заполнено!");
            }
            if (comboGender.SelectedItem == null)
            {
                stringBuilder.AppendLine("Пол должен быть выбран!");
            }
            if (comboRole.SelectedItem == null)
            {
                stringBuilder.AppendLine("Роль должна быть выбрана!");
            }
            if (txtEmail.Text == "")
            {
                stringBuilder.AppendLine("Email должен быть заполнен!");
            }
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                stringBuilder.AppendLine("Email должен быть следующего вида: email@gmail.com");
            }
            if (txtPhone.Text == "")
            {
                stringBuilder.AppendLine("Телефон должен быть заполнен!");
            }
            if (comboEvent.SelectedItem == null)
            {
                stringBuilder.AppendLine("Мероприятие должно быть выбрано!");
            }
            if (chkAddToEvent.IsChecked == true)
            {
                if (comboDirection.SelectedItem == null)
                {
                    stringBuilder.AppendLine("Направление должно быть выбрано!");
                }
            }
            else if (chkAddToEvent.IsChecked == false)
            {
                if (txtDirection.Text == "")
                {
                    stringBuilder.AppendLine("Направление должно быть указано!");
                }
            }
            if (chkVisiblePass.IsChecked == true)
            {
                if (!IsPasswordValid(txtPassword.Text))
                {
                    stringBuilder.AppendLine("Пароль не соответствует требованиям!");
                }
            }
            else if (chkVisiblePass.IsChecked == false)
            {
                if (!IsPasswordValid(passPassword.Password))
                {
                    stringBuilder.AppendLine("Пароль не соответствует требованиям!");
                }
            }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (chkAddToEvent.IsChecked == false)
                {
                    Model.Direction newDirection = new Model.Direction();
                    newDirection.Name = txtDirection.Text;
                    try
                    {
                        Model.ConferenceOrganizationSystemEntities.GetContext().Directions.Add(newDirection);
                        Model.ConferenceOrganizationSystemEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                        return;
                    }

                    Model.User user = new Model.User();
                    string[] array = txtFLP.Text.Split(' ');
                    user.Nname = array[0];
                    user.Fname = array[1];
                    user.Patronomic = array[2];
                    user.Gender = comboGender.SelectedItem.ToString();
                    user.Email = txtEmail.Text;
                    user.Phone = txtPhone.Text;
                    user.DateOfBirthday = DateTime.Parse("07.07.2000");
                    user.IdCountry = 1;
                    chkVisiblePass.IsChecked = true;
                    user.Password = passPassword.Password;
                    var role = Model.ConferenceOrganizationSystemEntities.GetContext().Roles.FirstOrDefault(p => p.Name == comboRole.SelectedItem.ToString());
                    user.IdRole = role.Id;

                    var eventUser = (Model.Event)comboEvent.SelectedItem;

                    try
                    {
                        Model.ConferenceOrganizationSystemEntities.GetContext().Users.Add(user);
                        Model.ConferenceOrganizationSystemEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                        return;
                    }

                    AddInPersonalTable(txtId.Text, eventUser.Id, newDirection.Id, role.Id);
                }
                else
                {
                    Model.User user = new Model.User();
                    string[] array = txtFLP.Text.Split(' ');
                    user.Nname = array[0];
                    user.Fname = array[1];
                    user.Patronomic = array[2];
                    user.Gender = comboGender.SelectedItem.ToString();
                    user.Email = txtEmail.Text;
                    user.Phone = txtPhone.Text;
                    user.DateOfBirthday = DateTime.Parse("07.07.2000");
                    user.IdCountry = 1;
                    user.Password = txtPassword.Text;
                    var role = Model.ConferenceOrganizationSystemEntities.GetContext().Roles.FirstOrDefault(p => p.Name == comboRole.SelectedItem.ToString());
                    user.IdRole = role.Id;

                    var eventUser = (Model.Event)comboEvent.SelectedItem;
                    var direction = (Model.Direction)comboDirection.SelectedItem;

                    try
                    {
                        Model.ConferenceOrganizationSystemEntities.GetContext().Users.Add(user);
                        Model.ConferenceOrganizationSystemEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                        return;
                    }

                    AddInPersonalTable(txtId.Text, eventUser.Id, direction.Id, role.Id);
                }
            }
        }

        private void AddInPersonalTable(string txtId, int eventUser, int direction, int role)
        {
            if (role == 1)
            {
                Model.Moderator moderator = new Model.Moderator();
                moderator.Id = int.Parse(txtId);
                moderator.IdEvent = eventUser;
                moderator.IdDirection = direction;

                try
                {
                    Model.ConferenceOrganizationSystemEntities.GetContext().Moderators.Add(moderator);
                    Model.ConferenceOrganizationSystemEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные сохранены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    UI.Views.wndOrganizator wndOrganizator = new wndOrganizator(Classes.User.user);
                    wndOrganizator.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
            else
            {
                Model.Jurie jurie = new Model.Jurie();
                jurie.Id = int.Parse(txtId);
                jurie.IdDirection = direction;

                try
                {
                    Model.ConferenceOrganizationSystemEntities.GetContext().Juries.Add(jurie);
                    Model.ConferenceOrganizationSystemEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные сохранены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    UI.Views.wndOrganizator wndOrganizator = new wndOrganizator(Classes.User.user);
                    wndOrganizator.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
        }

        private bool IsPasswordValid(string password)
        {
            // Define a regular expression pattern for password validation
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$";

            return Regex.IsMatch(password, pattern);
        }

        private void comboEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentEvent = (Model.Event)comboEvent.SelectedItem;
            string nameOfPhoto = "../../Resources/Images/" + currentEvent.Photo;
            imgEvent.Source = new BitmapImage(new Uri(nameOfPhoto, UriKind.Relative));
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }
    }
}
