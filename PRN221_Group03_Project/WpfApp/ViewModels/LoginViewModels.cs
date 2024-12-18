using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp.ViewModels
{
    public class LoginViewModels : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IProfileService _profileService;
        private string username;
        private string password;
        private string _errorMessage;
        private UserViewModels _userViewModels;

        public ICommand LoginCommand { get; }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public LoginViewModels(IProfileService profileService)
        {
            _profileService = profileService;
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            var user = await _profileService.LoginAdminn(Username, Password);
            if (user != null)
            {
                var homeModel = new HomeWindow();
                homeModel.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Login Failed! Invalid username or password!!!!");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
