using UnityEngine;

namespace Player.MovementSystem
{
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

        [SerializeField] private float moveSpeed = 5f;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnMove(Vector2 direction)
        {
            Vector3 offset = new Vector3(direction.x, 0, direction.y) * moveSpeed * Time.deltaTime;
            Vector3 newPosition = _rb.position + offset;

            _rb.MovePosition(newPosition);
        }

    }
}