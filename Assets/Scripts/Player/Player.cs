using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private float _innerSpeed;

    public void Move(Vector3 offset)
    {
        transform.position += offset * _innerSpeed;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Construct(float speed)
    {
        _speed = speed;
        _innerSpeed = _speed;
        ServiceLocator.Instance.BindData(typeof(Player),this);
    }
}
