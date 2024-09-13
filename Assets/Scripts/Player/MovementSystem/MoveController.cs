using UnityEngine;

public enum EInputSystem
{
    OldSystem,
    NewSystem
}


public class MoveController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EInputSystem _inputSystmType;

    private IInputSystem _inputSystem;

    public void Construct(Player player, IInputSystem inputSystem)
    {
        _player = player;
        _inputSystem = inputSystem;
        _inputSystem.OnMove += this.OnMove;
    }
    
    public void Awake()
    {
        /*if (_inputSystmType == EInputSystem.OldSystem)
        {
            _inputSystem = gameObject.AddComponent<KeyboardInput>();
        }
        else
        {
            _inputSystem = gameObject.AddComponent<AxisInputSystem>();
        }

        _inputSystem.OnMove += this.OnMove;*/
    }
    
    private void OnMove(Vector2 direction)
    {
        var offset = new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
        _player.Move(offset);
    }
}