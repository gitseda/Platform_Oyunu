using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] Transform[] positions;

    public float birdSpeed;
    public float idleTime;
    float idleSayac;
    int kacinciPos;

    Animator anim;
    Vector2 kusYonu;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        foreach (Transform pos in positions)
        {
            pos.parent = null;
        }
    }


    void Start()
    {
        kacinciPos = 0;
        transform.position = positions[kacinciPos].position;
    }

    void Update()
    {
        if (idleSayac > 0)
        {
            idleSayac -= Time.deltaTime;
            anim.SetBool("ucsunmu", false);
        }
        else
        {
            kusYonu = new Vector2(positions[kacinciPos].position.x - transform.position.x, positions[kacinciPos].position.y - transform.position.y);
            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x) * Mathf.Rad2Deg;

            if(transform.position.x > positions[kacinciPos].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }

            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = Vector3.MoveTowards(transform.position, positions[kacinciPos].position, birdSpeed * Time.deltaTime);
            anim.SetBool("ucsunmu", true);
            if (Vector3.Distance(transform.position, positions[kacinciPos].position) < 0.1)
            {
                PosizyonuDegistir();
                idleSayac = idleTime;
            }
        }
    }

    void PosizyonuDegistir()
    {
        kacinciPos++;
        if(kacinciPos >= positions.Length)
        {
            kacinciPos = 0;
        }
    }
}
