using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapManager : MonoBehaviour
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
            temp = Instantiate(FloralLeaf, new Vector3(Random.Range(-5f, 5f)+transform.position.x, transform.position.y,0), Quaternion.identity);
        }
    }
}
