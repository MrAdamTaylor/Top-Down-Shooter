using UnityEngine;

namespace UI.MVC
{
    public class CurrencyProvider : MonoBehaviour
    {
        [SerializeField] private CurrencyViewWithImage _ammoView;
        [SerializeField] private CurrencyView _scoresView;
        [SerializeField] private CurrencyView _moneyView;
        [SerializeField] private HpBar _hpBar;

        public HpBar HpBar => _hpBar;
    
        public CurrencyView ScoresView => _scoresView;
        public CurrencyViewWithImage AmmoView => _ammoView;
        public CurrencyView MoneyView => _moneyView;
    }
}
