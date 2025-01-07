using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Baþlangýç"); // Oyun sahnesinin adýný gir
    }

    public void QuitGame()
    {
        Application.Quit(); // Oyunu kapat
    }
}
