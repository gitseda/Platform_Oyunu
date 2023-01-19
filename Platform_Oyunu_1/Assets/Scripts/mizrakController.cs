using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mizrakController : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boa") && !other.GetComponent<BoaController>().oldumu)
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<BoaController>().BoaOldu();
        }
    }


    void Update()
    {
        
    }
}
