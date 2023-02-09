using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMan2 : MonoBehaviour
{
     public GameObject FloralLeaf;
    GameObject temp;
    float timer;
    public float regenTime;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer>regenTime)
        {
            timer=0;

            int rand = Random.Range(3,10);
            for(int i = 0; i< rand; i++)
            {
                GameObject instance = Instantiate(FloralLeaf, new Vector3(transform.position.x, transform.position.y,0), Quaternion.identity);
                instance.GetComponent<FloralLeaf>().newV();
            }

        }
    }
}
