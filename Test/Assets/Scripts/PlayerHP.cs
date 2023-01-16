using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]bool isAlive = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(!isAlive)
        {

        }
    }

    public void Dead()
    {
        if(isAlive)
        {
            isAlive=false;
            Debug.Log("Die");
        }
    }
}
