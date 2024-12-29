using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // Replikleri temsil eden bir yapý
    [System.Serializable]
    public struct DialogueLine
    {
        public string lineText; // Replik metni
        public string characterName; // Konuþan karakterin adý
        public Sprite characterSprite; // Karakterin görseli
        public AudioClip voiceClip; // Repliðe ait ses dosyasý
        public TextBoxPosition textBoxPosition; // Metin kutusunun konumunu belirler
    }

    public TextMeshProUGUI textComponent; // Replik metnini göstermek için kullanýlan TMP bileþeni
    public TextMeshProUGUI nameComponent; // Karakter adýný göstermek için kullanýlan TMP bileþeni
    public Image characterImage; // Karakter görselini göstermek için kullanýlan Image bileþeni
    public AudioSource audioSource; // Ses dosyalarýný çalmak için kullanýlan AudioSource

    public RectTransform textBoxRectTransform; // Metin kutusunun RectTransform bileþeni

    public DialogueLine[] lines; // Repliklerin tutulduðu dizi
    public float textSpeed = 0.05f; // Harflerin yazýlma hýzý
    public float lineDelay = 2f; // Bir replik tamamlandýktan sonra bekleme süresi

    private int index = 0; // Þu anki replik indeksi
    private bool isTyping = false; // Yazma iþleminin devam edip etmediðini takip eder
    private bool isAutoPlaying = true; // Otomatik oynatma kontrolü

    public GameObject dialogueBox; // Diyalog kutusu nesnesi

    public enum TextBoxPosition
    {
        BottomFarLeft,
        BottomLeft,
        BottomSlightLeft,
        BottomCenterLeft,
        BottomCenterRight,
        BottomSlightRight,
        BottomRight,
        BottomFarRight
    }

    void Start()
    {
        // Eðer diyalog kutusu devre dýþýysa etkinleþtir
        if (dialogueBox != null && !dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);
        }

        // Diyalog baþlatýlýr
        textComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        // Sol týk ile bir sonraki repliðe geçiþ
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            isAutoPlaying = false; // Manuel kontrol aktif
            NextLine();
        }
    }

    void StartDialogue()
    {
        index = 0; // Ýlk replikten baþla
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true; // Yazma iþlemi baþladý

        // Replik metni, karakter adý, görseli ve metin kutusu konumu ayarlanýr
        textComponent.text = string.Empty;
        nameComponent.text = lines[index].characterName;
        characterImage.sprite = lines[index].characterSprite;
        SetTextBoxPosition(lines[index].textBoxPosition);

        // Ses dosyasý çalýnýr
        if (lines[index].voiceClip != null)
        {
            audioSource.clip = lines[index].voiceClip;
            audioSource.Play();
        }

        // Replik metni harf harf yazýlýr
        foreach (char c in lines[index].lineText.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // Yazma iþlemi bitti

        // Otomatik oynatma aktifse bir süre sonra bir sonraki repliðe geç
        if (isAutoPlaying)
        {
            yield return new WaitForSeconds(lineDelay);
            NextLine();
        }
    }

    void NextLine()
    {
        // Eðer dizide daha fazla replik varsa bir sonrakine geç
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            // Diyalog bittiðinde canvas kapatýlýr
            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;
            characterImage.sprite = null;
            Debug.Log("Diyalog sona erdi.");

            // Diyalog kutusunu devre dýþý býrak
            if (dialogueBox != null)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void SetTextBoxPosition(TextBoxPosition position)
    {
       
        switch (position)
        {
            case TextBoxPosition.BottomFarLeft:
                textBoxRectTransform.anchorMin = new Vector2(0f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(0.2f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.2f, 0.5f);
                break;
            case TextBoxPosition.BottomLeft:
                textBoxRectTransform.anchorMin = new Vector2(0.2f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(0.4f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.35f, 0.5f);
                break;
            case TextBoxPosition.BottomSlightLeft:
                textBoxRectTransform.anchorMin = new Vector2(0.4f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(0.5f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.45f, 0.5f);
                break;
            case TextBoxPosition.BottomCenterLeft:
                textBoxRectTransform.anchorMin = new Vector2(0.5f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(0.6f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.55f, 0.5f);
                break;
            case TextBoxPosition.BottomCenterRight:
                textBoxRectTransform.anchorMin = new Vector2(0.6f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(0.7f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.6f, 0.5f);
                break;
            case TextBoxPosition.BottomSlightRight:
                textBoxRectTransform.anchorMin = new Vector2(0.7f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(0.8f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.65f, 0.5f);
                break;
            case TextBoxPosition.BottomRight:
                textBoxRectTransform.anchorMin = new Vector2(0.8f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(1f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.7f, 0.5f);
                break;
            case TextBoxPosition.BottomFarRight:
                textBoxRectTransform.anchorMin = new Vector2(0.9f, 0f);
                textBoxRectTransform.anchorMax = new Vector2(1f, 0.2f);
                textBoxRectTransform.pivot = new Vector2(0.8f, 0.5f);
                break;
        }
    }
}
