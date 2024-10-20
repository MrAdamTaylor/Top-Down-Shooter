using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlendTree : MonoBehaviour
{

    Animator animator;
    Vector2 input;
    public float smoothBlend = 0.1f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    

    void Update()
    {
        Move(input.x = Input.GetAxis("Horizontal"), input.y = Input.GetAxis("Vertical"));
    }

    void Move(float x, float y)
    {
        Vector3 direction = new Vector3(x, 0, y);
        direction = transform.InverseTransformDirection(direction);

        animator.SetFloat("InputX", direction.x, smoothBlend, Time.deltaTime);
        animator.SetFloat("InputY", direction.z, smoothBlend, Time.deltaTime);
    }
}
  