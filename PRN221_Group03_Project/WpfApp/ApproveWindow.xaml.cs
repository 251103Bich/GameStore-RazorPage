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
    /// Interaction logic for ApproveWindow.xaml
    /// </summary>
    public partial class ApproveWindow : Window
    {
        public ApproveWindow(ApproveSellerViewModel model)
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
    }
}
