using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Diyalog metni
    public string[] lines;               // Diyalog sat�rlar�
    public float textSpeed;              // Yaz� h�z�
    public GameObject backgroundPanel;   // Arka plan maskesi

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false); // Arka plan� ba�lang��ta gizle
        }
    }

    public void StartDialogue()
    {
        index = 0;
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(true); // Arka plan� g�r�n�r yap
        }
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (backgroundPanel != null)
            {
                backgroundPanel.SetActive(false); // Arka plan� gizle
            }
            gameObject.SetActive(false); // DialogueBox'� kapat
        }
    }
}