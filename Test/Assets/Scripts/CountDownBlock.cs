using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownBlock : MonoBehaviour
{
    [SerializeField]float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 5) 
        {
            timer = 0;
            WorldManager.instance.DestroyCountdownBlock();
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag=="Player") timer+= Time.deltaTime;
        else timer=0;
    }

    
}
