using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public float speed;
    public float delayTime;
    public bool runEnd = false;
    public GameObject title;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(runEnd==false)
        player.transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        animator.SetBool("isdie", true);
        Invoke("ChangeRunEndTrue", delayTime);
        Invoke("ActiveTitle", delayTime + 1.0f);
    }

    void ChangeRunEndTrue()
    {
        runEnd = true;
    }

    void ActiveTitle()
    {
        title.SetActive(true);  //ºÎµúÈ÷¸é Å¸ÀÌÆ² ÄÔ
    }


}
