using UnityEngine;

namespace Player
{
    public class AnimationBlendTree : MonoBehaviour
    {
        private Animator animator;
        private Vector2 input;
        public float smoothBlend = 0.1f;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Move(input.x = Input.GetAxis("Horizontal"), input.y = Input.GetAxis("Vertical"));
        }

        private void Move(float x, float y)
        {
            Vector3 direction = new Vector3(x, 0, y);
            direction = transform.InverseTransformDirection(direction);

            animator.SetFloat("InputX", direction.x, smoothBlend, Time.deltaTime);
            animator.SetFloat("InputY", direction.z, smoothBlend, Time.deltaTime);
        }
    }
}
  