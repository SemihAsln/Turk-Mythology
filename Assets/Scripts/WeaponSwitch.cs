using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleSpecificChildren : MonoBehaviour
{
    [SerializeField] private GameObject bow; // �lk obje
    [SerializeField] private GameObject sword; // �kinci obje
   
    private bool toggleState = true; // Ba�lang�� durumu


    private void Start()
    {
        bow.SetActive(false);
    }
    void Update()
    {
        // Q tu�una bas�ld���nda �al���r
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleChildren();
        }
    }

    private void ToggleChildren()
    {
        // �lk objeyi aktif/pasif yap
        if (bow != null)
        {
            bow.SetActive(toggleState);
        }

        // �kinci objeyi pasif/aktif yap
        if (sword != null)
        {
            sword.SetActive(!toggleState);
        }
       // StartCoroutine(Waitin());
        toggleState = !toggleState;
        // Toggle durumunu ters �evir

    }


   /* private IEnumerator Waitin() 
    {
        yield return new WaitForSeconds(2);
  
       
    }
   */
}
