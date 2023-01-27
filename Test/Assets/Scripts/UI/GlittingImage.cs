using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlittingImage : MonoBehaviour
{
    public static GlittingImage instance;
    int count = 0;
    void Start()
    {
        instance = this;
    }

    public void Exit()
    {
        StopAllCoroutines();
        Color co = gameObject.GetComponent<Image>().color;
        co.a = 0;
        gameObject.GetComponent<Image>().color = co;
    }

    public void Glitter()
    {
        if(count >=0)
        {
            StopAllCoroutines();
        }
        StartCoroutine(glitting());
    }

    IEnumerator glitting()
    {
        for(int i =1;i<=10;i++)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            Color co = gameObject.GetComponent<Image>().color;
            co.a = (float)i/10;
            gameObject.GetComponent<Image>().color = co;
            count = i;
        }
        count = -1;
    }
}
