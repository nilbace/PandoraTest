using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point3After : MonoBehaviour
{
    public SpriteRenderer Bul;
    public Color StartBulColor;
    Vector3 V3StartBulColor;
    public Color EndBulColor;
    Vector3 V3EndBulColor;
    public Vector3 ForLerp;
    public float slope;
    public float Offset;
    public float changeTime;
    public float durationTime;
    public float DownTime;
    public float DownSlope;
    public float offsetTime;
    void Start()
    {
        StartCoroutine(OffsetControl());
        V3StartBulColor = new Vector3(StartBulColor.r, StartBulColor.g, StartBulColor.b);
        V3EndBulColor = new Vector3(EndBulColor.r, EndBulColor.g, EndBulColor.b);
    }

    void Update()
    {
        Offset += Time.deltaTime * slope;

        ForLerp = Vector3.Lerp(V3EndBulColor, V3StartBulColor, Offset);
        Color nowBulColor = new Color(); nowBulColor.r = ForLerp.x; nowBulColor.g = ForLerp.y; nowBulColor.b = ForLerp.z; nowBulColor.a = 1;
        Bul.color = nowBulColor;
    }  


IEnumerator OffsetControl()
{
    yield return new WaitForSeconds(offsetTime);
    while(true)
    {
        slope = 1f;
        yield return new WaitForSeconds(changeTime);
        slope = 0;
        yield return new WaitForSeconds(durationTime);
        slope = DownSlope;
        yield return new WaitForSeconds(DownTime);
    }
}

}
