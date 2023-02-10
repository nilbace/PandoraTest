using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriAUdio : MonoBehaviour
{
    AudioSource myAudio;
    bool nowisBlood = false;
    public AudioClip flat;
    public AudioClip blood;
    Rigidbody2D myrgbd;
    public float bloodLocation;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myrgbd = GetComponent<Rigidbody2D>();
    }


    void Update()
    { 
        if(myrgbd.velocity.x == 0) myAudio.mute = true;
        else                        myAudio.mute = false;

        if(transform.position.x > bloodLocation && nowisBlood == false)
        {
            myAudio.clip = blood;
            nowisBlood = true;
            myAudio.Play();
        }
        else if(transform.position.x < bloodLocation && nowisBlood == true)
        {
            myAudio.clip = flat;
            nowisBlood = false;
            myAudio.Play();
        }
    }
}
