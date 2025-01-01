using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileþeni
    protected NavMeshAgent agent; // NavMeshAgent bileþeni

    public Animator animator;

    public LayerMask whatIsGround,whatIsPlayer;

    //Patrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking 
    public float timeBetweenAttacks;
    protected bool AlreadyAttacked;

    //States 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

  

    public abstract void AttackPlayer();

    void Start()
    {
        // Oyuncuyu etiketine göre bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent bileþenini al
        animator = GetComponent<Animator>();
       
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if(playerInAttackRange && playerInSightRange)
        {
           
            AttackPlayer();
        }
    }
    private void Patrolling() 
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("IsWalking", true);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude<1f)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint() 
    {
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
    }

   

    protected void ResetAttack()
    {
        animator.SetBool("IsAttacking", false);
       
        AlreadyAttacked = false;
    }
    public void OnDeath()
    {
        // Düþman öldüðünde hareketi durdur
        Debug.Log("OnDeath Metodu çalýþtý");

        animator.SetBool("IsDead", true);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", false);

        agent.isStopped = true;
    }

}
