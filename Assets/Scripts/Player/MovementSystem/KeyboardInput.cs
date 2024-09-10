using System;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputSystem
{
    [SerializeField]private double zBorder = 15;
    [SerializeField]private double xBorder = 20;


    public event Action<Vector2> OnMove;
    

    public void Update()
    {
        HandleKeyboard();
    }

    private void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }

    private void HandleKeyboard()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (this.transform.position.z <= zBorder)
            {
                this.Move(Vector2.up);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (this.transform.position.z >= -zBorder)
            {
                this.Move(Vector2.down);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.position.x >= -xBorder)
            {
                this.Move(Vector2.left);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.position.x <= xBorder)
            {
                this.Move(Vector2.right);
            }
        }
    }
}