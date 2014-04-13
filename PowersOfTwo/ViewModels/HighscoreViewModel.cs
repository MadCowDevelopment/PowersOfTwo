﻿using System.Windows.Input;
using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class HighscoreViewModel : ObservableObject
    {
        public HighscoreViewModel(MainWindowViewModel mainWindowViewModel)
        {
            BackToMainMenuCommand = new RelayCommand(p => mainWindowViewModel.ShowMainMenu());
        }

        public ICommand BackToMainMenuCommand { get; private set; }
    }
}
