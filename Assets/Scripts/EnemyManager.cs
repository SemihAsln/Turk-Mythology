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
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemyObjects)
        {
            Debug.Log("Bulunan düþman: " + enemy.name);
        }
        enemies.AddRange(enemyObjects);
        interactableObject.SetActive(false);
    }


    // Enemy öldüðünde çaðýrýlacak fonksiyon
    public void OnEnemyKilled(GameObject enemy)
    {
        enemies.Remove(enemy);

        // Eðer tüm enemy'ler öldüyse, interactable objeyi aktif et
        if (enemies.Count == 0)
        {
            interactableObject.SetActive(true);
        }
    }
}
