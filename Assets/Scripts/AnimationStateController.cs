using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isWalkingBackHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkingBackHash = Animator.StringToHash("isWalkingBack");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
    }
    
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardpressed = Input.GetKey("w");
        if (!isWalking && forwardpressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardpressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        bool isWalkingBack = animator.GetBool(isWalkingBackHash);
        bool backpressed = Input.GetKey("s");
        if (!isWalkingBack && backpressed)
        {
            animator.SetBool(isWalkingBackHash, true);
        }

        if (isWalkingBack && !backpressed)
        {
            animator.SetBool(isWalkingBackHash, false);
        }
    }
}
