using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Spawn edilecek düþman prefab'ý
    public int maxEnemies = 20;     // Ayný anda bulunacak maksimum düþman sayýsý
    public float spawnInterval = 4f; // 4 saniyede bir spawn
    public int enemiesToDefeat = 20; // Öldürüldüðünde bool dönecek düþman sayýsý

    private int currentEnemyCount = 0; // Mevcut düþman sayýsý
    private int defeatedEnemyCount = 0; // Öldürülen düþman sayýsý
    private bool allEnemiesDefeated = false; // Hedeflenen düþman öldürüldüðünde true olacak

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies && !allEnemiesDefeated)
            {
                Vector3 spawnPosition = GetRandomNavMeshPosition();
                if (spawnPosition != Vector3.zero)
                {
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    currentEnemyCount++;
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * 5f; // 5 birimlik bir alan
        randomPosition.y = 70; // Y eksenini sabit 60 olarak ayarla

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero; // Geçerli bir NavMesh pozisyonu bulunamazsa
    }


    public void OnEnemyDestroyed()
    {
        currentEnemyCount--; // Mevcut düþman sayýsýný azalt
        defeatedEnemyCount++; // Öldürülen düþman sayýsýný artýr

        if (defeatedEnemyCount >= enemiesToDefeat && !allEnemiesDefeated)
        {
            allEnemiesDefeated = true; // Hedeflenen düþman sayýsýna ulaþýldýðýnda true yap
            Debug.Log("All required enemies have been defeated!");
        }
    }

    public bool AreAllEnemiesDefeated()
    {
        return allEnemiesDefeated; // Hedeflenen düþman öldürüldü mü?
    }

    public void SetEnemiesToDefeat(int count)
    {
        if (count > 0)
        {
            enemiesToDefeat = count; // Öldürme hedefini güncelle
            allEnemiesDefeated = false; // Yeni hedefe göre sýfýrla
            defeatedEnemyCount = 0; // Öldürülen düþman sayýsýný sýfýrla
            Debug.Log("New enemy defeat target set to: " + count);
        }
        else
        {
            Debug.LogWarning("Enemy defeat target must be greater than 0.");
        }
    }
}
