using Mechanics;
using UnityEngine;

[RequireComponent(typeof(EnemyDeath))]
public class Health : MonoBehaviour
{
    private int CurrentHealth { get; set; }
    
    [HideInInspector] public int MaxHealth;

    private EnemyDeath _death;
    
    //TODO - DependencyCreate (Level - Awake) (class - Health)
    //TODO - Dependency (Level - Awake+GetComponent) (class - EnemyDeath)
    public void Awake()
    {
        _death = this.GetComponent<EnemyDeath>();
        ServiceLocator.Instance.BindData(typeof(Health), this);
    }

    public void DealDamage(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
        {
            _death.MakeDeath();
        }
    }

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }
}
