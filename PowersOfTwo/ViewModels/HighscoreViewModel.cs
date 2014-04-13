using System.Windows.Input;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class HighscoreViewModel : ObservableObject
    {
        public HighscoreViewModel(MainWindowViewModel mainWindowViewModel, int? currentScore)
        {
            InitializeHighscores(currentScore);
            BackToMainMenuCommand = new RelayCommand(p => mainWindowViewModel.ShowMainMenu());
        }

        private void InitializeHighscores(int? currentScore)
        {
            var highScoreService = new HighscoreService();
            if (currentScore.HasValue) highScoreService.AddScore(currentScore.Value);
            var scores = highScoreService.GetScores();
            if (scores.Count >= 1) Points1 = scores[0];
            if (scores.Count >= 2) Points2 = scores[1];
            if (scores.Count >= 3) Points3 = scores[2];
            PointsCurrent = currentScore;
        }

        public int? Points1 { get; private set; }
        public int? Points2 { get; private set; }
        public int? Points3 { get; private set; }
        public int? PointsCurrent { get; private set; }

        public ICommand BackToMainMenuCommand { get; private set; }
    }
}
