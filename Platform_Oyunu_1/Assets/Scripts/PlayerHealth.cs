using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxSaglik, gecerliSaglik;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        UIManager.instance.SliderGuncelle(gecerliSaglik, maxSaglik);
    }

    public void CaniAzalt()
    {
        gecerliSaglik--;
        UIManager.instance.SliderGuncelle(gecerliSaglik, maxSaglik);

        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            //gameObject.SetActive(false);
            PlayerController.instance.PlayerCanVerdi();
        }
    }
    public void CaniArttir()
    {
        gecerliSaglik++;
        if (gecerliSaglik >= maxSaglik)
            gecerliSaglik = maxSaglik;

        UIManager.instance.SliderGuncelle(gecerliSaglik, maxSaglik);
    }
}
