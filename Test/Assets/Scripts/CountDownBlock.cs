using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownBlock : MonoBehaviour
{
    [SerializeField]float timer;
    [SerializeField]bool contracting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(contracting) timer+=Time.deltaTime;
        if(timer > 5) 
        {
            timer = 0;
            WorldManager.instance.DestroyCountdownBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player") 
        {
            contracting = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag=="Player") 
        {
            contracting = false;
            timer=0;
        }
    }

    
}
