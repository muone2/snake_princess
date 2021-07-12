using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool isTail = false;
    public int tailnum = 0;

    private GameObject player;
    public float speed = 10; //플레이어와 같다
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
        if (targetCount == GameManager.instance.turePointCount)  //더이상 타겟이 없으므로 플레이어 쫒음
        {
            Vector3 a = player.transform.position - gameObject.transform.position;
            Vector3 b = a.normalized; //이건 무조건 10, -10, 0-1, 01 넷 중 하나
            gameObject.transform.Translate(new Vector3(movedistance * b.x, movedistance * b.y, 0));
        }
        else if (targetCount < GameManager.instance.turePointCount)  //타겟을 쫒음
        {
            switch (GameManager.instance.arrowToTurnPoint[targetCount])
            {
                case 'L':
                    gameObject.transform.Translate(new Vector3(-movedistance, 0, 0));
                    if (gameObject.transform.position.x <= GameManager.instance.turnPoint[targetCount].x)  //타겟 지점을 통과하면
                    {
                        targetCount++;  //타겟을 다음으로 변경
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
            Debug.Log("무언가 잘못되었군요...(39)");
        }
    }

    public void deletePoint()
    {
        if (tailnum == GameManager.instance.tailCount) //마지막 꼬리면
        {
            GameManager.instance.DeleteArrowToTurnPoint();
            GameManager.instance.DeleteTurnPoint();          //지나갔을 0번 배열을 삭제 그리고.... 뭐해야하더라
            GameManager.instance.coinTargetCountDown();  //모든 코인의 타겟카운트 깍기
        }
    }
    public void TargetCountDown()
    {
        targetCount--;
    }
    public void changeTag()
    {
        gameObject.tag = "Tail";  //(태그 꼬리로 변경)
        if (tailnum <= 6)
            gameObject.tag = "TailD"; //(앞쪽 꼬리면 꼬리D로 변경)
    }

}
