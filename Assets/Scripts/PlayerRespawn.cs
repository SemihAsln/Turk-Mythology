using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastSafePosition;
    private float minimumY = 50f; // D��me olarak kabul edilecek Y koordinat�
    private float checkInterval = 4f; // G�venli konum kay�t s�kl���
    private bool isGrounded;

    void Start()
    {
        lastSafePosition = transform.position;
        InvokeRepeating("SaveSafePosition", 0f, checkInterval);
    }

    void Update()
    {
        // Oyuncu belirlenen Y koordinat�n�n alt�na d��t���nde
        if (transform.position.y < minimumY)
        {
            RespawnPlayer();
        }
    }

    void SaveSafePosition()
    {
        // Oyuncu yerde ve g�venli bir konumdaysa pozisyonu kaydet
        if (IsPlayerSafe())
        {
            lastSafePosition = transform.position;
        }
    }

    bool IsPlayerSafe()
    {
        // Yerde olma kontrol� i�in bir raycast kullanabilirsiniz
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f);
        return isGrounded;
    }

    void RespawnPlayer()
    {
        // Oyuncuyu son g�venli konuma ���nla
        transform.position = lastSafePosition;

        // Gerekirse oyuncunun h�z�n� s�f�rla (Rigidbody varsa)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}