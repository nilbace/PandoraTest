using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurTrail : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(blur());
    }

    void Update()
    {
        
    }

    IEnumerator blur()
    {
        for(int i =0;i<5;i++)
        {
            yield return new WaitForSeconds(0.04f);
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a -= 0.08f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
