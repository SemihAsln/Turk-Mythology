using UnityEngine;

public class SwordDamageDealer : MonoBehaviour
{
    public int damage = 10; // Bullet'ýn vereceði hasar miktarý
    private Collider swordCollider; // SwordDamageDealer'ýn collider'ý
    private PlayerController playerController; // PlayerController script'ine referans

    private void Start()
    {
        // SwordDamageDealer script'inin bulunduðu objenin collider'ýný al
        swordCollider = GetComponent<Collider>();

        // Player objesini ve onun PlayerController script'ini bul
        GameObject player = GameObject.FindWithTag("Player"); // Player objesinin tag'ý "Player" olmalý
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player objesi bulunamadý!");
        }
    }

    private void Update()
    {
            if(playerController !=null && playerController._isAttacking ==false )
        {

            swordCollider.enabled = false;
        }
        else 
        {
            swordCollider.enabled = true;
        }
    }
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