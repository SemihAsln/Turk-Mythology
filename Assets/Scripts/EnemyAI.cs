using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bile�eni
    private NavMeshAgent agent; // NavMeshAgent bile�eni

    void Start()
    {
        // Oyuncuyu etiketine g�re bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent bile�enini al
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
        // D��man �ld���nde hareketi durdur
        Debug.Log("OnDeath Metodu �al��t�");
        agent.isStopped = true;
    }
}
