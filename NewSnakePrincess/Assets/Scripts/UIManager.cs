using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private int score = 0;
    public Text texts;
    public AudioSource buttonSound;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int i)
    {
        score += i*1000;
        texts.text = "Score : " + score;
    }

    public void Restart()
    {
        buttonSound.Play();
        SceneManager.LoadScene("GameScene");
    }
    public void GameESC()
    {
        buttonSound.Play();
        Application.Quit();
    }
}
