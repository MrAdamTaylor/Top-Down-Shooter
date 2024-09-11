using Mechanics.BafMechaniks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    
    private float _innerSpeed;

    void Awake()
    {
        _innerSpeed = _speed;
        ServiceLocator.Instance.BindData(typeof(Player),this);
    }

    public void Move(Vector3 offset)
    {
        transform.position += offset * _innerSpeed;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SwitchSpeed(float speedChange)
    {
        _innerSpeed = _speed - (_speed * speedChange);
    }

    public void AddBonus<T>() where T : MonoBehaviour, IPlayerBonusComponent
    {
        gameObject.AddComponent<T>();
    }
}
