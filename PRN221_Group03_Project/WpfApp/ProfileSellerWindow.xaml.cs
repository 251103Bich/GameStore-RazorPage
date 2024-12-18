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
    /// Interaction logic for ProfileSellerWindow.xaml
    /// </summary>
    public partial class ProfileSellerWindow : Window
    {
        public ProfileSellerWindow(ProfileSellerViewModel model)
        {
            InitializeComponent();
            DataContext = model; // Ensure the ViewModel is being passed correctly
        }


        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }
        private void Button_Click_Approve(object sender, RoutedEventArgs e)
        {
            var approve = App.ServiceProvider.GetRequiredService<ApproveSellerViewModel>();
            ApproveWindow approveWindow = new ApproveWindow(approve);
            approveWindow.Show();

            this.Close();
        }
        private void Button_Click_After_Delete(object sender, RoutedEventArgs e)
        {
            var approve = App.ServiceProvider.GetRequiredService<ProfileSellerDelete>();
            ProfileDeleteWindow approveWindow = new ProfileDeleteWindow(approve);
            approveWindow.Show();

            this.Close();
        }
    }
}
