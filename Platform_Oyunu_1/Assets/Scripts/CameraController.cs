using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController player;
    Vector2 sonPos;
    [SerializeField] Collider2D boundsBox;
    [SerializeField] Transform backgrounds;

    float halfH, halfW;

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        halfH = Camera.main.orthographicSize;
        halfW = halfH * Camera.main.aspect;
        sonPos = transform.position;
    }

    private void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfW, boundsBox.bounds.max.x - halfW),
                /*Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfH, boundsBox.bounds.max.y - halfH),*/
                transform.position.y,
                transform.position.z);
        }
        backgroundSpeed();
    }

    void backgroundSpeed()
    {
        Vector2 aradakiFark = new Vector2(transform.position.x - sonPos.x, transform.position.y - sonPos.y);
        backgrounds.position += new Vector3(aradakiFark.x, aradakiFark.y, 0f);
        sonPos = transform.position;
    }
}
