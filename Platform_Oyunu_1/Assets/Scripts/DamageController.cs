using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.instance.CaniAzalt();
            PlayerController.instance.GeriTepkiFunc();
        }
    }

    
    void Update()
    {
        
    }
}
