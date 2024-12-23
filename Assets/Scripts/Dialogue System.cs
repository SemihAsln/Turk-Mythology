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
        public Sprite characterSprite; // Karakterin g�rseli
        public AudioClip voiceClip; // Repli�e ait ses dosyas�
    }

    public TextMeshProUGUI textComponent; // Replik metnini g�stermek i�in kullan�lan TMP bile�eni
    public Image characterImage; // Karakter g�rselini g�stermek i�in kullan�lan Image bile�eni
    public AudioSource audioSource; // Ses dosyalar�n� �almak i�in kullan�lan AudioSource

    public DialogueLine[] lines; // Repliklerin tutuldu�u dizi
    public float textSpeed = 0.05f; // Harflerin yaz�lma h�z�
    public float lineDelay = 2f; // Bir replik tamamland�ktan sonra bekleme s�resi

    public GameObject dialogueBox; // Diyalog kutusu

    private int index = 0; // �u anki replik indeksi
    private bool isTyping = false; // Yazma i�leminin devam edip etmedi�ini takip eder
    private bool isAutoPlaying = true; // Otomatik oynatma kontrol�

    void Start()
    {
        // E�er diyalog kutusu devre d���ysa etkinle�tir
        if (dialogueBox != null && !dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);
        }

        // Diyalog ba�lat�l�r
        textComponent.text = string.Empty;
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

        // Replik metni ve g�rseli ayarlan�r
        textComponent.text = string.Empty;
        characterImage.sprite = lines[index].characterSprite;

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
            characterImage.sprite = null;
            Debug.Log("Diyalog sona erdi.");
        }
    }
}
