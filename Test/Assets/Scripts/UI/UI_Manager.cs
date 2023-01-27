using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public GameObject MenuImage;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public List<GameObject> Menus;
    public bool Opened = false;

    int screenYhalf;
    int screenXhalf;


    private void Awake() {
        instance=this;
        DontDestroyOnLoad(this);
    }

    private void Start() {
        screenYhalf = (int)Camera.main.orthographicSize;
        screenXhalf = (int)Camera.main.aspect;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Opened==false)
            {
                Time.timeScale=0;
                foreach(GameObject obj in Menus)
                {
                    obj.SetActive(true);
                }
                DropDown();
                Opened = true;
            }
            else
            {
                Time.timeScale=1;
                foreach(GameObject obj in Menus)
                {
                    obj.SetActive(false);
                }
                Opened = false;
            }
        }
    }
    Vector3 StartPoz = new Vector3(250  -960, -70 + 540, 0);
    Vector3 but1Poz = new Vector3(250 -960, -207 + 540, 0);
    Vector3 but2Poz = new Vector3(250 -960, -332 + 540,0);
    Vector3 but3Poz = new Vector3(250 -960, -447 + 540, 0);
    void DropDown()
    {
        StartCoroutine(movetoPos());
    }

    public float delaytime;
    IEnumerator movetoPos()
    {
        for(int i =0;i<20;i++)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            Button1.GetComponent<RectTransform>().localPosition = Vector3.Lerp(StartPoz, but1Poz, (float)i/19);
            Button2.GetComponent<RectTransform>().localPosition = Vector3.Lerp(StartPoz, but2Poz, (float)i/19);
            Button3.GetComponent<RectTransform>().localPosition = Vector3.Lerp(StartPoz, but3Poz, (float)i/19);
        }
    }
}
