using UnityEngine;
using TMPro; // TextMeshPro namespace
using System.Collections;

public class VoiceOverSystem : MonoBehaviour
{
    public GameObject panel; // UI Panel
    public TMP_Text subtitleText; // TextMeshPro Text bile�eni
    public AudioSource audioSource; // Ses dosyas� oynat�c�
    public string[] subtitles; // Altyaz� metinleri
    public float[] subtitleTimings; // Her altyaz�n�n ba�lang�� zaman�

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
            panel.SetActive(true); // Paneli a�
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

        audioSource.Play(); // Ses dosyas�n� �al

        for (int i = 0; i < subtitles.Length; i++)
        {
            if (i >= subtitleTimings.Length)
            {
                Debug.LogWarning("Timing array is shorter than subtitle array.");
                break;
            }

            subtitleText.text = subtitles[i]; // Altyaz�y� de�i�tir
            yield return new WaitForSeconds(subtitleTimings[i]); // Altyaz� s�resini bekle
        }

        subtitleText.text = ""; // Altyaz�y� temizle
        panel.SetActive(false); // Paneli kapat
        subtitleText.gameObject.SetActive(false); // Altyaz�y� devre d��� b�rak
    }
}
