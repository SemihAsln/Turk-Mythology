using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Bullet'ýn vereceði hasar miktarý

    private void OnTriggerEnter(Collider other)
    {

        // Eðer çarpýlan obje "Enemy" tag'ine sahipse
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Düþmana isabet etti.");
            // Enemy'deki Health script'ini al
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Hasar ver
                Debug.Log("Düþmana hasar verdi.");
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}