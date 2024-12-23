using System.Collections;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab; // Instantiate edilecek k�re prefab�
    public float spawnDelay = 2f; // Bekleme s�resi
    public float launchForce = 5f; // F�rlatma kuvveti
    public int[] possibleSphereCounts = { 3, 4, 6, 8, 12 }; // Olas� k�re say�lar�

    void Start()
    {
        StartCoroutine(SpawnSpheres());
    }

    IEnumerator SpawnSpheres()
    {
        // 2 saniye bekle
        yield return new WaitForSeconds(spawnDelay);

        // Rastgele bir k�re say�s� se�
        int sphereCount = possibleSphereCounts[Random.Range(0, possibleSphereCounts.Length)];

        // K�relerin e�it aral�klarda da��t�lmas� i�in a�� hesapla
        float angleStep = 360f / sphereCount;
        float currentAngle = 0f;

        for (int i = 0; i < sphereCount; i++)
        {
            // A��dan y�n vekt�r� hesapla (Y ekseni hari�)
            float radians = currentAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));

            // K�re olu�tur
            GameObject sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);

            // K�reye Rigidbody ekle ve kuvvet uygula
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = sphere.AddComponent<Rigidbody>();
            }
            rb.AddForce(direction * launchForce, ForceMode.Impulse);

            // Bir sonraki a��ya ge�
            currentAngle += angleStep;
            Destroy(sphere, 3f);
        }
        Destroy(gameObject);
    }
}
