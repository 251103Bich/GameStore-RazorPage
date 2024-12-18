using BussinessLogic.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp.ViewModels
{
    public class ProfileSellerViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IProfileService _profleService;

        public List<string> Genders { get; } = new List<string> { "Male", "Female", "Other" };

        private Profile _SelectedProfile;
        public Profile SelectedProfile
        {
            get => _SelectedProfile;
            set
            {
                _SelectedProfile = value;
                IsNewProfile = _SelectedProfile == null || string.IsNullOrWhiteSpace(_SelectedProfile.Username);
                OnPropertyChanged(nameof(SelectedProfile));
                OnPropertyChanged(nameof(IsProfileIdReadOnly));
                CommandManager.InvalidateRequerySuggested(); // This triggers CanExecute to re-evaluate
            }
        }

        private bool _isNewProfile;
        public bool IsNewProfile
        {
            get => _isNewProfile;
            set
            {
                _isNewProfile = value;
                OnPropertyChanged(nameof(IsProfileIdReadOnly)); // Notify of change
            }
        }

        public bool IsProfileIdReadOnly => !IsNewProfile; // Read-only if not creating a new Profile

        public ObservableCollection<Profile> Profiles { get; set; } = new ObservableCollection<Profile>();

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public ProfileSellerViewModel(IProfileService ProfileService)
        {
            CreateCommand = new RelayCommand(async () => await CreateProfile(), CanExecuteCreateOrUpdate);
            UpdateCommand = new RelayCommand(async () => await UpdateProfile(), CanExecuteCreateOrUpdate);
            DeleteCommand = new RelayCommand(async () => await DeleteProfile(), () => SelectedProfile != null);
            ClearCommand = new RelayCommand(ClearSelection);

            _profleService = ProfileService;
            _ = LoadProfiles();
        }

        public async Task LoadProfiles()
        {
            try
            {
                var ProfileFromService = await _profleService.GetSellerAll();
                Profiles.Clear(); // Clear the existing ObservableCollection
                foreach (var profile in ProfileFromService)
                {
                    Profiles.Add(profile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading Profiles: {ex.Message}");
            }
        }

        private async Task CreateProfile()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SelectedProfile?.Username))
                {
                    MessageBox.Show("Please fill in all required fields before creating a user.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Exit the method early if validation fails
                }
                if (SelectedProfile != null)
                {
                    await _profleService.AddProfile(SelectedProfile);
                    await LoadProfiles();
                    ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating Profile: {ex.Message}");
            }
        }

        private async Task UpdateProfile()
        {
            try
            {
                // Ensure that a game is selected
                if (SelectedProfile == null)
                {
                    MessageBox.Show("Please select a user to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (SelectedProfile != null)
                {
                    await _profleService.UpdateProfile(SelectedProfile);
                    await LoadProfiles();
                    ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while updating Profile: {ex.Message}");
            }
        }

        private async Task DeleteProfile()
        {
            try
            {
                if (SelectedProfile != null)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        await _profleService.DeleteProfile(SelectedProfile.Username);
                        await LoadProfiles();
                        ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting Profile: {ex.Message}");
            }
        }

        private void ClearSelection()
        {
            SelectedProfile = new Profile(); // Clear the selected Profile
            IsNewProfile = true; // Set flag to indicate a new Profile is being created
            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanExecuteCreateOrUpdate()
        {
            // Ensure that all necessary fields in SelectedGame are filled out
            if (IsNewProfile)
            {
                // For creating a new game, ensure Gid and any other required fields are filled
                return !string.IsNullOrWhiteSpace(SelectedProfile?.Username);
            }
            else
            {
                // For updating an existing game, just ensure SelectedGame is not null
                return SelectedProfile != null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
