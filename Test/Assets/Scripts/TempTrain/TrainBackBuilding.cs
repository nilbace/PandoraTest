using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBackBuilding : MonoBehaviour
{
    public float floatingSpeed;   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-floatingSpeed*Time.deltaTime,0,0);
    }

    private void OnBecameInvisible() {
        transform.position+= new Vector3(23,0,0);
    }
}
