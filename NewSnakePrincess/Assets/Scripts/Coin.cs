using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool isTail = false;
    public int tailnum = 0;

    private GameObject player;
    public float speed = 10; //�÷��̾�� ����
    public float movedistance;

    public int targetCount = 0;

    public float timeCount = 0;
    public bool followReady = false;

    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTail == true) {
            if (GameManager.instance.buffOn == false)
              timeCount += Time.deltaTime;
            if (GameManager.instance.buffOn == true)
              timeCount += Time.deltaTime * 2;
        }
        if (tailnum == 1) {
            if (timeCount > 0.2f)
                followReady = true;
        }
        else
        {
            if (timeCount > 0.15f)
                followReady = true;
        }
        if (isTail == true && followReady == true)
            FollowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Coin")
            {
                GameManager.instance.MakeTail(gameObject, collision.gameObject);
                tailnum = GameManager.instance.tailCount;

                Invoke("changeTag", 0f);
              
                isTail = true;
                GameManager.instance.coinMakeReady = true;
                if (GameManager.instance.buffOn == true)
                    speed = player.GetComponent<PlayerController>().speed;

                UIManager.instance.AddScore((tailnum-1)/10 +1);
            }
            else if (gameObject.tag == "Tail")
            {

            }
            else if (gameObject.tag == "TailD")
            {

            }

        }
    }

    public void FollowPlayer()
    {
        movedistance = speed * Time.deltaTime;
        if (targetCount == GameManager.instance.turePointCount)  //���̻� Ÿ���� �����Ƿ� �÷��̾� �i��
        {
            Vector3 a = player.transform.position - gameObject.transform.position;
            Vector3 b = a.normalized; //�̰� ������ 10, -10, 0-1, 01 �� �� �ϳ�
            gameObject.transform.Translate(new Vector3(movedistance * b.x, movedistance * b.y, 0));
        }
        else if (targetCount < GameManager.instance.turePointCount)  //Ÿ���� �i��
        {
            switch (GameManager.instance.arrowToTurnPoint[targetCount])
            {
                case 'L':
                    gameObject.transform.Translate(new Vector3(-movedistance, 0, 0));
                    if (gameObject.transform.position.x <= GameManager.instance.turnPoint[targetCount].x)  //Ÿ�� ������ ����ϸ�
                    {
                        targetCount++;  //Ÿ���� �������� ����
                        deletePoint();
                    }
                    break;
                case 'R':
                    gameObject.transform.Translate(new Vector3(movedistance, 0, 0));
                    if (gameObject.transform.position.x >= GameManager.instance.turnPoint[targetCount].x)
                    {
                        targetCount++;
                        deletePoint();
                    }
                    break;
                case 'U':
                    gameObject.transform.Translate(new Vector3(0, movedistance, 0));
                    if (gameObject.transform.position.y >= GameManager.instance.turnPoint[targetCount].y)
                    {
                        targetCount++;
                        deletePoint();
                    }
                    break;
                case 'D':
                    gameObject.transform.Translate(new Vector3(0, -movedistance, 0));
                    if (gameObject.transform.position.y <= GameManager.instance.turnPoint[targetCount].y)
                    {
                        targetCount++;
                        deletePoint();
                    }
                    break;
            }
        }
        else if (targetCount > GameManager.instance.turePointCount)
        {
            Debug.Log("���� �߸��Ǿ�����...(39)");
        }
    }

    public void deletePoint()
    {
        if (tailnum == GameManager.instance.tailCount) //������ ������
        {
            GameManager.instance.DeleteArrowToTurnPoint();
            GameManager.instance.DeleteTurnPoint();          //�������� 0�� �迭�� ���� �׸���.... ���ؾ��ϴ���
            GameManager.instance.coinTargetCountDown();  //��� ������ Ÿ��ī��Ʈ ���
        }
    }
    public void TargetCountDown()
    {
        targetCount--;
    }
    public void changeTag()
    {
        gameObject.tag = "Tail";  //(�±� ������ ����)
        if (tailnum <= 6)
            gameObject.tag = "TailD"; //(���� ������ ����D�� ����)
    }

}
