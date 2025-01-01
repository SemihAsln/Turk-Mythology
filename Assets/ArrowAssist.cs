using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAtMousePoint : MonoBehaviour
{
    public Camera mainCamera; // Ana kamera referans�
    public LayerMask groundLayer; // T�klanabilir zemin katman�
    public GameObject bow;
    public GameObject sword;

    private ToggleSpecificChildren toggle;

    private void Start()
    {
        toggle = GetComponent<ToggleSpecificChildren>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol mouse t�klamas�
        {
            if (toggle.toggleState==false)
            {
                
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
                    {
                        Vector3 targetPosition = hitInfo.point;

                        // Hedef pozisyonun Y eksenini objenin y�ksekli�i ile e�itle
                        targetPosition.y = transform.position.y;

                        // Objenin sadece Y ekseninde hedefe d�nmesini sa�la
                        Vector3 direction = (targetPosition - transform.position).normalized;
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
                    }
                
            }
        }
    }
}