using UnityEngine;

public sealed class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Camera _targetCamera;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private Player _player;
    
    public void LateUpdate()
    {
        this._targetCamera.transform.position = _player.GetPosition() + this._offset;
    }
}