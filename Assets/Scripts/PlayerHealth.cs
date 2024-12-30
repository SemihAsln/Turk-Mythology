using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth; // Mevcut can
    public Image playerHealth;
    Animator animator;

    private Renderer[] renderers;
    private List<Material> allMaterials = new List<Material>();
    private Color[] originalColors;

    public DeathMenu deathMenu;


    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Oyuncu baþlangýçta tam canla baþlar
        UpdateHealthBar();

        // Tüm Renderer bileþenlerini bul
        renderers = GetComponentsInChildren<Renderer>();

        // Tüm malzemeleri tek bir listede topla
        foreach (Renderer renderer in renderers)
        {
            allMaterials.AddRange(renderer.materials);
        }

        // Orijinal renkleri sakla
        originalColors = new Color[allMaterials.Count];
        for (int i = 0; i < allMaterials.Count; i++)
        {
            originalColors[i] = allMaterials[i].color;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasarý mevcut candan düþ
        if (currentHealth < 0) currentHealth = 0; // Can 0'ýn altýna düþmesin
        UpdateHealthBar();
        Debug.Log("Player Health: " + currentHealth);

        // Yanýp sönme efekti
        StartCoroutine(FlashEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashEffect()
    {
        // Yanýp sönme için renkler
        Color flashColor1 = Color.black;
        Color flashColor2 = Color.white;

        int flashCount = 5; // Yanýp sönme sayýsý
        float flashDuration = 0.1f; // Her yanýp sönme süresi

        for (int i = 0; i < flashCount; i++)
        {
            // Tüm malzemeleri siyah yap
            foreach (Material mat in allMaterials)
            {
                mat.color = flashColor1;
            }

            yield return new WaitForSeconds(flashDuration);

            // Tüm malzemeleri beyaz yap
            foreach (Material mat in allMaterials)
            {
                mat.color = flashColor2;
            }

            yield return new WaitForSeconds(flashDuration);
        }

        // Efekt bittikten sonra orijinal renkleri geri yükle
        ResetMaterialColors();
    }

    private void ResetMaterialColors()
    {
        for (int i = 0; i < allMaterials.Count; i++)
        {
            allMaterials[i].color = originalColors[i];
        }
    }

    private void UpdateHealthBar()
    {
        // HealthBar'ýn doluluk oranýný ayarla (0 ile 1 arasýnda)
        playerHealth.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsDashing", false);
        animator.SetBool("IsAttackingBow", false);
        animator.SetBool("IsAttackingSword", false);
        animator.SetBool("Combo1", false);
        animator.SetBool("Combo2", false);
        animator.SetBool("Combo3", false);
        Debug.Log("Player is dead!");
        deathMenu.OnPlayerDeath();
    }
}
