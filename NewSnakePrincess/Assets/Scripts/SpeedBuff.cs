using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedBuff : MonoBehaviour
{
    public Image icon;

    public PlayerController playerController;

    public float duration;  //���� ���ӽð�
    public float currentTime;  //���� Ÿ�̸�
    public float coolTime;
    private bool isReady = false;

    private WaitForSeconds seconds = new WaitForSeconds(0.1f);

    public AudioSource skillUse;
    public AudioSource coolOn;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Invoke("GameReady", 3f); //(������ 3�� �ִٰ� ������)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isReady==true)
        {
            isReady = false;
            currentTime = duration;
            StartCoroutine(BuffOn());
            StartCoroutine(SpeedUp());
        }
        
    }

    IEnumerator BuffOn()
    {

        while (currentTime>0)
        {
            currentTime -= 0.1f;
            icon.fillAmount = currentTime / duration;
            yield return seconds;
        }
        currentTime = 0;

        while (currentTime < coolTime)
        {
            currentTime += 0.1f;
            icon.fillAmount = currentTime / coolTime;
            yield return seconds;
        }
        currentTime = coolTime;  //�̰� 1/1�� ���߷��� �� ��. �ʱ�ȭ�� ������Ʈ����.
        isReady = true;
    }
    IEnumerator SpeedUp()
    {
        skillUse.Play();
        playerController.speed = playerController.speed * 2;
        GameManager.instance.SpeedBuff(true, playerController.speed);
        GameManager.instance.buffOn = true;

        yield return new WaitForSeconds(duration);

        playerController.speed = playerController.speed / 2;
        GameManager.instance.SpeedBuff(false, playerController.speed);
        GameManager.instance.buffOn = false;

        yield return new WaitForSeconds(coolTime);
        coolOn.Play();
    }
    void GameReady()
    {
        isReady = true;
    }
}