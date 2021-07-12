using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{

    // Start is called before the first frame update
    int count = 0;
    public int maxBounceCount;
    public GameObject smoke;
    public GameObject shake;
    public GameObject player;
    public Animator animator;
    public GameObject button;
    public float surpriseTime;
    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        count++;

        if (count >= maxBounceCount) {
            Rigidbody2D a = GetComponent<Rigidbody2D>();
            Destroy(a);
        }
        if (count == 1)
        {
            audioSource.Play();
            smoke.SetActive(true);
            shake.SetActive(true);
            animator.SetBool("StartAni", true);
            animator.SetBool("StartAni2", true);
            animator.SetBool("StartAni3", true);
            Invoke("changeStartAni", surpriseTime);
            player.transform.rotation = Quaternion.Euler(0, 0, -45);
            player.transform.position += new Vector3(-0.5f, -0.4f, 0); 
        }
    }
    public void changeStartAni()
    {
        animator.SetBool("StartAni", false);
        animator.SetBool("StartAni3", false);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.transform.position += new Vector3(0.5f, 0.4f, 0);
        Invoke("buttonOn", 2.0f);
    }
    void buttonOn()
    {
        button.SetActive(true);
    }

}
