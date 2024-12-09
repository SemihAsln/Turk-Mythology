using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Raycast'in ula�abilece�i mesafe
    public float rayDistance = 5f;

    // Etkile�im tu�u
    public KeyCode interactKey = KeyCode.E;

    // Etkile�imde olunan objeyi takip etmek i�in
    private GameObject currentInteractable;

    void Update()
    {
        // Ray'i player objesinin pozisyonundan ve forward y�n�nden g�nderiyoruz
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Raycast g�nder ve �arpan bir objeyi kontrol et
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            // �arp�lan objenin tag'ini kontrol et
            if (hitObject.CompareTag("Interactable"))
            {
                // �u an etkile�imde olunan objeyi takip et
                currentInteractable = hitObject;

                // UI veya benzeri bir bilgi g�stermek i�in
                Debug.Log("Etkile�im m�mk�n: " + hitObject.name);

                // Oyuncu "E" tu�una basarsa
                if (Input.GetKeyDown(interactKey))
                {
                    InteractWithObject(hitObject);
                }
            }
            else
            {
                // E�er tag e�le�mezse, s�f�rla
                currentInteractable = null;
            }
        }
        else
        {
            // E�er hi�bir �eye �arpm�yorsa, s�f�rla
            currentInteractable = null;
        }
    }

    // Etkile�im i�lemleri burada yap�l�r
    private void InteractWithObject(GameObject interactable)
    {
        Debug.Log(interactable.name + " ile etkile�ime ge�ildi!");

        // �rne�in: Kap� a�ma, nesne alma gibi i�levler burada tan�mlanabilir
    }

    // Ray'i g�rselle�tirmek i�in
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
