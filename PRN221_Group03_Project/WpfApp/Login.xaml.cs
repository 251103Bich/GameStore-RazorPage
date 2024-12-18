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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login(LoginViewModels login)
        {
            InitializeComponent();
            DataContext = login;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModels viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
