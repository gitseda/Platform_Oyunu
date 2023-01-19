using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWeaponManager : MonoBehaviour
{
    [SerializeField] bool kilicmi, mizrakmi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(other != null && kilicmi)
            {
                other.GetComponent<PlayerController>().NormaliKapatKiliciAc();
                
            }

            if (other != null && mizrakmi)
            {
                other.GetComponent<PlayerController>().HerseyiKapatMizrakAc();
                
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
