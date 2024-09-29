using EnterpriceLogic.Constants;
using UnityEngine;

public sealed class CameraFollower : MonoBehaviour
{
    //[SerializeField]
    private Camera _targetCamera;

    [SerializeField] private Vector3 _offset = Constants.CAMERA_POSITION; //new Vector3(0,5f,0);

    //[SerializeField]
    private Player _player;

    public void Construct(Camera camera, Player player)
    {
        _targetCamera = camera;
        _player = player;
    }

    public void LateUpdate()
    {
        this._targetCamera.transform.position = _player.GetPosition() + this._offset;
    }
}