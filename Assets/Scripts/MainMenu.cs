using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Ba�lang��"); // Oyun sahnesinin ad�n� gir
    }

    public void QuitGame()
    {
        Application.Quit(); // Oyunu kapat
    }
}
