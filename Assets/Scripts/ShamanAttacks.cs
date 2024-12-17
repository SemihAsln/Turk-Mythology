using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanAttacks : EnemyAI
{
    public Transform arrowspawnpoint;
    public GameObject arrowPrefab;
    public float bulletSpeed = 10; //for magic ball
                                  
    public override void AttackPlayer()
    {
        {
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            animator.SetBool("IsWalking", false);

            if (!AlreadyAttacked)
            {

                //THE ATTACKING CODE SHOULD BE HERE 
                Debug.Log("Düþman Saldýrýyor");
                animator.SetBool("IsAttacking", true);

                
                var magicsphere = Instantiate(arrowPrefab, arrowspawnpoint.position, arrowspawnpoint.rotation);
                magicsphere.GetComponent<Rigidbody>().velocity = arrowspawnpoint.forward * bulletSpeed;
                Destroy(magicsphere, 4f);

                AlreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

    }
    
}
