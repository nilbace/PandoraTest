using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamera : MonoBehaviour
{
    public GameObject MainCam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MainCam.transform.position = new Vector3(transform.position.x+0.5f, MainCam.transform.position.y, MainCam.transform.position.z);
    }
}
