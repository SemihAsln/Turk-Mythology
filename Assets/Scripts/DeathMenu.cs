using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuPanel; // Paneli buraya atayaca��z.
    public float delayBeforeMenu = 2f;

    void Start()
    {
        deathMenuPanel.SetActive(false); // Men� ba�lang��ta kapal�.
    }

    public void OnPlayerDeath()
    {
        StartCoroutine(OpenMenuWithDelay()); // Coroutine ba�lat�l�r.
    }

    private IEnumerator OpenMenuWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeMenu); // Belirtilen s�re kadar bekler.
        deathMenuPanel.SetActive(true); // Men� a��l�r.
        Time.timeScale = 0; // Oyunu durdurur.
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1; // Oyunu yeniden ba�lat�r.
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesini y�kler.
    }

    public void ReloadScene()
    {
        Time.timeScale = 1; // Oyunu yeniden ba�lat�r.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Ge�erli sahneyi yeniden y�kler.
    }

    public void QuitGame()
    {
        Application.Quit(); // Oyundan ��kar.
    }
}
