using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public AudioSource buttonsound;

    public GameObject infor;
    public GameObject startButton;
    public GameObject inforButton;

    public void SceneChange()
    {
        SceneManager.LoadScene("GameScene");
        buttonsound.Play();
    }

    public void infoOn()
    {
        infor.SetActive(true);
        startButton.SetActive(false);
        inforButton.SetActive(false);
        buttonsound.Play();
    }
    public void infoOff()
    {
        infor.SetActive(false);
        startButton.SetActive(true);
        inforButton.SetActive(true);
        buttonsound.Play();
    }

}
