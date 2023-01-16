using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProCamera : MonoBehaviour
{
    public GameObject Player;
    public GameObject EventPos;
    public Camera mainCam;
    float orthSize = 2.11f;
    bool EventStarted = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(!EventStarted)
            gameObject.transform.position = new Vector3(Player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        
        if(gameObject.transform.position.x > EventPos.transform.position.x && !EventStarted)
        {
            EventStarted = true;
        }

        if(EventStarted)
        {
            PriMove.instance.CanWalk = false;
            //PriMove.instance.HandLight.SetActive(false);
            if(orthSize < 7)
            {
                orthSize+=Time.deltaTime;
                mainCam.orthographicSize = orthSize;
            }
        }
    }
}
