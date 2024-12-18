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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        public GameWindow(GameViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }

        private void Button_Click_After_Delete(object sender, RoutedEventArgs e)
        {
            var approve = App.ServiceProvider.GetRequiredService<GameDeleteModel>();
            GameDeleteWindow approveWindow = new GameDeleteWindow(approve);
            approveWindow.Show();

            this.Close();
        }

        private void Button_Click_Approve(object sender, RoutedEventArgs e)
        {
            var approve = App.ServiceProvider.GetRequiredService<ApproveGameViewModel>();
            ApproveGameWindow approveWindow = new ApproveGameWindow(approve);
            approveWindow.Show();

            this.Close();
        }
    }
}
