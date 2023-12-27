using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
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
using System.Security.Cryptography;
using System.Diagnostics.Eventing.Reader;
using Electrichka.Model;
using Electrichka.Classes;

namespace Electrichka.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginBox.Text))
            {
                MessageBox.Show("Вы не ввели данные");
            }
            else
            {
                Model.Users users = new Users
                {
                    fullName = LoginBox.Text,
                    roleId = 2
                };

                ModelClasses.db.Users.Add(users);
                ModelClasses.db.SaveChanges();

                MessageBox.Show("Вы успешно зарегестрировались");
                NavigationService.Navigate(new Authorization());
            }
        }
    }
}
