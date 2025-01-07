using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; // Singleton pattern

    private List<GameObject> enemies = new List<GameObject>();
    public GameObject interactableObject; // Diyalog objesi

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Leveldeki t�m Enemy tagine sahip objeleri bul ve listeye ekle
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(enemyObjects);

        // Interactable objeyi ba�lang��ta kapat
        interactableObject.SetActive(false);
    }

    // Enemy �ld���nde �a��r�lacak fonksiyon
    public void OnEnemyKilled(GameObject enemy)
    {
        enemies.Remove(enemy);

        // E�er t�m enemy'ler �ld�yse, interactable objeyi aktif et
        if (enemies.Count == 0)
        {
            interactableObject.SetActive(true);
        }
    }
}
