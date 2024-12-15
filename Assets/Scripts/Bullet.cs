using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Bullet'�n verece�i hasar miktar�

    private void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {

        // E�er �arp�lan obje "Enemy" tag'ine sahipse
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("D��mana isabet etti.");
            // Enemy'deki Health script'ini al
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            CyclopsHealth cyclopsHealth = other.GetComponent<CyclopsHealth>();
            if (enemyHealth != null)
            {
                // Hasar ver
                Debug.Log("D��mana hasar verdi.");
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);

            }
            if(cyclopsHealth!=null)
            {
                cyclopsHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }


    
}