using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastSafePosition;
    private float minimumY = 50f; // Düþme olarak kabul edilecek Y koordinatý
    private float checkInterval = 4f; // Güvenli konum kayýt sýklýðý
    private bool isGrounded;

    void Start()
    {
        lastSafePosition = transform.position;
        InvokeRepeating("SaveSafePosition", 0f, checkInterval);
    }

    void Update()
    {
        // Oyuncu belirlenen Y koordinatýnýn altýna düþtüðünde
        if (transform.position.y < minimumY)
        {
            RespawnPlayer();
        }
    }

    void SaveSafePosition()
    {
        // Oyuncu yerde ve güvenli bir konumdaysa pozisyonu kaydet
        if (IsPlayerSafe())
        {
            lastSafePosition = transform.position;
        }
    }

    bool IsPlayerSafe()
    {
        // Yerde olma kontrolü için bir raycast kullanabilirsiniz
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f);
        return isGrounded;
    }

    void RespawnPlayer()
    {
        // Oyuncuyu son güvenli konuma ýþýnla
        transform.position = lastSafePosition;

        // Gerekirse oyuncunun hýzýný sýfýrla (Rigidbody varsa)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}