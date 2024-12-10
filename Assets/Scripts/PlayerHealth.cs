using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth; // Mevcut can
    public Image playerHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Oyuncu baþlangýçta tam canla baþlar
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasarý mevcut candan düþ
        if (currentHealth < 0) currentHealth = 0; // Can 0'ýn altýna düþmesin
        UpdateHealthBar();
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        // HealthBar'ýn doluluk oranýný ayarla (0 ile 1 arasýnda)
        playerHealth.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        Debug.Log("Player is dead!");
    }
}