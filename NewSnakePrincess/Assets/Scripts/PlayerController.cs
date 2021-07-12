using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public GameObject player;

    public float speed;
    public float delayTime;

    private float MoveDistance;
    private char downKey = 'R';
    private float nowTime;

    public AudioSource coinGetSound;
    public AudioSource dieSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tail" || collision.tag == "Rock")
        {
            animator.SetBool("isDie", true);
            GameManager.instance.gameover();
            speed = 0;
            player.tag = "Coin"; //적당히 플레이어가 아닌 다른 걸로.
            dieSound.Play();
        }
        if (collision.tag == "Coin")
        {
            coinGetSound.Play();
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        nowTime = delayTime;
    }

    void Update()
    {
        checkDownKey();
        MovePlayer();
        if (nowTime > -5)
        {
            nowTime -= Time.deltaTime;
        }
    }

    void checkDownKey()
    {
        if (nowTime <= 0)
        {  //현재 방향 키를 누르면 딜레이 타임이 안 돌아가게 하려고, 뒤쪽 방향으로는 못 가게 하려고 조건 추가
            if (Input.GetKeyDown(KeyCode.LeftArrow) && downKey != 'L' && downKey != 'R' && speed != 0)
            {
                GameManager.instance.AddArrowToTurnPoint(downKey);  //이 점까지 오는 길(방향)이므로 바뀌기 전에 알려줘야함
                downKey = 'L';
                nowTime = delayTime;  //턴 하면 딜레이타임 생김
                GameManager.instance.AddTurnPoint(player.transform.position);
                player.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && downKey != 'R' && downKey != 'L' && speed != 0)
            {
                GameManager.instance.AddArrowToTurnPoint(downKey);
                downKey = 'R';
                nowTime = delayTime;
                GameManager.instance.AddTurnPoint(player.transform.position);
                player.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && downKey != 'U' && downKey != 'D' && speed != 0)
            {
                GameManager.instance.AddArrowToTurnPoint(downKey);
                downKey = 'U';
                nowTime = delayTime;
                GameManager.instance.AddTurnPoint(player.transform.position);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && downKey != 'D' && downKey != 'U' && speed != 0)
            {
                GameManager.instance.AddArrowToTurnPoint(downKey);
                downKey = 'D';
                nowTime = delayTime;
                GameManager.instance.AddTurnPoint(player.transform.position);
            }
        }
    }

    void MovePlayer()
    {
        MoveDistance = speed * Time.deltaTime;

        switch (downKey)
        {
            case 'L':
                player.transform.Translate(new Vector3(-MoveDistance, 0, 0));
                break;
            case 'R':
                player.transform.Translate(new Vector3(MoveDistance, 0, 0));
                break;
            case 'U':
                player.transform.Translate(new Vector3(0, MoveDistance, 0));
                break;
            case 'D':
                player.transform.Translate(new Vector3(0, -MoveDistance, 0));
                break;

        }
    }
}
