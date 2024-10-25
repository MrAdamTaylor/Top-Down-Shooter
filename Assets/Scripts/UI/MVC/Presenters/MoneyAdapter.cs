using System;
using UI.MVC.Model;

namespace UI.MVC.Presenters
{
    public class MoneyAdapter : IDisposable
    {
        private readonly CurrencyView _currencyView;
        private readonly MoneyStorage _moneyStorage;
        public MoneyAdapter(CurrencyView view, MoneyStorage ammoStorage)
        {
            _currencyView = view;
            _moneyStorage = ammoStorage;
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneyChanged += UpdateAmmo;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= UpdateAmmo;
        }

        private void UpdateAmmo(long value)
        {
            _currencyView.UpdateCurrency(value);
        }
    }
}