using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Spawn edilecek d��man prefab'�
    public int maxEnemies = 20;     // Ayn� anda bulunacak maksimum d��man say�s�
    public float spawnInterval = 4f; // 4 saniyede bir spawn
    public int enemiesToDefeat = 20; // �ld�r�ld���nde bool d�necek d��man say�s�

    private int currentEnemyCount = 0; // Mevcut d��man say�s�
    private int defeatedEnemyCount = 0; // �ld�r�len d��man say�s�
    private bool allEnemiesDefeated = false; // Hedeflenen d��man �ld�r�ld���nde true olacak

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
        return Vector3.zero; // Ge�erli bir NavMesh pozisyonu bulunamazsa
    }


    public void OnEnemyDestroyed()
    {
        currentEnemyCount--; // Mevcut d��man say�s�n� azalt
        defeatedEnemyCount++; // �ld�r�len d��man say�s�n� art�r

        if (defeatedEnemyCount >= enemiesToDefeat && !allEnemiesDefeated)
        {
            allEnemiesDefeated = true; // Hedeflenen d��man say�s�na ula��ld���nda true yap
            Debug.Log("All required enemies have been defeated!");
        }
    }

    public bool AreAllEnemiesDefeated()
    {
        return allEnemiesDefeated; // Hedeflenen d��man �ld�r�ld� m�?
    }

    public void SetEnemiesToDefeat(int count)
    {
        if (count > 0)
        {
            enemiesToDefeat = count; // �ld�rme hedefini g�ncelle
            allEnemiesDefeated = false; // Yeni hedefe g�re s�f�rla
            defeatedEnemyCount = 0; // �ld�r�len d��man say�s�n� s�f�rla
            Debug.Log("New enemy defeat target set to: " + count);
        }
        else
        {
            Debug.LogWarning("Enemy defeat target must be greater than 0.");
        }
    }
}
