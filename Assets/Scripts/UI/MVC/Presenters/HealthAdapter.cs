using Player;

namespace UI.MVC.Presenters
{
    public class HealthAdapter
    {
        private PlayerHealth _playerHealth;
        private ImageFillAmountView _imageFillAmountView;
    
        public HealthAdapter(PlayerHealth health, ImageFillAmountView imageFillAmountView)
        {
            _playerHealth = health;
            _imageFillAmountView = imageFillAmountView;
        }

        public void UpdateValues(float current, float maxHp)
        {
            _imageFillAmountView.SetValue(current, maxHp);
        }
    }
}