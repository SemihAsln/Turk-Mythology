using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth; // Mevcut can
    public Image playerHealth;
    public float delayBeforeMainMenu = 3f; // Ana menüye geçmeden önceki bekleme süresi (saniye)

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
        // Oyuncu öldüðünde yapýlacak iþlemler
        StartCoroutine(WaitAndLoadMainMenu());

    }

    private IEnumerator WaitAndLoadMainMenu()
    {
        yield return new WaitForSeconds(delayBeforeMainMenu); // Belirtilen süre kadar bekler
        SceneManager.LoadScene(0); // Ana menü sahnesini yükler
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}