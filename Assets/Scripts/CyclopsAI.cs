using UnityEngine;
using UnityEngine.AI;

public class CyclopsAI : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileþeni
    private NavMeshAgent agent; // NavMeshAgent bileþeni

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
       
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        animator.SetBool("IsWalking", false);

        if (!AlreadyAttacked)
        {

            //THE ATTACKING CODE SHOULD BE HERE 
            CyclopsAttack();
            Debug.Log("Düþman Saldýrýyor");

            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        animator.SetBool("Attack4", false);
        animator.SetBool("InIdle", true);
        
        AlreadyAttacked = false;
    }
    public void OnDeath()
    {
        // Düþman öldüðünde hareketi durdur
        Debug.Log("OnDeath Metodu çalýþtý");
        animator.SetBool("IsDead", true);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        animator.SetBool("Attack4", false);
        animator.SetBool("IsWalking", false);

        agent.isStopped = true;
    }

    private void CyclopsAttack()
    {
        int Attackrandomizer = Random.Range(1, 4);
        if(Attackrandomizer== 1)
        {
            animator.SetBool("Attack1", true);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", false);
            animator.SetBool("Attack4", false);
        }
        if (Attackrandomizer == 2)
        {
            animator.SetBool("Attack2", true);
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack3", false);
            animator.SetBool("Attack4", false);
        }
        if (Attackrandomizer == 3)
        {
            animator.SetBool("Attack3", true);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack4", false);
        }
        if (Attackrandomizer == 4)
        {
            animator.SetBool("Attack4", true);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", false);
            animator.SetBool("Attack1", false);
        }

    }




}
