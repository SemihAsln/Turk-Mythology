using UnityEngine;

public class SwordDamageDealer : MonoBehaviour
{
    public int damage = 10; // Bullet'�n verece�i hasar miktar�
    private Collider swordCollider; // SwordDamageDealer'�n collider'�
    private PlayerController playerController; // PlayerController script'ine referans

    private void Start()
    {
        // SwordDamageDealer script'inin bulundu�u objenin collider'�n� al
        swordCollider = GetComponent<Collider>();

        // Player objesini ve onun PlayerController script'ini bul
        GameObject player = GameObject.FindWithTag("Player"); // Player objesinin tag'� "Player" olmal�
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player objesi bulunamad�!");
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

        // E�er �arp�lan obje "Enemy" tag'ine sahipse
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("D��mana isabet etti.");
            // Enemy'deki Health script'ini al
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Hasar ver
                Debug.Log("D��mana hasar verdi.");
                enemyHealth.TakeDamage(damage);
             
            }
        }
    }



}