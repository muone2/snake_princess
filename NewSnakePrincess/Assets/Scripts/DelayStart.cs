using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStart : MonoBehaviour
{
    public Sprite a;
    public Sprite b;
    public Sprite c;
    public Sprite d;

    public float time = 0;
    private SpriteRenderer spriteRenderer;
    public PlayerController controller;
    private float num;

    private bool isFinish1 =false;
    private bool isFinish2 =false;
    private bool isFinish3 =false;
    private bool isFinish4 =false;

    public AudioSource sound123;
    public AudioSource soundStart;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        num = controller.speed;
        controller.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        if(time<4)
        time += Time.deltaTime;

        if (time < 1)
        {
            if (isFinish1 == false)
            {
                sound123.Play();
                spriteRenderer.sprite = a;
                isFinish1 = true;
            }
        }
        else if (time < 2)
        {
            if (isFinish2 == false)
            {
                sound123.Play();
                spriteRenderer.sprite = b;
                isFinish2 = true;
            }
        }
        else if (time < 3)
        {
            if (isFinish3 == false)
            {
                sound123.Play();
                spriteRenderer.sprite = c;
                isFinish3 = true;
            }
        }
        else if (time < 4)
        {
            if (isFinish4 == false)
            {
                soundStart.Play();
                spriteRenderer.sprite = d;
                controller.speed = num;
                isFinish4 = true;
            }
        }
        else
            gameObject.SetActive(false);
    }

}
