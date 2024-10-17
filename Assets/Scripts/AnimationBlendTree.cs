using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlendTree : MonoBehaviour
{

    Animator animator;
    Vector2 input;
    public float smoothBlend = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

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
  /*  void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }
    private float m_LastHorizontal, m_LastVertical;
    private float m_AnimMultiplier;
}
   private void Animate(Vector2 input)
    {
        float targetHorizontal = input.x * m_AnimMultiplier;
        if (targetHorizontal > m_LastHorizontal)
        {
            m_LastHorizontal += Time.deltaTime;
        }
        else if (targetHorizontal < m_LastHorizontal)
        {
            m_LastHorizontal -= Time.deltaTime;
        }
        float targetVertical = input.y * m_AnimMultiplier;
        if (targetVertical > m_LastVertical)
        {
            m_LastVertical += Time.deltaTime;
        }
        else if (targetVertical < m_LastVertical)
        {
            m_LastVertical -= Time.deltaTime;
        }
        animator.SetFloat("horizontal", m_LastHorizontal);
        animator.SetFloat("vertical", m_LastVertical);
    }
}
  */