using System;

public class AmmoAdapter : IDisposable
{
    private readonly CurrencyView _currencyView;
    private readonly AmmoStorage _scoresStorage;
    public AmmoAdapter(CurrencyView view, AmmoStorage ammoStorage)
    {
        _currencyView = view;
        _scoresStorage = ammoStorage;
    }
    
    public void Initialize()
    {
        _scoresStorage.OnAmmoChanged += UpdateAmmo;
    }

    private void UpdateAmmo(long value)
    {
        _currencyView.UpdateCurrency(value);
    }

    public void Dispose()
    {
        _scoresStorage.OnAmmoChanged -= UpdateAmmo;
    }
}