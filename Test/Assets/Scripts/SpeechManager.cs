using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpeechManager : MonoBehaviour
{
    public static SpeechManager instance;
    public GameObject priest;
    public GameObject speechBubble;
    public RectTransform speechBubbleSize;
    public TMP_Text text;
    public Vector3 plusPoz;
    public float PrintingDelay = 0.1f;
    public float EndBubbleDelay = 3f;
 
    private void Start() {
        instance=this;
    }

    private void Update() {
        speechBubble.transform.position = priest.transform.position + plusPoz;
    }

    public void PrintSpeech(Dialogue dialogue)
    {
        StartCoroutine(printingSpeech(dialogue));
    }

    IEnumerator printingSpeech(Dialogue dialogue)
    {
        
        int nowLength;
        string nowString;
        PrintingDelay = dialogue.speed;
        for( nowLength = 1; nowLength < dialogue.diaData.Length; nowLength++)
        {
            yield return new WaitForSeconds(PrintingDelay);
            nowString = dialogue.diaData.Substring(0, nowLength+1);
            text.text = nowString;
            speechBubbleSize.sizeDelta = new Vector2(100f+(nowLength)*28f, speechBubbleSize.sizeDelta.y);
            speechBubble.SetActive(true);
        }
        if(nowLength == dialogue.diaData.Length)
        {
            yield return new WaitForSeconds(EndBubbleDelay);
            speechBubble.SetActive(false);
        }
    }
}
