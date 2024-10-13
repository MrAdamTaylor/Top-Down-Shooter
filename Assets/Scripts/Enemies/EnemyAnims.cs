using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnims : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemyAttack()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            animator.SetInteger("AttackIndex", Random.Range(0, 3));
            animator.SetTrigger("Attack");
        }
    }

    IEnumerator EnemyDeath()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            animator.SetInteger("DeathIndex", Random.Range(0, 3));
            animator.SetTrigger("EnemyDeath");
        }
    }
}
