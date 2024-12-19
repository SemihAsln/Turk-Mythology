using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleSpecificChildren : MonoBehaviour
{
    [SerializeField] private GameObject bow; // Ýlk obje
    [SerializeField] private GameObject sword; // Ýkinci obje
   
    private bool toggleState = true; // Baþlangýç durumu


    private void Start()
    {
        bow.SetActive(false);
    }
    void Update()
    {
        // Q tuþuna basýldýðýnda çalýþýr
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleChildren();
        }
    }

    private void ToggleChildren()
    {
        // Ýlk objeyi aktif/pasif yap
        if (bow != null)
        {
            bow.SetActive(toggleState);
        }

        // Ýkinci objeyi pasif/aktif yap
        if (sword != null)
        {
            sword.SetActive(!toggleState);
        }
       // StartCoroutine(Waitin());
        toggleState = !toggleState;
        // Toggle durumunu ters çevir

    }


   /* private IEnumerator Waitin() 
    {
        yield return new WaitForSeconds(2);
  
       
    }
   */
}
