using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public GameObject trailObject;
   

    public void StartTrail()
    {
        StartCoroutine(starttrail());
    }

    public void StartTrailLeft()
    {
        StartCoroutine(starttrailLeft());
    }
    public float trailDelay;
    public float trailDestroyDelay;

    IEnumerator starttrail()
    {
        for(int i = 0; i < 5; i ++)
        {
            GameObject trailOne = Instantiate(trailObject, gameObject.transform.position, Quaternion.identity);
            trailOne.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            Color trailColor = trailOne.GetComponent<SpriteRenderer>().color;
            trailColor.a = 0.6f;
            trailColor.r = 0;
            trailColor.g = 0.7f;
            trailOne.GetComponent<SpriteRenderer>().color = trailColor;
            Destroy(trailOne, trailDestroyDelay);
            yield return new WaitForSeconds(trailDelay);
        }
    }

    IEnumerator starttrailLeft()
    {
        for(int i = 0; i < 5; i ++)
        {
            GameObject trailOne = Instantiate(trailObject, gameObject.transform.position,  Quaternion.Euler(0,180,0));
            trailOne.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            Color trailColor = trailOne.GetComponent<SpriteRenderer>().color;
            trailColor.a = 0.4f;
            trailColor.r = 0;
            trailColor.g = 0.7f;
            trailOne.GetComponent<SpriteRenderer>().color = trailColor;
            Destroy(trailOne, trailDestroyDelay);
            yield return new WaitForSeconds(trailDelay);
        }
    }
}
