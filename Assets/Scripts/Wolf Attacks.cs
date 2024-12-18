using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttacks : EnemyAI
{
   
                                  
    public override void AttackPlayer()
    {
        {
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            animator.SetBool("IsWalking", false);

            if (!AlreadyAttacked)
            {

                //THE ATTACKING CODE SHOULD BE HERE 
                Debug.Log("D��man Sald�r�yor");
                animator.SetBool("IsAttacking", true);

                
               

                AlreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

    }
    
}
