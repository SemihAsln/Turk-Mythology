using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Diyalog metni
    public string[] lines;               // Diyalog satýrlarý
    public float textSpeed;              // Yazý hýzý
    public GameObject backgroundPanel;   // Arka plan maskesi

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false); // Arka planý baþlangýçta gizle
        }
    }

    public void StartDialogue()
    {
        index = 0;
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(true); // Arka planý görünür yap
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
                backgroundPanel.SetActive(false); // Arka planý gizle
            }
            gameObject.SetActive(false); // DialogueBox'ý kapat
        }
    }
}