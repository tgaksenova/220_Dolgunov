using Electrichka.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Electrichka.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            var CurrentUser = ModelClasses.db.Users.FirstOrDefault( x => x.fullName == LoginBox.Text);

            if(CurrentUser != null)
            {
                switch(CurrentUser.Roles.name)
                {
                    case "Кассир":
                        NavigationService.Navigate(new BuyBilet());
                        break;
                    case "Пассажир":
                        NavigationService.Navigate(new PokupkaBiletov());
                        break;
                }
            }
            else
            {
                MessageBox.Show("Невернно введены данные");
            }
        }

        private void RegistrarionBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
