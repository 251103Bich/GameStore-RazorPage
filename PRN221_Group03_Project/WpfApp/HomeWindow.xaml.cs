using Microsoft.Extensions.DependencyInjection;
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
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Game(object sender, RoutedEventArgs e)
        {
            // Retrieve GameViewModel from the service provider
            var gameViewModel = App.ServiceProvider.GetRequiredService<GameViewModel>();

            // Create GameWindow with the ViewModel and show it
            GameWindow gameWindow = new GameWindow(gameViewModel);
            gameWindow.Show();

            // Close the current window
            this.Close();
        }

        private void Button_Click_Seller(object sender, RoutedEventArgs e)
        {
            // Retrieve ProfileSellerViewModel from the service provider
            var profileSellerViewModel = App.ServiceProvider.GetRequiredService<ProfileSellerViewModel>();

            // Create ProfileSellerWindow with the ViewModel and show it
            ProfileSellerWindow profileSellerWindow = new ProfileSellerWindow(profileSellerViewModel);
            profileSellerWindow.Show();

            // Close the current window
            this.Close();
        }

        private void Button_Click_Customer(object sender, RoutedEventArgs e)
        {
            // Retrieve ProfileSellerViewModel from the service provider
            var userViewModel = App.ServiceProvider.GetRequiredService<UserViewModels>();

            // Create ProfileSellerWindow with the ViewModel and show it
            WindowUser profileSellerWindow = new WindowUser(userViewModel);
            profileSellerWindow.Show();

            // Close the current window
            this.Close();
        }
    }
}
