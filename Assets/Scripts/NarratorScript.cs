using UnityEngine;
using TMPro; // TextMeshPro namespace
using System.Collections;

public class VoiceOverSystem : MonoBehaviour
{
    public GameObject panel; // UI Panel
    public TMP_Text subtitleText; // TextMeshPro Text bileþeni
    public AudioSource audioSource; // Ses dosyasý oynatýcý
    public string[] subtitles; // Altyazý metinleri
    public float[] subtitleTimings; // Her altyazýnýn baþlangýç zamaný

    private bool isTriggered = false;

    private void Start()
    {
        panel.SetActive(false);   
    }
    public void TriggerVoiceOver()
    {
        if (!isTriggered) // Sadece bir kez tetiklensin
        {
            isTriggered = true;
            panel.SetActive(true); // Paneli aç
            StartCoroutine(PlayVoiceOver());
        }
    }

    private IEnumerator PlayVoiceOver()
    {
        if (subtitles.Length == 0 || subtitleTimings.Length == 0)
        {
            Debug.LogWarning("Subtitle or timing array is empty.");
            panel.SetActive(false);
            subtitleText.gameObject.SetActive(false);
            yield break;
        }

        audioSource.Play(); // Ses dosyasýný çal

        for (int i = 0; i < subtitles.Length; i++)
        {
            if (i >= subtitleTimings.Length)
            {
                Debug.LogWarning("Timing array is shorter than subtitle array.");
                break;
            }

            subtitleText.text = subtitles[i]; // Altyazýyý deðiþtir
            yield return new WaitForSeconds(subtitleTimings[i]); // Altyazý süresini bekle
        }

        subtitleText.text = ""; // Altyazýyý temizle
        panel.SetActive(false); // Paneli kapat
        subtitleText.gameObject.SetActive(false); // Altyazýyý devre dýþý býrak
    }
}
