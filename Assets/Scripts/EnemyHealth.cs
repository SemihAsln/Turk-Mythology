using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Can miktar�
    public int currentHealth;
    public Image healthBar;
    public bool isInCoolDown;
    [SerializeField] Animator animator;
    public AudioClip hitSound; // Vurma sesi
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isInCoolDown = false;
        currentHealth = maxHealth;
        UpdateHealthBar();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        animator.SetBool("GetHit", true);
     
        Debug.Log("D��man " + damage + " hasar yedi.");


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
            healthBar.fillAmount = (float)currentHealth / maxHealth; // Sa�l�k y�zdesine g�re doluluk
        }
    }

 
    private void Die()
    {

        EnemyAI enemyAI = GetComponentInParent<EnemyAI>();


        if (enemyAI == null)
        {
            Debug.Log("EnemyAI'a ula��lam�yor" + gameObject.name);
        }

        if (enemyAI != null)
        {
            enemyAI.OnDeath(); // EnemyAI'ye �l�m� bildir
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


    public void GetHitSound()
    {
        // E�er ses atanm��sa ve AudioSource varsa sesi �al
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogWarning("Vurma sesi atanmad� veya AudioSource bulunamad�!");
        }
    }
}
