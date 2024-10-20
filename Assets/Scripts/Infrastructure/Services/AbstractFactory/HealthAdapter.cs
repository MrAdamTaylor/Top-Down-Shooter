using EnterpriceLogic.Constants;
using UI.MVC;

public class HealthAdapter
{
    private PlayerHealth _playerHealth;
    private HpBar _hpBar;
    
    public HealthAdapter(PlayerHealth health, HpBar hpBar)
    {
        _playerHealth = health;
        _hpBar = hpBar;
    }

    public void UpdateValues(float current, float maxHp)
    {
        _hpBar.SetValue(current, maxHp);
    }
}