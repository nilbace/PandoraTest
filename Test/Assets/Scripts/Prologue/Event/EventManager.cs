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
    public float devModeSpeed;
    public int eventChecker = 0;
    [Header("Object")]

    public GameObject priest;
    public GameObject Hand1;
    public GameObject hand2;
    public GameObject prisoner;
    public GameObject fire;

    [Header("Cam")]
    public GameObject Cam;
    float Zoomtime;


    public List<Light2D> WorldLights;
    public PlayableDirector CamMove;
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

    [Header("Event")]
    public float EventZone1;  //1번은 죽은사제
    public float EventZone2;  //2번은 손1
    public float EventZone3;  //3번은 손2
    public float EventZone4;  //4번은 죄수 
    public float PandoraEvent;
    bool isEnding = false;
    public float timer;
    public float bendBodyTime =2f;  //사제 발견후 몸 숙임
    public float wakeuptime = 2f;   //일어남
    public float wakeAndWalk = 2f;  //일어나고 다시 걷기까지

    public float EnPanDiaDelay = 2f;
    
    private void Start() {
        Screen.SetResolution(640, 360, true);
        myaudio = GetComponent<AudioSource>();
        firstInten = new List<float>();
        jailSetting();

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
            PriMove.instance.maxSpeed = devModeSpeed;
        }
        else
        {
            eventChecker=0;
        }
    }

    private void LateUpdate() {
        if(!isEnding) Cam.transform.position = new Vector3(priest.transform.position.x+0.5f, Cam.transform.position.y, Cam.transform.position.z);

    }
    void Update()
    {
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

        #region  빛의 봉인검
        /* 당분간 봉인
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
            //StartCoroutine(ZoomOut());
        }

        if(eventChecker==9 && timer > 4f && Input.anyKey)
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = true;
            //StartCoroutine(Zoomin());
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
            //StartCoroutine(ZoomOut());
        }

        if(eventChecker==12 && timer>4f && Input.anyKey)
        {
            timer=0; eventChecker++;
            PriMove.instance.CanWalk=true;
            //StartCoroutine(Zoomin());
        }
        여기까지 봉인
        */
        #endregion

        //시체 조우
        if(eventChecker==7 && priest.transform.position.x > EventZone1)
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = false;
            StartCoroutine(movePoint5());
            
            // ...!
        }

        if(eventChecker==8 && Input.GetKeyDown(KeyCode.Z) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[5]);
            //무슨 일이
        }

        if(eventChecker==9 && Input.GetKeyDown(KeyCode.Z) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[6]);
            priest.GetComponent<PriMove>().isShaking = false;
            // 다른 사람들은?
        }

        if(eventChecker==10 && Input.GetKeyDown(KeyCode.Z) && timer>0.5f)
        //걷기 가능
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = true;
        }  
        
        //Prison
        if(eventChecker==11 && priest.transform.position.x > EventZone2)
        //손1
        {
            timer=0;
            eventChecker++;
            Hand1.SetActive(true);
            myaudio.clip = hand; myaudio.Play();
        } 

        if(eventChecker==12 && priest.transform.position.x > EventZone3)
        //손2
        {
            timer=0;
            eventChecker++;
            hand2.SetActive(true);
            myaudio.clip = hand; myaudio.Play();
        }

        if(eventChecker==13 && priest.transform.position.x > EventZone4)
        //죄수 완료
        {
            timer=0;
            eventChecker++;
            fire.SetActive(true);
            prisoner.SetActive(true);
            myaudio.clip = hand; myaudio.Play();
        }

        if(eventChecker==14 && priest.transform.position.x > PandoraEvent)
        {
            eventChecker++;
            PriMove.instance.CanWalk=false;
            isEnding=true;
            CamMove.Play();
        }
    }

    void ZtoScreen()
    {
        print("Z키가 화면에 나온다");
    }
    void jailSetting()
    {
        Hand1.SetActive(false);
        hand2.SetActive(false);
        fire.SetActive(false);
        prisoner.SetActive(false);
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
    public float deadlookDelay;
    public float shakedelay;
    IEnumerator movePoint5()
    {
        for(int i = 1;i<=30;i++)
        {
            yield return new WaitForSeconds(0.008f);
            float k = (float)i/30f;
            priest.transform.position = new Vector3(Mathf.Lerp(EventZone1, 16.6f, k),priest.transform.position.y,0);
        }
        yield return new WaitForSeconds(deadlookDelay);
        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[4]);
        yield return new WaitForSeconds(shakedelay);
        priest.GetComponent<PriMove>().isShaking = true;
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
