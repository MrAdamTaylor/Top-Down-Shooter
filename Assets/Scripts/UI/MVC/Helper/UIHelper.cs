using Infrastructure.ServiceLocator;
using UI.MVC.Model;
using UnityEngine;

namespace UI.MVC.Helper
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private long _current;

        private MoneyStorage _moneyStorage;
        private AmmoStorage _ammoStorage;
        private ScoresStorage _scoresStorage;

        public void Construct()
        {
            //_moneyStorage = (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage));
            //_scoresStorage = (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage));
        }
    

        public void AddMoney()
        {
            _moneyStorage.AddMoney(_current);
        }

        public void SpendMoney()
        {
            _moneyStorage.SpendMoney(_current);
        }
    
    
        public void AddScores()
        {
            _scoresStorage.AddScores(_current);
        }
        public void SpendScores()
        {
            _scoresStorage.SpendScores(_current);
        }
    }
}