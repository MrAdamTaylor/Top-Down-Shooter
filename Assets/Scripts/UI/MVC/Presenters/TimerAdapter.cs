using System;
using System.Text;
using Infrastructure.ServiceLocator;
using Logic.Timer;
using UI.MVC.Model;
using UI.MVC.View;
using UnityEngine;

namespace UI.MVC.Presenters
{
    public class TimerAdapter : IDisposable
    {
        private const int SECONDS_IN_MINUTE = 60;
        private const int MIN_TIME = 5;
        private readonly ImageColorView _currencyView;

        private GameTimer _gameTimer;
        private WaveTimer _waveTimer;

        private StringBuilder _stringBuilder;
        //private readonly MoneyStorage _moneyStorage;
        /*public TimerAdapter(CurrencyView view, MoneyStorage ammoStorage)
        {
            _currencyView = view;
            //_moneyStorage = ammoStorage;
        }*/

        public TimerAdapter(ImageColorView view, GameTimer gameTimer, WaveTimer waveTimer)
        {
            _stringBuilder = new StringBuilder();
            //_stringBuilder.Append("");
            _currencyView = view;
            _gameTimer = gameTimer;
            _waveTimer = waveTimer;
            ServiceLocator.Instance.BindData(typeof(TimerAdapter), this);
        }

        public void Initialize()
        {
            if (_gameTimer.IsActive)
            {
                _gameTimer.Subscribe(UpdateAmmo);
            }

            if (_waveTimer.IsActive)
            {
                _waveTimer.Subscribe(UpdateAmmo);
            }

            _gameTimer.GameTimerFinish += ChangeToWaveTimer;
            _waveTimer.EndWaveAction += ChangeToGameTimer;
        }

        private void ChangeToGameTimer()
        {
            _waveTimer.UnSubscribe(UpdateAmmo);
            _gameTimer.Subscribe(UpdateAmmo);
            _currencyView.ReturnOriginalColor();
        }

        private void ChangeToWaveTimer()
        {
            _gameTimer.Unsubscribe(UpdateAmmo);
            _waveTimer.Subscribe(UpdateAmmo);
            _currencyView.ReturnOriginalColor();
        }

        public void Dispose()
        {
            _gameTimer.Unsubscribe(UpdateAmmo);
            _waveTimer.UnSubscribe(UpdateAmmo);
        }

        private void UpdateAmmo(float value)
        {
            int minutes = Mathf.FloorToInt(value / SECONDS_IN_MINUTE);
            int seconds = Mathf.FloorToInt(value % SECONDS_IN_MINUTE);

            string rowSeconds = GetRowSeconds(seconds);
            string time = minutes.ToString() + ":" + rowSeconds;
            
            _stringBuilder.Clear();
            _stringBuilder.Append(time);
            _currencyView.UpdateCurrency(_stringBuilder.ToString());
            
            if(value < MIN_TIME)
                _currencyView.SwitchColor();
            //_currencyView.UpdateCurrency((long)value);
        }

        private string GetRowSeconds(int seconds)
        {
            string row;
            if (seconds < 10)
                row = '0' + seconds.ToString();
            else
                row = seconds.ToString();

            return row;
        }
    }
}