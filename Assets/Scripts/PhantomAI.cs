using UnityEngine;
using UnityEngine.AI;

public class PhantomAI : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bile�eni
    private NavMeshAgent agent; // NavMeshAgent bile�eni

    public LayerMask whatIsGround,whatIsPlayer;

    [SerializeField] Animator animator;
  

    //Patrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking 
    public float timeBetweenAttacks;
    bool AlreadyAttacked;

    //States 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        // Oyuncuyu etiketine g�re bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponentInParent<NavMeshAgent>(); // NavMeshAgent bile�enini al
       
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
       
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        //transform.LookAt(player);

        animator.SetBool("IsWalking", false);

        if (!AlreadyAttacked)
        {

            //THE ATTACKING CODE SHOULD BE HERE 
            PhantomAttack();
            Debug.Log("D��man Sald�r�yor");

            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("IsAttacking", false);
       
        animator.SetBool("InIdle", true);
        
        AlreadyAttacked = false;
    }
    public void OnDeath()
    {
        // D��man �ld���nde hareketi durdur
        Debug.Log("OnDeath Metodu �al��t�");
        animator.SetBool("IsDead", true);
        animator.SetBool("IsAttacking", false);
 
        animator.SetBool("IsWalking", false);

        agent.isStopped = true;
    }

    private void PhantomAttack()
    {
            animator.SetBool("IsAttacking", true); 
    }
}
