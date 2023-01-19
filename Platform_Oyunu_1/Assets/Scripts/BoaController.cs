using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoaController : MonoBehaviour
{
    [SerializeField] float boaYurumeHizi, boaKosmaHizi;
    [SerializeField] float gorusMesafesi = 8f;
    [SerializeField] BoxCollider2D boaCollider;
    [SerializeField] GameObject bloodEfect;

    Animator anim;
    Rigidbody2D rb;
    
    public bool oldumu;
    public LayerMask playerLayer; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        oldumu = false;
    }

    
    void Update()
    {
        if(oldumu)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), gorusMesafesi, playerLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * gorusMesafesi, Color.green);

        transform.localScale = new Vector3(-1, 1, 1);

        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector2(-boaKosmaHizi, rb.velocity.y);
                anim.SetBool("kossunmu", true);
            }
        }
        else
        {
            rb.velocity = new Vector2(-boaYurumeHizi, rb.velocity.y);
            anim.SetBool("kossunmu", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(boaCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if(other.CompareTag("Player"))
            {
                anim.SetTrigger("Player");
                other.GetComponent<PlayerController>().GeriTepkiFunc();
                other.GetComponent<PlayerHealth>().CaniAzalt();
            }
        }
    }

    public void BoaOldu()
    {
        oldumu = true;
        anim.SetTrigger("canVerdi");
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        Instantiate(bloodEfect, transform.position, transform.rotation);

        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            box.enabled = false;
        }
        
        Destroy(gameObject, 3f);
    }
}
