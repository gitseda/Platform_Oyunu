using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilicPasif : MonoBehaviour
{
    public GameObject kilicVurusBox;

   
    public void KilicKapat()
    {
        kilicVurusBox.SetActive(false);
    }

   
    void Update()
    {
        
    }
}
