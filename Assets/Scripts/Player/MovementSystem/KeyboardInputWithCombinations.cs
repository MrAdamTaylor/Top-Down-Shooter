using System;
using UnityEngine;

public class KeyboardInputWithCombinations : MonoBehaviour, IInputSystem
{
    public event Action<Vector2> OnMove;
    
    /*public void OnUpdate(float timeDelta)
    {
        this.HandleKeyboard();
    }*/

    public void Update()
    {
        this.HandleKeyboard();
    }

    private void Move(Vector2 direction)
    {
        this.OnMove?.Invoke(direction);
    }

    private void HandleKeyboard()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.Move(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.Move(Vector2.down);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.Move(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.Move(Vector2.right);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            this.Move(Vector2.up + Vector2.down); 
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            this.Move(Vector2.up + Vector2.left); 
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            this.Move(Vector2.up + Vector2.right); 
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            this.Move(Vector2.down + Vector2.left); 
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            this.Move(Vector2.down + Vector2.right); 
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            this.Move(Vector2.left + Vector2.right); 
        }
    }
}