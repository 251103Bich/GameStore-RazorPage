using BussinessLogic.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp.ViewModels
{
    public class UserViewModels : BaseViewModel, INotifyPropertyChanged


    {


        public List<string> Genders { get; } = new List<string> { "Male", "Female", "Orther" };
        public List<string> Type { get; } = new List<string> { "Customer", "Seller" };
        private readonly IProfileService _profileService;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        private Profile selectedUser;
        private bool _isNewUser;
        public bool IsUserNameReadOnly => !IsNewUser;
        public bool IsNewUser
        {
            get => _isNewUser;
            set
            {
                _isNewUser = value;
                OnPropertyChanged(nameof(IsNewUser));
                OnPropertyChanged(nameof(IsUserNameReadOnly));
            }
        }
        public Profile SelectedUser
        {
            get => selectedUser;
            set
            {

                selectedUser = value;
                IsNewUser = selectedUser == null;
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(IsUserNameReadOnly));
                CommandManager.InvalidateRequerySuggested();

            }
        }





        public ObservableCollection<Profile> Profiles { get; set; } = new ObservableCollection<Profile>();
        public UserViewModels(IProfileService profileService)
        {
            CreateCommand = new RelayCommand(async () => await Create(), CanExecuteCreate);
            UpdateCommand = new RelayCommand(async () => await Update(), CanExecuteUpdateOrDelete);
            DeleteCommand = new RelayCommand(async () => await Delete(), CanExecuteUpdateOrDelete);
            ClearCommand = new RelayCommand(ClearSelection);
            _profileService = profileService;
            _ = Load();
            ClearSelection();

        }



        public async Task Load()
        {
            try
            {
                var users = await _profileService.GetListAllProfile();
                Profiles.Clear();
                foreach (var item in users.Where(u => u.Type == ("Customer")))
                {


                    Profiles.Add(item);
                }

                Console.WriteLine($"Loaded {Profiles.Count} profiles.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\nDetails: {ex.InnerException?.Message}");
            }
        }

        private async Task Create()
        {
            try
            {
                if (SelectedUser != null)
                {
                    Debug.WriteLine(selectedUser.Gender);
                    await _profileService.AddProfile(SelectedUser);

                    await Load();
                    ClearSelection();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ịt xép sần");
                MessageBox.Show($"An error occurred: {ex.Message}\nDetails: {ex.InnerException?.Message}");
            }

            Debug.WriteLine("đéo chạy được");
        }

        private async Task Update()
        {
            try
            {
                if (SelectedUser != null)
                {
                    await _profileService.UpdateProfile(SelectedUser);
                    await Load();
                    ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\nDetails: {ex.InnerException?.Message}");
            }
        }

        private async Task Delete()
        {
            try
            {
                if (SelectedUser != null)
                {
                    await _profileService.DeleteProfile(SelectedUser.Username);
                    await Load();
                    ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\nDetails: {ex.InnerException?.Message}");
            }
        }

        private void ClearSelection()
        {
            SelectedUser = new Profile(); // Clear the selected student
            IsNewUser = true; // Set flag to indicate a new student is being created
            selectedUser.Date = DateTime.Now;
        }



        private bool CanExecuteCreate()
        {
            return IsNewUser;
        }

        private bool CanExecuteUpdateOrDelete()
        {
            return !IsNewUser && SelectedUser != null; ;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
