using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite sword; // �lk sprite
    public Sprite bow; // �kinci sprite

    private Image uiImage; // UI �zerindeki Image bile�eni
    private bool isSprite1Active = true; // Hangi sprite'�n aktif oldu�unu takip eder

    void Start()
    {
        // Bu GameObject �zerindeki Image bile�enini al�yoruz
        uiImage = GetComponent<Image>();

        // �lk sprite'� varsay�lan olarak ayarl�yoruz
        if (uiImage != null && sword != null)
        {
            uiImage.sprite = sword;
        }
    }

    void Update()
    {
        // Q tu�una bas�ld���nda
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Aktif sprite'� de�i�tiriyoruz
            if (uiImage != null)
            {
                if (isSprite1Active)
                {
                    uiImage.sprite = bow;
                }
                else
                {
                    uiImage.sprite = sword;
                }

                // Aktif durumu tersine �eviriyoruz
                isSprite1Active = !isSprite1Active;
            }
        }
    }
}
