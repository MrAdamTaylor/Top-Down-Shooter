using UnityEngine;

namespace UI.MVC.View
{
    public class CurrencyProvider : MonoBehaviour
    {
        [SerializeField] private CurrencyViewWithImage _ammoView;
        [SerializeField] private CurrencyView _scoresView;
        [SerializeField] private ImageColorView _timerView;
        [SerializeField] private ImageColorView _waveView;
        [SerializeField] private ImageFillAmountView _imageFillAmountView;

        public ImageFillAmountView ImageFillAmountView => _imageFillAmountView;
    
        public CurrencyView ScoresView => _scoresView;
        public CurrencyViewWithImage AmmoView => _ammoView;
        public ImageColorView TimerView => _timerView;
        public ImageColorView WaveView => _waveView;
    }
}
