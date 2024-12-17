using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10; // Verilecek hasar miktarý

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncunun tag'ini kontrol et
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // PlayerHealth scriptini al
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Hasar uygula
            }
            if(this.CompareTag("EnemyProjectile")==true)
            {
                Destroy(gameObject);
            }
        }
    }
}
