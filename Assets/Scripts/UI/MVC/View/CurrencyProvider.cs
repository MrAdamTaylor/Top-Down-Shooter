using UnityEngine;

namespace UI.MVC
{
    public class CurrencyProvider : MonoBehaviour
    {
        [SerializeField] private CurrencyViewWithImage _ammoView;
        [SerializeField] private CurrencyView _scoresView;
        [SerializeField] private CurrencyView _timerView;
        [SerializeField] private CurrencyView _waveView;
        [SerializeField] private ImageFillAmountView _imageFillAmountView;

        public ImageFillAmountView ImageFillAmountView => _imageFillAmountView;
    
        public CurrencyView ScoresView => _scoresView;
        public CurrencyViewWithImage AmmoView => _ammoView;
        public CurrencyView TimerView => _timerView;
        public CurrencyView WaveView => _waveView;
    }
}
