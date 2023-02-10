using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Playables;

public class EventManager : MonoBehaviour
{
    [Header("Dev")]
    public bool DevMode;
    public int eventChecker = 0;

    public GameObject priest;

    [Header("Cam")]
    public GameObject Cam;
    float Zoomtime;


    public List<Light2D> WorldLights;
    public PlayableDirector Event1;
    public PlayableDirector CamOutEvent;
    AudioSource myaudio;
    public AudioSource BGM;
    [Header("AudioClip")]
    public AudioClip knock;
    public AudioClip opendoor;
    public AudioClip stair;
    public AudioClip hand;
    public AudioClip meetpandora;
    public AudioClip black;


    List<float> firstInten;
    public float FadeinTime;
    public GameObject EventZone1;  //1번은 뱀위치
    public GameObject EventZone2;  //2번은 의사위치
    public GameObject EventZone3;  //시체 위치
    public GameObject PandoraEvent;
    public float timer;
    public float bendBodyTime =2f;  //사제 발견후 몸 숙임
    public float wakeuptime = 2f;   //일어남
    public float wakeAndWalk = 2f;  //일어나고 다시 걷기까지

    public float EnPanDiaDelay = 2f;
    
    private void Start() {
        Screen.SetResolution(640, 360, true);
        myaudio = GetComponent<AudioSource>();
        firstInten = new List<float>();

        //어둠게 시작
        foreach(Light2D light in WorldLights)
        {
            firstInten.Add(light.intensity);
            light.intensity = 0;
        }
        if(DevMode)
        {
            StartCoroutine(Fadein());
            PriMove.instance.CanWalk = true;
            PriMove.instance.maxSpeed = 2f;
        }
        else
        {
            eventChecker=0;
        }
    }

    void Update()
    {
        Cam.transform.position = new Vector3(priest.transform.position.x+0.5f, Cam.transform.position.y, Cam.transform.position.z);
        timer+=Time.deltaTime;

        if(eventChecker == 0)
        {
            timer=0; eventChecker++;
            myaudio.clip = knock;
            myaudio.Play();
        }
        if(eventChecker==1 && timer > 2f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[0]);
            //간부님
        }

        if(eventChecker==2 && timer > 2f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[1]);
            //계승식은 끝내셨습니까
        }

        if(eventChecker==3 && timer > 2f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[2]);
            ZtoScreen();
            //들어가겠습니다
        }
        
        if(eventChecker==4 && Input.GetKeyDown(KeyCode.Z))
        {
            timer=0;
            eventChecker++;
            myaudio.clip = opendoor;
            myaudio.Play();
        }

        if(eventChecker==5 && timer>5f)
        {
            timer=0;
            eventChecker++;
            myaudio.clip = stair;
            myaudio.Play();
        }

        if(eventChecker==6 && timer > 4)
        {
            StartCoroutine(Fadein());
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[3]);
            PriMove.instance.CanWalk = true;
            BGM.Play();
            //아무도 안 계십니까
            //하급사제 움직임 가능
        }

        if(eventChecker==7 && priest.transform.position.x > EventZone1.transform.position.x)
        //첫번째 이벤트존 뱀 앞에 도달
        //단순줌아웃
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = false;
            print("Z키좀눌러라");
            //뱀 불 켜짐
        }

        if(eventChecker==8 && Input.GetKeyDown(KeyCode.Z))
        {
            timer=0;
            eventChecker++;
            StartCoroutine(ZoomOut());
        }

        if(eventChecker==9 && timer > 4f && Input.anyKey)
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = true;
            StartCoroutine(Zoomin());
        }

        //의사벽화 만남
        if(eventChecker==10 && priest.transform.position.x > EventZone2.transform.position.x)
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = false;
            print("Z키좀눌러라");
        }

        if(eventChecker==11 && Input.GetKeyDown(KeyCode.Z))
        {
            timer=0;
            eventChecker++;
            StartCoroutine(ZoomOut());
        }

        if(eventChecker==12 && timer>4f && Input.anyKey)
        {
            timer=0; eventChecker++;
            PriMove.instance.CanWalk=true;
            StartCoroutine(Zoomin());
        }

        //시체 조우
        if(eventChecker==13 && priest.transform.position.x > EventZone3.transform.position.x)
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = false;
            print("eventStart");
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[4]);
            // ...!
        }

        if(eventChecker==14 && Input.GetKeyDown(KeyCode.Z) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[5]);
            //무슨 일이
        }

        if(eventChecker==15 && Input.GetKeyDown(KeyCode.Z) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[6]);
            // 다른 사람들은?
        }

        if(eventChecker==16 && Input.GetKeyDown(KeyCode.Z) && timer>0.5f)
        //걷기 가능
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = true;
        }  
    }

    void ZtoScreen()
    {
        print("Z키가 화면에 나온다");
    }


    IEnumerator Fadein()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(FadeinTime/10f);

            for(int j = 0; j < WorldLights.Count; j++)
            {
                WorldLights[j].intensity = firstInten[j]*(float)i/9f;
            }
        }
    }

    float zoomScale;
    IEnumerator ZoomOut()
    {
        Zoomtime=3.2f;
        zoomScale=3f;
        while(timer<Zoomtime)
        {
            yield return null;
            Cam.transform.position = new Vector3 (priest.transform.position.x + 0.5f, Mathf.Lerp(0.4f, 1f, Mathf.Pow((timer/Zoomtime),zoomScale)), -10f);
            Camera temp = Cam.GetComponent<Camera>();
            temp.orthographicSize = Mathf.Lerp(0.92f, 1.67f, Mathf.Pow((timer/Zoomtime),zoomScale));
            
        }
    }

    IEnumerator Zoomin()
    {
        Zoomtime = 2f;
        zoomScale = 0.5f;
        while(timer<Zoomtime)
        {
            yield return null;
            Cam.transform.position = new Vector3 (priest.transform.position.x + 0.5f, Mathf.Lerp(1f, 0.4f, Mathf.Pow((timer/Zoomtime),zoomScale)), -10f);
            Camera temp = Cam.GetComponent<Camera>();
            temp.orthographicSize = Mathf.Lerp(1.67f, 0.92f, Mathf.Pow((timer/Zoomtime),zoomScale));
        }
    }

    IEnumerator CheckPriest()
    {
        //몸을 숙이는 애니
        yield return new WaitForSeconds(bendBodyTime);
        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[7]);
        //괜찮으십니까 사제님


        yield return new WaitForSeconds(wakeuptime);
        //일어나는 애니
        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[8]);
        //이게무슨일이야

        yield return new WaitForSeconds(wakeAndWalk);
        PriMove.instance.CanWalk = true;
        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[9]);
        //다른 사람들은?
    }

    IEnumerator EncounterPandora()
    {
        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[7]);
        yield return new WaitForSeconds(EnPanDiaDelay);
        //네가 어쨰서

        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[8]);
        //왜

        //판도라 애니메이션

        //타이틀 띄움
    
    }
}
