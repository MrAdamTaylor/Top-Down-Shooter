using System;

public class ScoresAdapter : IDisposable
{
    private readonly CurrencyView _currencyView;
    private readonly ScoresStorage _scoresStorage;
    
    public ScoresAdapter(CurrencyView currencyView, ScoresStorage scoresStorage)
    {
        _currencyView = currencyView;
        _scoresStorage = scoresStorage;
    }

    public void Initialize()
    {
        _scoresStorage.OnScoresChanged += UpdateScores;
    }

    public void Dispose()
    {
        _scoresStorage.OnScoresChanged -= UpdateScores;
    }

    private void UpdateScores(long value)
    {
        _currencyView.UpdateCurrency(value);
    }
}
