using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float rayDistance = 5f;
    public KeyCode interactKey = KeyCode.E;

    private GameObject currentInteractable;

    // Dialogue kutusunu referans al
    public GameObject dialogueBox;
    public GameObject backgroundPanel;
    public TextMeshProUGUI interactionText;
    private bool isDialogueActive = false;

    void Start()
    {
        // Dialogue kutusu ve arka plan maskesi ba�lang��ta gizli
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }

        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isDialogueActive)
        {
            // Diyalog aktifken di�er i�lemleri yapma
            return;
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Interactable"))
            {
                currentInteractable = hitObject;

                Debug.Log("Etkile�im m�mk�n: " + hitObject.name);
                // Interaction metnini g�ncelle
                interactionText.gameObject.SetActive(true);
                interactionText.text = "E - Konu�";
               

                if (Input.GetKeyDown(interactKey))
                {
                    interactionText.gameObject.SetActive(false);
                    ShowDialogue();
                }
            }

            else if (hitObject.CompareTag("Teleportation"))
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "E - Devam Et";
            }
            else
            {
                currentInteractable = null;
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            currentInteractable = null;
            interactionText.gameObject.SetActive(false);
        }
    }

    void ShowDialogue()
    {
        if (dialogueBox != null && backgroundPanel != null)
        {
            // Dialogue kutusunu ve arka plan maskesini g�ster
            dialogueBox.SetActive(true);
            backgroundPanel.SetActive(true);

            // Interaction metnini gizle
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }

            Dialogue dialogueComponent = dialogueBox.GetComponent<Dialogue>();
            if (dialogueComponent != null)
            {
                dialogueComponent.StartDialogue();
            }

            // Diyalog aktif olarak i�aretlenir
            isDialogueActive = true;
        }
    }




    public void HideDialogue()
    {
        if (dialogueBox != null && backgroundPanel != null)
        {
            // Dialogue kutusunu ve arka plan maskesini gizle
            dialogueBox.SetActive(false);
            backgroundPanel.SetActive(false);

            isDialogueActive = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Ray'i g�rselle�tirmek i�in
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
