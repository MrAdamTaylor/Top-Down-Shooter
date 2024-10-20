using UnityEngine;

public class MouseRotateController : MonoBehaviour, IMouseRotateController
{
    
    [SerializeField] 
    private float _rotationSpeed = 450f;
    
    private Player _player;

    private Camera _camera;

    private Quaternion _targetRotation;

    public void Construct(Camera cam, Player player)
    {
        _camera = cam;
        _player = player;
    }

    private void Update()
    {
        MouseRotate();
    }

    private void MouseRotate()
    {
            Vector3 mousePos = Input.mousePosition;
            mousePos = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,
                _camera.transform.position.y - transform.position.y));
            _targetRotation =
                Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
            _player.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,
                _targetRotation.eulerAngles.y, _rotationSpeed * Time.deltaTime);
    }
}