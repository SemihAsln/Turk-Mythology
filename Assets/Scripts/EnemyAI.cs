using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileþeni
    private NavMeshAgent agent; // NavMeshAgent bileþeni

    void Start()
    {
        // Oyuncuyu etiketine göre bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent bileþenini al
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Oyuncunun pozisyonuna git
        }
    }

    public void OnDeath()
    {
        // Düþman öldüðünde hareketi durdur
        Debug.Log("OnDeath Metodu çalýþtý");
        agent.isStopped = true;
    }
}
