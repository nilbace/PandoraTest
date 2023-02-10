using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloralLeaf : MonoBehaviour
{
    public Color AColor;
    public Color ZColor;
    public Rigidbody2D myrgbd;
    public SpriteRenderer mySprend;
    float colorLerp;
    bool StopFall= false;
    void Start()
    {
        colorLerp = Random.Range(0f, 1f);
        float red = Mathf.Lerp(AColor.r, ZColor.r, colorLerp);
        float green = Mathf.Lerp(AColor.g, ZColor.g, colorLerp);
        float blue = Mathf.Lerp(AColor.b, ZColor.b, colorLerp);
        Color myColor = new Color();
        myColor.r = red; myColor.g = green; myColor.b = blue;
        myColor.a = 1;

        mySprend.color = myColor;
        myrgbd.velocity = new Vector2(Random.Range(-0.4f, -0.7f), Random.Range(0f,1f));
        StartCoroutine(Rotate());
        Destroy(gameObject, 5f);
    }

    public void newV()
    {
        StartCoroutine(newvelo());
    }

    IEnumerator newvelo()
    { yield return new WaitForSeconds(0.1f);
        myrgbd.velocity = new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(0, 1f));

    }

    void Update()
    {            

    }
/*
private void OnTriggerEnter2D(Collider2D other){
       if(other.gameObject.tag=="Ground")
        {StopFall = true;
            myrgbd.bodyType = RigidbodyType2D.Static;

        Destroy(gameObject);
        }
    }
    */

    IEnumerator Rotate()
    {
        while(!StopFall)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.2f));
            transform.Rotate(Random.Range(0,40f), Random.Range(0,41), Random.Range(0f,42));
            transform.Translate(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
        }
    }
}
