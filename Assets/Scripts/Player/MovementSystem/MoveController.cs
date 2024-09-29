using UnityEngine;

/*public enum EInputSystem
{
    OldSystem,
    NewSystem
}*/


public class MoveController : MonoBehaviour
{
    private Player _player;

    private IInputSystem _inputSystem;

    public void Construct(Player player, IInputSystem inputSystem)
    {
        _player = player;
        _inputSystem = inputSystem;
        _inputSystem.OnMove += this.OnMove;
    }
    
    private void OnMove(Vector2 direction)
    {
        var offset = new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
        _player.Move(offset);
    }
}