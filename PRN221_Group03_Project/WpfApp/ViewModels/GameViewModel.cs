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
    public class GameViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IGameService _gameService;

        private Game _selectedGame;
        public Game SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                IsNewGame = _selectedGame == null || string.IsNullOrWhiteSpace(_selectedGame.Gid);
                OnPropertyChanged(nameof(SelectedGame));
                OnPropertyChanged(nameof(IsGameIdReadOnly));
                CommandManager.InvalidateRequerySuggested(); // This triggers CanExecute to re-evaluate
            }
        }

        private bool _isNewGame;
        public bool IsNewGame
        {
            get => _isNewGame;
            set
            {
                _isNewGame = value;
                OnPropertyChanged(nameof(IsGameIdReadOnly)); // Notify of change
            }
        }

        public bool IsGameIdReadOnly => !IsNewGame; // Read-only if not creating a new Game

        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public GameViewModel(IGameService GameService)
        {
            //CreateCommand = new RelayCommand(async () => await CreateGame(), CanExecuteCreateOrUpdate);
            //UpdateCommand = new RelayCommand(async () => await UpdateGame(), CanExecuteCreateOrUpdate);
            DeleteCommand = new RelayCommand(async () => await DeleteGame(), () => SelectedGame != null);
            ClearCommand = new RelayCommand(ClearSelection);

            _gameService = GameService;
            _ = LoadGames();
        }

        public async Task LoadGames()
        {
            try
            {
                var GamesFromService = await _gameService.GetListAllGame();
                Games.Clear(); // Clear the existing ObservableCollection
                foreach (var Game in GamesFromService)
                {
                    Games.Add(Game);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading Games: {ex.Message}");
            }
        }

        //private async Task CreateGame()
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(SelectedGame?.Gid))
        //        {
        //            MessageBox.Show("Please fill in all required fields before creating a game.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return; // Exit the method early if validation fails
        //        }
                    
        //        if (SelectedGame != null)
        //        {
        //            await _gameService.AddGame(SelectedGame);
        //            await LoadGames();
        //            ClearSelection();
        //            if (!string.IsNullOrWhiteSpace(SelectedGame?.Gid))
        //            {
        //                MessageBox.Show("Please fill in all required fields before creating a game.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                return; // Exit the method early if validation fails
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error while creating Game: {ex.Message}");
        //    }
        //}

        //private async Task UpdateGame()
        //{
        //    try
        //    {
        //        // Ensure that a game is selected
        //        if (SelectedGame == null)
        //        {
        //            MessageBox.Show("Please select a game to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }

        //        if (SelectedGame != null)
        //        {
        //            await _gameService.UpdateGame(SelectedGame);
        //            await LoadGames();
        //            ClearSelection();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error while updating Game: {ex.Message}");
        //    }
        //}

        private async Task DeleteGame()
        {
            try
            {
                if (SelectedGame != null)
                {
                    // Confirm deletion
                    var result = MessageBox.Show("Are you sure you want to delete this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        await _gameService.DeleteGame(SelectedGame.Gid);
                        await LoadGames();
                        ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting Game: {ex.Message}");
            }
        }

        private void ClearSelection()
        {
            SelectedGame = new Game(); // Clear the selected Game
            IsNewGame = true; // Set flag to indicate a new Game is being created
            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanExecuteCreateOrUpdate()
        {
            // Ensure that all necessary fields in SelectedGame are filled out
            if (IsNewGame)
            {
                // For creating a new game, ensure Gid and any other required fields are filled
                return !string.IsNullOrWhiteSpace(SelectedGame?.Gid);
            }
            else
            {
                // For updating an existing game, just ensure SelectedGame is not null
                return SelectedGame != null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
