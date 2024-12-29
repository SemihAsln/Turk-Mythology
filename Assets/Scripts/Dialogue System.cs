using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // Replikleri temsil eden bir yap�
    [System.Serializable]
    public struct DialogueLine
    {
        public string lineText; // Replik metni
        public string characterName; // Konu�an karakterin ad�
        public Sprite characterSprite; // Karakterin g�rseli
        public AudioClip voiceClip; // Repli�e ait ses dosyas�
        public TextBoxPosition textBoxPosition; // Metin kutusunun konumunu belirler
    }

    public TextMeshProUGUI textComponent; // Replik metnini g�stermek i�in kullan�lan TMP bile�eni
    public TextMeshProUGUI nameComponent; // Karakter ad�n� g�stermek i�in kullan�lan TMP bile�eni
    public Image characterImage; // Karakter g�rselini g�stermek i�in kullan�lan Image bile�eni
    public AudioSource audioSource; // Ses dosyalar�n� �almak i�in kullan�lan AudioSource

    public RectTransform textBoxRectTransform; // Metin kutusunun RectTransform bile�eni

    public DialogueLine[] lines; // Repliklerin tutuldu�u dizi
    public float textSpeed = 0.05f; // Harflerin yaz�lma h�z�
    public float lineDelay = 2f; // Bir replik tamamland�ktan sonra bekleme s�resi

    private int index = 0; // �u anki replik indeksi
    private bool isTyping = false; // Yazma i�leminin devam edip etmedi�ini takip eder
    private bool isAutoPlaying = true; // Otomatik oynatma kontrol�

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
        // E�er diyalog kutusu devre d���ysa etkinle�tir
        if (dialogueBox != null && !dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);
        }

        // Diyalog ba�lat�l�r
        textComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        // Sol t�k ile bir sonraki repli�e ge�i�
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            isAutoPlaying = false; // Manuel kontrol aktif
            NextLine();
        }
    }

    void StartDialogue()
    {
        index = 0; // �lk replikten ba�la
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true; // Yazma i�lemi ba�lad�

        // Replik metni, karakter ad�, g�rseli ve metin kutusu konumu ayarlan�r
        textComponent.text = string.Empty;
        nameComponent.text = lines[index].characterName;
        characterImage.sprite = lines[index].characterSprite;
        SetTextBoxPosition(lines[index].textBoxPosition);

        // Ses dosyas� �al�n�r
        if (lines[index].voiceClip != null)
        {
            audioSource.clip = lines[index].voiceClip;
            audioSource.Play();
        }

        // Replik metni harf harf yaz�l�r
        foreach (char c in lines[index].lineText.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // Yazma i�lemi bitti

        // Otomatik oynatma aktifse bir s�re sonra bir sonraki repli�e ge�
        if (isAutoPlaying)
        {
            yield return new WaitForSeconds(lineDelay);
            NextLine();
        }
    }

    void NextLine()
    {
        // E�er dizide daha fazla replik varsa bir sonrakine ge�
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            // Diyalog bitti�inde canvas kapat�l�r
            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;
            characterImage.sprite = null;
            Debug.Log("Diyalog sona erdi.");

            // Diyalog kutusunu devre d��� b�rak
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
