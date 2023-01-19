using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField] float takipMesafesi = 8f;
    [SerializeField] float batHizi;
    [SerializeField] Transform hedefPlayer;
    [SerializeField] GameObject iksirPrefab;
    Animator anim;
    Rigidbody2D rb;
    BoxCollider2D batCollider;
    public float atakYapmaSuresi;
    float mesafe, atakYapmaSayac;
    Vector2 hareketYonu;
    public int maxSaglik;
    int gecerliSaglik;
    
    private void Awake()
    {
        hedefPlayer = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        batCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
    }

    private void Update()
    {
        if(atakYapmaSayac<0)
        {

            if(hedefPlayer && gecerliSaglik > 0 && !PlayerController.instance.playerCanVerdiMi)
            {
                mesafe = Vector2.Distance(transform.position, hedefPlayer.position);

                if (mesafe < takipMesafesi)
                {
                    anim.SetTrigger("ucusaGecti");
                    hareketYonu = hedefPlayer.position - transform.position;

                    if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }

                    rb.velocity = hareketYonu * batHizi;
                }
            }
        }
        else
        {
            atakYapmaSayac -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(batCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if(other.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                atakYapmaSayac = atakYapmaSuresi;
                anim.SetTrigger("atakYapti");

                other.GetComponent<PlayerController>().GeriTepkiFunc();
                other.GetComponent<PlayerHealth>().CaniAzalt();
            }
        }
    }
    
    public void CaniAzalt()
    {
        gecerliSaglik--;
        atakYapmaSayac = atakYapmaSuresi;
        rb.velocity = Vector2.zero;

        if(gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            batCollider.enabled = false;
            Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            anim.SetTrigger("canVerdi");
            Destroy(gameObject, 1f); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }
}
