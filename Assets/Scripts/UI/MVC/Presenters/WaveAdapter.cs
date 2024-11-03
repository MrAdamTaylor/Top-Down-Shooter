using Logic;
using Logic.Timer;
using UI.MVC.View;

namespace UI.MVC.Presenters
{
    public class WaveAdapter
    {
        private WaveSystem _waveSystem;
        private ImageColorView _view;
        private WaveTimer _waveTimer;
    
        public WaveAdapter(ImageColorView currencyProviderWaveView, WaveSystem waveSystem)
        {
            _waveSystem = waveSystem;
            _waveSystem.WaveChange += ChangeWave;
            _waveTimer = waveSystem.WaveTimer;
            _waveTimer.EndWaveAction += SwithColor;
            _view = currencyProviderWaveView;
        }

        private void SwithColor()
        {
            _view.ReturnOriginalColor();
        }

        private void ChangeWave(int waveCount)
        {
            _view.UpdateCurrency(waveCount);
            _view.SwitchColor();
        }
    }
}