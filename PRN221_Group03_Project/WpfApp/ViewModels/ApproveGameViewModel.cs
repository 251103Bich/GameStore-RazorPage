using BussinessLogic.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace WpfApp.ViewModels
{
    public class ApproveGameViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IGameService _profileService;

        private Game _selectedProfile;
        public Game SelectedProfile
        {
            get => _selectedProfile;
            set
            {
                if (_selectedProfile != value)
                {
                    _selectedProfile = value;
                    OnPropertyChanged(nameof(SelectedProfile));
                    CommandManager.InvalidateRequerySuggested(); // Re-evaluate CanExecute for commands
                }
            }
        }

        public ObservableCollection<Game> Profiles { get; set; } = new ObservableCollection<Game>();

        public ICommand ApproveCommand { get; }

        public ApproveGameViewModel(IGameService profileService)
        {
            _profileService = profileService;
            ApproveCommand = new RelayCommand(async () => await ApproveSelectedProfile(), () => SelectedProfile != null);
            _ = LoadProfiles(); // Fire and forget the loading
        }

        private async Task LoadProfiles()
        {
            try
            {
                var profilesFromService = await _profileService.GetGameApproveAll();
                Profiles.Clear();
                foreach (var profile in profilesFromService)
                {
                    Profiles.Add(profile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading profiles: {ex.Message}");
            }
        }

        private async Task ApproveSelectedProfile()
        {
            if (SelectedProfile == null)
            {
                MessageBox.Show("Please select a profile to approve.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _profileService.ApproveGame(SelectedProfile.Gid);
                MessageBox.Show("Seller approved successfully.");
                await LoadProfiles(); // Refresh the list after approval
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while approving profile: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
