using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform altPoint;
    Animator anim;

    Vector3 hareketYonu = Vector3.up;
    Vector3 orijinalPos;
    Vector3 animPos;

    public LayerMask playerLayer;
    bool animasyonBaslasinMi;
    bool hareketEtsinmi = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        orijinalPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;
    }

    private void Update()
    {
        CarpismayiKontrolEt();
        AnimasyonuBaslat();
    }

    void CarpismayiKontrolEt()
    {
        if(hareketEtsinmi)
        {
            RaycastHit2D hit = Physics2D.Raycast(altPoint.position, Vector2.down, .1f, playerLayer);
            if (hit && hit.collider.gameObject.tag == "Player")
            {
                anim.Play("mat");
                animasyonBaslasinMi = true;
                hareketEtsinmi = false;
            }
        }
    }

    void AnimasyonuBaslat()
    {
        if (animasyonBaslasinMi)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);
            if(transform.position.y >= animPos.y)
            {
                hareketYonu = Vector3.down;
            }
            else if(transform.position.y <= orijinalPos.y)
            {
                animasyonBaslasinMi = false;
            }
        }
    }
}
