using System.Collections;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab; // Instantiate edilecek küre prefabý
    public float spawnDelay = 2f; // Bekleme süresi
    public float launchForce = 5f; // Fýrlatma kuvveti
    public int[] possibleSphereCounts = { 3, 4, 6, 8, 12 }; // Olasý küre sayýlarý

    void Start()
    {
        StartCoroutine(SpawnSpheres());
    }

    IEnumerator SpawnSpheres()
    {
        // 2 saniye bekle
        yield return new WaitForSeconds(spawnDelay);

        // Rastgele bir küre sayýsý seç
        int sphereCount = possibleSphereCounts[Random.Range(0, possibleSphereCounts.Length)];

        // Kürelerin eþit aralýklarda daðýtýlmasý için açý hesapla
        float angleStep = 360f / sphereCount;
        float currentAngle = 0f;

        for (int i = 0; i < sphereCount; i++)
        {
            // Açýdan yön vektörü hesapla (Y ekseni hariç)
            float radians = currentAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));

            // Küre oluþtur
            GameObject sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);

            // Küreye Rigidbody ekle ve kuvvet uygula
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = sphere.AddComponent<Rigidbody>();
            }
            rb.AddForce(direction * launchForce, ForceMode.Impulse);

            // Bir sonraki açýya geç
            currentAngle += angleStep;
            Destroy(sphere, 3f);
        }
        Destroy(gameObject);
    }
}
