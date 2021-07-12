using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager instance
    {
        get { if(m_instance == null) 
            { m_instance = FindObjectOfType<GameManager>(); }
            return m_instance; } //r��������δ� ���� ������ �� ��.
    }

    //======================================//

    public List<Vector2> turnPoint = new List<Vector2>();
    public int turePointCount = 0;

    public List<char> arrowToTurnPoint = new List<char>();
    public int arrowToTurnPointCount = 0;

    public List<GameObject> tail = new List<GameObject>();
    public List<Coin> coinScript = new List<Coin>();
    public int tailCount = 0;

    public bool coinMakeReady = false;

    public List<Coin> allCoinsScript = new List<Coin>();
    public int allCoinsScriptCount = 0;
    public bool buffOn = false;

    public GameObject gameoverUI;
    public AudioSource gameoverSound;
    public void AddTurnPoint(Vector2 point)
    {   if (tailCount >= 1) //������ ��������
        {
            turePointCount++;
            turnPoint.Insert(turePointCount - 1, point);
        }
        //0��° ����Ʈ�� ����, ī��Ʈ�� 1�� �ǰ���.
    }
    public void AddArrowToTurnPoint(char arrow)
    {
        if (tailCount >= 1)
        {
            arrowToTurnPointCount++;
            arrowToTurnPoint.Insert(arrowToTurnPointCount - 1, arrow);
        }
    }
    public void DeleteTurnPoint()
    {
        turePointCount--;
        turnPoint.RemoveAt(0);
    }
    public void DeleteArrowToTurnPoint()
    {
        arrowToTurnPointCount--;
        arrowToTurnPoint.RemoveAt(0);
    }

    public void MakeTail(GameObject coin, GameObject player)
    {
        tailCount++;
        tail.Insert(tailCount - 1, coin);
        coinScript.Insert(tailCount - 1, coin.GetComponent<Coin>());
        tail[tailCount - 1].SetActive(false);
        if (tailCount - 1 == 0)
        {
            tail[tailCount - 1].transform.position = player.transform.position;
        }
        else if (tailCount - 1 > 0)
        {
            tail[tailCount - 1].transform.position = tail[tailCount - 2].transform.position;  //�ڱ� �ٷ� �� ���� ���� ��ġ
            coinScript[tailCount - 1].targetCount = coinScript[tailCount - 2].targetCount;
        }

        tail[tailCount - 1].SetActive(true);
    }

    public void coinTargetCountDown()
    {
        for (int i = 1; i < tailCount + 1; i++)
        {
          // coinScript[tailCount - 1].TargetCountDown();
            coinScript[i - 1].TargetCountDown();
        }
    }
    public void gameover()
    {
        for (int i = 0; i < tailCount ; i++)
        {
            coinScript[i].speed=0;
        }
        //�÷��̾��� ���ǵ�� �÷��̾���Ʈ�ѷ����� �ٲ���.
        Invoke("gameoverUIOn", 3f);
    }

    public void SpeedBuff(bool on, float playerSpeed)
    {
        if (on == true)
        {
            for (int i = 0; i < tailCount; i++)
            {
                coinScript[i].speed = playerSpeed;
            }
        }
        else if (on == false)
        {
            for (int i = 0; i < tailCount; i++)
            {
                coinScript[i].speed = playerSpeed;
            }
        }
    }

    void gameoverUIOn()
    {
        gameoverUI.SetActive(true);
        gameoverSound.Play();
    }
}
