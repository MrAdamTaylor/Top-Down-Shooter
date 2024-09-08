using System;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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

    private void UpdateScores(long value)
    {
        _currencyView.UpdateCurrency(value);
    }

    public void Dispose()
    {
        _scoresStorage.OnScoresChanged -= UpdateScores;
    }
}
