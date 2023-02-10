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
    public float Offset;
    void Start()
    {
        V3StartBulColor = new Vector3(StartBulColor.r, StartBulColor.g, StartBulColor.b);
        V3EndBulColor = new Vector3(EndBulColor.r, EndBulColor.g, EndBulColor.b);
    }

    void Update()
    {
        float dis = PriMove.instance.gameObject.transform.position.x-transform.position.x;
        float a = 1/(Mathf.Pow(1.3f,4)*10);
        Offset = -a*Mathf.Pow(dis,4)+1;


        ForLerp = Vector3.Lerp(V3EndBulColor, V3StartBulColor, Offset);
        Color nowBulColor = new Color(); nowBulColor.r = ForLerp.x; nowBulColor.g = ForLerp.y; nowBulColor.b = ForLerp.z; nowBulColor.a = 1;
        Bul.color = nowBulColor;
    }  
}
