using UI.MVC.Model;
using UnityEngine;

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
            //TODO wirte data in playerPref
            Debug.Log($"<color=green>Result is {scores}</color>");
        }
    }
}

