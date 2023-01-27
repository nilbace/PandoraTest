using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainShake : MonoBehaviour
{
    public float shakeCoolTime;
    public float shakeDuration;
    public float shakeScale;
    public Camera mainCam;
    private void Start() {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        while(true)
        {
            shakeScale = Random.Range(-0.5f, 0.5f);
            yield return new WaitForSeconds(shakeCoolTime);
            transform.position += new Vector3(shakeScale, -shakeScale,0);
            mainCam.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f),0);
            yield return new WaitForSeconds(shakeDuration);
            transform.position+= new Vector3(-shakeScale, shakeScale,0);
            mainCam.transform.position = new Vector3(0,0,-10);
        }
    }
}
