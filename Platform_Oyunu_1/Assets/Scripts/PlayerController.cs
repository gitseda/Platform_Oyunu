using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] Transform zeminKontrolNoktasi, mizrakCikisNoktasi;
    [SerializeField] Animator normalAnim, kilicAnim, mizrakAnim;
    [SerializeField] float geriTepkiSuresi, geriTepkiGucu;
    [SerializeField] SpriteRenderer normalSprite, kilicSprite, mizrakSprite;
    [SerializeField] GameObject normalPlayer, kilicPlayer, mizrakPlayer;
    [SerializeField] GameObject kilicVurusBox, atilacakMizrak;

    Rigidbody2D rb;

    public LayerMask zeminMaske;
    public float hareketHizi;
    public float jumpSpeed;
    float geriTepkiSayac;

    bool zemindemi;
    bool secondJump;
    bool yonSagdami;
    public bool playerCanVerdiMi;
    bool kiliciVurduMu;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        playerCanVerdiMi = false;
        kiliciVurduMu = false;
        kilicVurusBox.SetActive(false);
    }

    private void Update()
    {
        if (playerCanVerdiMi)
            return;

        if(geriTepkiSayac <= 0)
        {
            PlayerPlay();
            PlayerJump();
            YonuDegistir();

            if(normalPlayer.activeSelf)
            {
                normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }

            if (kilicPlayer.activeSelf)
            {
                kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, 1f);
            }

            if (mizrakPlayer.activeSelf)
            {
                mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, 1f);
            }

            if (Input.GetMouseButtonDown(0) && kilicPlayer.activeSelf)
            {
                kiliciVurduMu = true;
                kilicVurusBox.SetActive(true);
            }
            else
            {
                kiliciVurduMu = false;
            }

            if (Input.GetKeyDown(KeyCode.W) && mizrakPlayer.activeSelf)
            {
                mizrakAnim.SetTrigger("mizrakAtti");
                Invoke("mizragiFirlat", 0.8f);
            }

        }
        else
        {
            geriTepkiSayac -= Time.deltaTime;

            if(yonSagdami)
            {
                rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(geriTepkiGucu, rb.velocity.y);
            }
        }

         if(normalPlayer.activeSelf)
         {
             normalAnim.SetBool("zemindemi", zemindemi);
             normalAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
         }

         if(kilicPlayer.activeSelf)
         {
             kilicAnim.SetBool("zemindemi", zemindemi);
             kilicAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));    
         }

        if (mizrakPlayer.activeSelf)
        {
            mizrakAnim.SetBool("zemindemi", zemindemi);
            mizrakAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
        }

        if (kiliciVurduMu && kilicPlayer.activeSelf)
         {
             kilicAnim.SetTrigger("kilicVurdu");
         }
    } 

    void mizragiFirlat()
    {
        GameObject mizrak = Instantiate(atilacakMizrak, mizrakCikisNoktasi.position, mizrakCikisNoktasi.rotation);
        mizrak.transform.localScale = transform.localScale;
        mizrak.GetComponent<Rigidbody2D>().velocity = mizrakCikisNoktasi.right * transform.localScale.x * 5f;
        Invoke("HerseyiKapatNormaliAc", .1f);
    }

    void PlayerPlay()
    {
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * hareketHizi, rb.velocity.y);
    }

    void YonuDegistir()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            yonSagdami = true;
        }
    }

    void PlayerJump()
    {
        zemindemi = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, zeminMaske);

        if(Input.GetButtonDown("Jump") && (zemindemi || secondJump))
        {
            if(zemindemi)
            {
                secondJump = true;
            }
            else
            {
                secondJump = false;
            }

            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    public void GeriTepkiFunc()
    {
        geriTepkiSayac = geriTepkiSuresi;

        if (normalPlayer.activeSelf)
        {
            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, .5f);
        }

        if (kilicPlayer.activeSelf)
        {
            kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, .5f);
        }

        if (mizrakPlayer.activeSelf)
        {
            mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, .5f);
        }

        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void PlayerCanVerdi()
    {
        rb.velocity = Vector2.zero;
        playerCanVerdiMi = true;

        if (normalPlayer.activeSelf)
        {
            normalAnim.SetTrigger("canVerdi");
        }

        if (kilicPlayer.activeSelf)
        {
            kilicAnim.SetTrigger("canVerdi");
        }

        if (mizrakPlayer.activeSelf)
        {
            mizrakAnim.SetTrigger("canVerdi");
        }

        StartCoroutine(PlayerYokEtSahneYenile());
    }

    IEnumerator PlayerYokEtSahneYenile()
    {
        yield return new WaitForSeconds(2f);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NormaliKapatKiliciAc()
    {
        normalPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        kilicPlayer.SetActive(true);
    }

    public void HerseyiKapatMizrakAc()
    {
        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(true);
    }

    public void HerseyiKapatNormaliAc()
    {
        normalPlayer.SetActive(true);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
    }
}
