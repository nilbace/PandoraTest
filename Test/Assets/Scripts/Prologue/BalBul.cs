using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalBul : MonoBehaviour
{
    public SpriteRenderer Bam;
    public SpriteRenderer Bul;
    public float slope;
    public float Offset;
    public float changeTime;
    public float durationTime;
    void Start()
    {
        StartCoroutine(OffsetControl());
    }

    void Update()
    {
        Offset += Time.deltaTime * slope;

        Color newcolor = Bam.color;
        newcolor.r = 0.4f + Offset/6f;
        Bam.color = newcolor;

        Color newBulColor = Bul.color;
        newBulColor.r = 0.2f + Offset/6f;
        newBulColor.b = newBulColor.r+0.1f;
        Bul.color = newBulColor;
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