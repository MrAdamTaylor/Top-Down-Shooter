using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private IInputSystem _inputSystem;
    [SerializeField]
    private Player _player;


    
    public void Awake()
    {
        _inputSystem = gameObject.AddComponent<KeyboardInput>();
        _inputSystem.OnMove += this.OnMove;
    }
    
    private void OnMove(Vector2 direction)
    {
        var offset = new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
        _player.Move(offset);
    }
}