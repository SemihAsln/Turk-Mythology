using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Raycast'in ulaþabileceði mesafe
    public float rayDistance = 5f;

    // Etkileþim tuþu
    public KeyCode interactKey = KeyCode.E;

    // Etkileþimde olunan objeyi takip etmek için
    private GameObject currentInteractable;

    void Update()
    {
        // Ray'i player objesinin pozisyonundan ve forward yönünden gönderiyoruz
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Raycast gönder ve çarpan bir objeyi kontrol et
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Çarpýlan objenin tag'ini kontrol et
            if (hitObject.CompareTag("Interactable"))
            {
                // Þu an etkileþimde olunan objeyi takip et
                currentInteractable = hitObject;

                // UI veya benzeri bir bilgi göstermek için
                Debug.Log("Etkileþim mümkün: " + hitObject.name);

                // Oyuncu "E" tuþuna basarsa
                if (Input.GetKeyDown(interactKey))
                {
                    InteractWithObject(hitObject);
                }
            }
            else
            {
                // Eðer tag eþleþmezse, sýfýrla
                currentInteractable = null;
            }
        }
        else
        {
            // Eðer hiçbir þeye çarpmýyorsa, sýfýrla
            currentInteractable = null;
        }
    }

    // Etkileþim iþlemleri burada yapýlýr
    private void InteractWithObject(GameObject interactable)
    {
        Debug.Log(interactable.name + " ile etkileþime geçildi!");

        // Örneðin: Kapý açma, nesne alma gibi iþlevler burada tanýmlanabilir
    }

    // Ray'i görselleþtirmek için
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
