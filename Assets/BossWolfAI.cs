using UnityEngine;
using UnityEngine.AI;

public class BossWolfAI : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileþeni
    private NavMeshAgent agent; // NavMeshAgent bileþeni

    public LayerMask whatIsGround, whatIsPlayer;

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

    public GameObject dirtPrefab; // Dirt prefabini buraya atayýn
    public Transform dirtspawnpoint; // Dirt spawn noktasýný belirlemek için bir Transform
    public float bulletSpeed = 10f; // Dirt'in hýzýný kontrol eden bir deðiþken


    void Start()
    {
        // Oyuncuyu etiketine göre bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponentInParent<NavMeshAgent>(); // NavMeshAgent bileþenini al

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }
    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("IsWalking", true);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {

        agent.SetDestination(player.position);
        animator.SetBool("IsWalking", true);

    }

    // ThrowDirt metodu
public void ThrowDirt()
{
    if (dirtPrefab != null && dirtspawnpoint != null)
    {
        var dirtObject = Instantiate(dirtPrefab, dirtspawnpoint.position, dirtspawnpoint.rotation);
        dirtObject.GetComponent<Rigidbody>().velocity = dirtspawnpoint.forward * bulletSpeed; // Hareket hýzý
        Destroy(dirtObject, 3f); // Dirt nesnesini 3 saniye sonra yok et
    }
    else
    {
        Debug.LogWarning("DirtPrefab veya DirtSpawnPoint atanmadý!");
    }
}

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        //transform.LookAt(player);

        animator.SetBool("IsWalking", false);

        if (!AlreadyAttacked)
        {

            //THE ATTACKING CODE SHOULD BE HERE 
            BossWolfAttack();
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
        animator.SetBool("IsWalking", false);

        agent.isStopped = true;
    }

    private void BossWolfAttack()
    {
        int Attackrandomizer = Random.Range(1, 4); // Random.Range'in üst sýnýrýný 4 yaptým çünkü 1-3 arasýnda seçim yapmasý gerekiyor
        if (Attackrandomizer == 1)
        {
            animator.SetBool("Attack1", true);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", false);

            Debug.Log("Attack1 animasyonu tetiklendi.");
        }
        if (Attackrandomizer == 2)
        {
            animator.SetBool("Attack2", true);
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack3", false);

            Debug.Log("Attack2 animasyonu tetiklendi.");
        }
        if (Attackrandomizer == 3)
        {
            animator.SetBool("Attack3", true);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack1", false);

            Debug.Log("Attack3 animasyonu tetiklendi. Dirt atýlýyor...");
        }
    }

}
