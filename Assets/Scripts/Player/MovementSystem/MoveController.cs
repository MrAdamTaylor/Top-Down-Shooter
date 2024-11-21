using UnityEngine;

namespace Player.MovementSystem
{
    public class MoveController : MonoBehaviour
    {
        private Player _player;
        private IInputSystem _inputSystem;

        [SerializeField] private float moveSpeed = 7f;
        private Rigidbody _rb;

        public void Construct(Player player, IInputSystem inputSystem)
        {
            _player = player;
            _inputSystem = inputSystem;
            _inputSystem.OnMove += this.OnMove;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotation; // предотвращает вращение при столкновении
        }

        private void OnMove(Vector2 direction)
        {
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y).normalized; // нормализуем направление
            Vector3 targetVelocity = moveDirection * moveSpeed; // создаем целевую скорость

            _rb.velocity = new Vector3(targetVelocity.x, _rb.velocity.y, targetVelocity.z); // применяем постоянную скорость к Rigidbody
        }
    }
}
