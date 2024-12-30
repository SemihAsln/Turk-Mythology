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
        currentHealth = maxHealth; // Oyuncu ba�lang��ta tam canla ba�lar
        UpdateHealthBar();

        // T�m Renderer bile�enlerini bul
        renderers = GetComponentsInChildren<Renderer>();

        // T�m malzemeleri tek bir listede topla
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
        currentHealth -= damage; // Hasar� mevcut candan d��
        if (currentHealth < 0) currentHealth = 0; // Can 0'�n alt�na d��mesin
        UpdateHealthBar();
        Debug.Log("Player Health: " + currentHealth);

        // Yan�p s�nme efekti
        StartCoroutine(FlashEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashEffect()
    {
        // Yan�p s�nme i�in renkler
        Color flashColor1 = Color.black;
        Color flashColor2 = Color.white;

        int flashCount = 5; // Yan�p s�nme say�s�
        float flashDuration = 0.1f; // Her yan�p s�nme s�resi

        for (int i = 0; i < flashCount; i++)
        {
            // T�m malzemeleri siyah yap
            foreach (Material mat in allMaterials)
            {
                mat.color = flashColor1;
            }

            yield return new WaitForSeconds(flashDuration);

            // T�m malzemeleri beyaz yap
            foreach (Material mat in allMaterials)
            {
                mat.color = flashColor2;
            }

            yield return new WaitForSeconds(flashDuration);
        }

        // Efekt bittikten sonra orijinal renkleri geri y�kle
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
        // HealthBar'�n doluluk oran�n� ayarla (0 ile 1 aras�nda)
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
