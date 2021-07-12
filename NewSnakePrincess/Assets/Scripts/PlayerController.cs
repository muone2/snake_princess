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
            player.tag = "Coin"; //������ �÷��̾ �ƴ� �ٸ� �ɷ�.
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
        {  //���� ���� Ű�� ������ ������ Ÿ���� �� ���ư��� �Ϸ���, ���� �������δ� �� ���� �Ϸ��� ���� �߰�
            if (Input.GetKeyDown(KeyCode.LeftArrow) && downKey != 'L' && downKey != 'R' && speed != 0)
            {
                GameManager.instance.AddArrowToTurnPoint(downKey);  //�� ������ ���� ��(����)�̹Ƿ� �ٲ�� ���� �˷������
                downKey = 'L';
                nowTime = delayTime;  //�� �ϸ� ������Ÿ�� ����
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
