using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneGoStart : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    static void FirstLoad()

    {

        if (SceneManager.GetActiveScene().name.CompareTo("StartScene") != 0)

        {

            SceneManager.LoadScene("StartScene");

        }

    }
}


