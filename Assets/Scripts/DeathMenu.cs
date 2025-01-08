using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuPanel; // Paneli buraya atayacaðýz.
    public float delayBeforeMenu = 2f;

    void Start()
    {
        deathMenuPanel.SetActive(false); // Menü baþlangýçta kapalý.
    }

    public void OnPlayerDeath()
    {
        StartCoroutine(OpenMenuWithDelay()); // Coroutine baþlatýlýr.
    }

    private IEnumerator OpenMenuWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeMenu); // Belirtilen süre kadar bekler.
        deathMenuPanel.SetActive(true); // Menü açýlýr.
        Time.timeScale = 0; // Oyunu durdurur.
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1; // Oyunu yeniden baþlatýr.
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükler.
    }

    public void ReloadScene()
    {
        Time.timeScale = 1; // Oyunu yeniden baþlatýr.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Geçerli sahneyi yeniden yükler.
    }

    public void QuitGame()
    {
        Application.Quit(); // Oyundan çýkar.
    }
}
