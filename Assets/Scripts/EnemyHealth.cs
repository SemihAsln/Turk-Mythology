using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Can miktarý
    public int currentHealth;
    public Image healthBar;
    [SerializeField] Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        animator.SetBool("GetHit", true);
        Debug.Log("Düþman " + damage + " hasar yedi.");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // Saðlýk yüzdesine göre doluluk
        }
    }

    private void Die()
    {

        EnemyAI enemyAI = GetComponentInParent<EnemyAI>();


        if (enemyAI == null)
        {
            Debug.Log("EnemyAI'a ulaþýlamýyor" + gameObject.name);
        }

        if (enemyAI != null)
        {
            enemyAI.OnDeath(); // EnemyAI'ye ölümü bildir
        }

        if (transform.parent != null)
        {
            Debug.Log("Parent objesi yok ediliyor: " + transform.parent.name);
            Destroy(transform.parent.gameObject, 3f); // Parent objeyi yok et
        }
        else
        {
            Debug.Log("Objesi yok ediliyor: " + gameObject.name);
            Destroy(gameObject, 3f); // Parent yoksa kendi objesini yok et
        }
    }
}
