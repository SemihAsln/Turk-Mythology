using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float rayDistance = 5f;
    public KeyCode interactKey = KeyCode.E;

    private GameObject currentInteractable;

    // Dialogue kutusunu referans al
    public GameObject dialogueBox;
    public GameObject backgroundPanel;

    void Start()
    {
        // Dialogue kutusu ve arka plan maskesi baþlangýçta gizli
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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Interactable"))
            {
                currentInteractable = hitObject;

                Debug.Log("Etkileþim mümkün: " + hitObject.name);

                if (Input.GetKeyDown(interactKey))
                {
                    ShowDialogue();
                }
            }
            else
            {
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractable = null;
        }
    }

    void ShowDialogue()
    {
        if (dialogueBox != null && backgroundPanel != null)
        {
            // Dialogue kutusunu ve arka plan maskesini göster
            dialogueBox.SetActive(true);
            backgroundPanel.SetActive(true);

            Dialogue dialogueComponent = dialogueBox.GetComponent<Dialogue>();
            if (dialogueComponent != null)
            {
                dialogueComponent.StartDialogue();
            }
        }
    }

    public void HideDialogue()
    {
        if (dialogueBox != null && backgroundPanel != null)
        {
            // Dialogue kutusunu ve arka plan maskesini gizle
            dialogueBox.SetActive(false);
            backgroundPanel.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        // Ray'i görselleþtirmek için
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
