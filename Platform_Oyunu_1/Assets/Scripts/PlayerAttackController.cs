using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] BoxCollider2D kilicVurusBox;
    [SerializeField] GameObject parlamaEfekt;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("EnemyLayer")))
        {
            if (other.CompareTag("Spider"))
            {
                if(parlamaEfekt)
                {
                    Instantiate(parlamaEfekt, other.transform.position, Quaternion.identity);
                }

                StartCoroutine(other.GetComponent<SpiderController>().GeriTepkiFunc());
            }
        }

        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("EnemyLayer")))
        {
            if (other.CompareTag("Bat"))
            {
                if (parlamaEfekt)
                {
                    Instantiate(parlamaEfekt, other.transform.position, Quaternion.identity);
                }
                other.GetComponent<BatController>().CaniAzalt();
            }
        }
    }   
}
