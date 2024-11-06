using UI.MVC.Model;
using UnityEngine;
using YG;

namespace Infrastructure.Services
{
    internal class DataSaver : IService
    {
        private ScoresStorage _scoresStorage;
        
        public DataSaver(ScoresStorage data)
        {
            _scoresStorage = data;
        }

        public void SaveResult()
        {
            long scores = _scoresStorage.Scores;
            int bestScore = PlayerPrefs.GetInt("BestScore", 0);
            if (scores > bestScore)
            {
                PlayerPrefs.SetInt("BestScore", (int)scores);
                YandexGame.NewLeaderboardScores("LBbestScore", scores);
                Debug.Log($"<color=yellow>New high score: {scores}</color>");
            }

            
            PlayerPrefs.SetInt("Score", (int)scores);
            PlayerPrefs.Save();

            Debug.Log($"<color=green>Result is {scores}</color>");
        }
        public void ResetResult()
        {
            _scoresStorage.Reset();
        }

    }
}

