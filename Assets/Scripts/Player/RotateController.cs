using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    
    [SerializeField] private float _rotationSpeed;
    
    [SerializeField] private Player _player;

    [SerializeField] private Camera _camera;

    private Quaternion _targetRotation;
    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {*/
            MouseRotate();
        //}
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

            
                /*Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                Vector3 motion = input;
                motion *=((Mathf.Approximately(Mathf.Abs(input.x), 1f)) && (Mathf.Approximately(Mathf.Abs(input.z), 1f)))
                    ? .7f
                    : 1;
                motion += Vector3.up * -8f;*/
    }
}
