using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite sword; // Ýlk sprite
    public Sprite bow; // Ýkinci sprite

    private Image uiImage; // UI üzerindeki Image bileþeni
    private bool isSprite1Active = true; // Hangi sprite'ýn aktif olduðunu takip eder

    void Start()
    {
        // Bu GameObject üzerindeki Image bileþenini alýyoruz
        uiImage = GetComponent<Image>();

        // Ýlk sprite'ý varsayýlan olarak ayarlýyoruz
        if (uiImage != null && sword != null)
        {
            uiImage.sprite = sword;
        }
    }

    void Update()
    {
        // Q tuþuna basýldýðýnda
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Aktif sprite'ý deðiþtiriyoruz
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

                // Aktif durumu tersine çeviriyoruz
                isSprite1Active = !isSprite1Active;
            }
        }
    }
}
