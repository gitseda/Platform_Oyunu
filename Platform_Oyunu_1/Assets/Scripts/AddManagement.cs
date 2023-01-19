using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddManagement : MonoBehaviour
{
    [SerializeField] bool coin, iksir;
    [SerializeField] GameObject patlamaEfekt;
    bool add;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !add)
        {
            if(coin)
            {
                add = true;
                GameManager.instance.toplananCoinAdet++;
                UIManager.instance.CoinAdetGuncelle();
                Destroy(gameObject);
                Instantiate(patlamaEfekt, transform.position, Quaternion.identity);
            }
            if(iksir)
            {
                add = true;
                PlayerHealth.instance.CaniArttir();
                Destroy(gameObject);
                Instantiate(patlamaEfekt, transform.position, Quaternion.identity);
            }
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
