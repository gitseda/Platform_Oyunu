using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] Slider spiderSlider;
    [SerializeField] GameObject iksirPrefab;

    public float spiderSpeed, beklemeSuresi, takipMesafesi;
    public int maxSaglik;
    int gecerliSaglik;
    float beklemeSayac;
    int kacinciPos;
    Animator anim;
    Transform hedefPlayer;
    BoxCollider2D spiderCollider;
    bool atakYapabilirmi;
    Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spiderCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        gecerliSaglik = maxSaglik;
        spiderSlider.maxValue = maxSaglik;
        SliderGuncelle();
        atakYapabilirmi = true;
        hedefPlayer = GameObject.Find("Player").transform;

        foreach (Transform pos in positions)
        {
            pos.parent = null;
        }
    }

    void Update()
    {
        if (!atakYapabilirmi)
        {
            return;
        }

        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("hareketEtsinmi", false);
        }
        else
        {
            if(hedefPlayer.position.x > positions[0].position.x && hedefPlayer.position.x < positions[1].position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, hedefPlayer.position, spiderSpeed * Time.deltaTime);
                anim.SetBool("hareketEtsinmi", true);
                if (transform.position.x > hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < hedefPlayer.position.x)
                {
                    transform.localScale = Vector3.one;
                }
            }
            else
            {
                anim.SetBool("hareketEtsinmi", true);

                if (transform.position.x > positions[kacinciPos].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < positions[kacinciPos].position.x)
                {
                    transform.localScale = Vector3.one;
                }

                transform.position = Vector3.MoveTowards(transform.position, positions[kacinciPos].position, spiderSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, positions[kacinciPos].position) < 0.1f)
                {
                    beklemeSayac = beklemeSuresi;
                    PozisyonuDegistir();
                }
            }
        }
    }

    void PozisyonuDegistir()
    {
        kacinciPos++;

        if(kacinciPos >= positions.Length)
        {
            kacinciPos = 0;
        }
    }

    private void OnDrawGizmosSeleceted()
    {
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(spiderCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirmi)
        {
            atakYapabilirmi = false;
            anim.SetTrigger("atakYapti");
            other.GetComponent<PlayerController>().GeriTepkiFunc();
            other.GetComponent<PlayerHealth>().CaniAzalt();

            StartCoroutine(YenidenAtakYapsin());
        }
    }

    IEnumerator YenidenAtakYapsin()
    {
        yield return new WaitForSeconds(1f);
        atakYapabilirmi = true;
    }

    public IEnumerator GeriTepkiFunc()
    {
        atakYapabilirmi = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);
        gecerliSaglik--;
        SliderGuncelle();

        if (gecerliSaglik <= 0)
        {
            atakYapabilirmi = false;
            gecerliSaglik = 0;
            Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            anim.SetTrigger("canVerdi");
            spiderCollider.enabled = false;
            spiderSlider.gameObject.SetActive(false);
            Destroy(gameObject, 2f);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);
                yield return new WaitForSeconds(0.05f);
            }

            anim.SetBool("hareketEtsinmi", false);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            atakYapabilirmi = true;
        }
    }
    void SliderGuncelle()
    {
        spiderSlider.value = gecerliSaglik;
    }
}
