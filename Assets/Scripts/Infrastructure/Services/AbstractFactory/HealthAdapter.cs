using EnterpriceLogic.Constants;
using UI.MVC;

public class HealthAdapter
{
    private PlayableHealth _playableHealth;
    private HpBar _hpBar;
    
    public HealthAdapter(PlayableHealth health, HpBar hpBar)
    {
        _playableHealth = health;
        _hpBar = hpBar;
    }

    public void UpdateValues(float current)
    {
        _hpBar.SetValue(current, Constants.PLAYER_HP);
    }
}