using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBTNGlitter : MonoBehaviour
{
    public GameObject GlitEffect;
    
    public void Exit()
    {
        GlittingImage.instance.Exit();
    }

    public void Glitter()
    {
        GlittingImage.instance.Glitter();
        GlitEffect.transform.position = transform.position;
    }

    
}
