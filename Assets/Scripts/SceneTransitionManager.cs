using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public float interactionDistance = 2.0f; // Etkile�im mesafesi
    public KeyCode interactionKey = KeyCode.E; // Etkile�im tu�u (�rne�in \"E\")

    private Transform player; // Oyuncunun transformu
    private List<string> sceneNames; // Build Settings'deki sahne isimlerini tutar

    private void Start()
    {
        // Oyuncu objesini bul
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Build Settings'teki sahne isimlerini al
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        sceneNames = new List<string> 
        {
            "Ba�lang��", "3.kat","5.kat","7.kat","9.Kat","11.Kat","15.kat"

        
        };

       

        if (sceneNames.Count == 0)
        {
            Debug.LogError("No scenes found in Build Settings.");
        }
    }

    private void Update()
    {
        // Belirli bir obje �zerinde kontrol yap�l�yor
        GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Teleportation");

        foreach (GameObject obj in interactableObjects)
        {
            if (Vector3.Distance(player.position, obj.transform.position) <= interactionDistance)
            {
                Debug.Log("Yakla��ld�: " + obj.name);
                if (Input.GetKeyDown(interactionKey))
                {
                    LoadNextScene();
                }
            }
        }
    }

    public void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentIndex = sceneNames.IndexOf(currentSceneName);

        if (currentIndex < 0)
        {
            Debug.LogError("Current scene is not in the Build Settings scene list.");
            return;
        }

        int nextIndex = (currentIndex + 1) % sceneNames.Count; // Listeyi d�ng�sel hale getirir
        SceneManager.LoadScene(sceneNames[nextIndex]);
    }
}
