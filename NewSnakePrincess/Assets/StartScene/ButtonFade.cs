using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFade : MonoBehaviour
{
    public Image sr;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeIn");
        StartCoroutine("FadeInT");
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i <= 10; i++)
        {
            float f = i / 10.0f;
            Color c = sr.color;
            c.a = f;
            sr.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeInT()
    {

        for (int i = 0; i <= 10; i++)
        {
            float f = i / 10.0f;
            Color c = text.color;
            c.a = f;
            text.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    } 
}
