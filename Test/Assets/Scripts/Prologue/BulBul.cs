using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulBul : MonoBehaviour
{
    Color bamColor;
    SpriteRenderer mySPrite;
    public float slope;
    public float Offset;
    public float changeTime;
    public float durationTime;
    void Start()
    {
        bamColor = GetComponent<SpriteRenderer>().color;
        mySPrite = GetComponent<SpriteRenderer>();
        StartCoroutine(OffsetControl());
    }

    void Update()
    {
        Offset += Time.deltaTime * slope;

        Color newcolor = bamColor;
        newcolor.r = 0.2f + Offset/6f;
        newcolor.b = newcolor.r+0.1f;
        mySPrite.color = newcolor;
    }  


IEnumerator OffsetControl()
{
    while(true)
    {
        slope = 1;
        yield return new WaitForSeconds(changeTime);
        slope = 0;
        yield return new WaitForSeconds(durationTime);
        slope = -1;
        yield return new WaitForSeconds(changeTime);
    }
}

}
