using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Playables;

public class EventManager : MonoBehaviour
{
    public GameObject priest;
    public List<Light2D> WorldLights;
    public PlayableDirector Event1;
    public PlayableDirector CamOutEvent;
    List<float> firstInten;
    public float FadeinTime;
    int eventChecker = 1;
    public GameObject EventZone1;  //1번은 암전화면 첫 대사
    public GameObject EventZone2;  //2번은 암전화면 밝아짐 & 조작가능
    public GameObject PandoraEvent;
    public float timer;
    public float bendBodyTime =2f;  //사제 발견후 몸 숙임
    public float wakeuptime = 2f;   //일어남
    public float wakeAndWalk = 2f;  //일어나고 다시 걷기까지

    public float EnPanDiaDelay = 2f;
    
    private void Start() {
        Screen.SetResolution(640, 360, true);

        firstInten = new List<float>();

        foreach(Light2D light in WorldLights)
        {
            firstInten.Add(light.intensity);
            light.intensity = 0;
        }
    }

    void Update()
    {
        timer+=Time.deltaTime;
        if(eventChecker == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            //timer=0; eventChecker++;
        }
        if(eventChecker==1 && Input.GetKeyDown(KeyCode.Space))
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[0]);
            //간부님
        }

        if(eventChecker==2 && Input.GetKeyDown(KeyCode.Space) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[1]);
            //계승식은 끝내셨습니까
        }

        if(eventChecker==3 && Input.GetKeyDown(KeyCode.Space) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[2]);
            //들어가겠습니다
        }
        
        if(eventChecker==4 && Input.GetKeyDown(KeyCode.Space) && timer>0.5f)
        {
            timer=0;
            eventChecker++;
            StartCoroutine(Fadein());
        }

        if(eventChecker==5 && Input.GetKeyDown(KeyCode.Space) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[3]);
            PriMove.instance.CanWalk = true;
            //아무도 안 계십니까
            //하급사제 움직임 가능
        }

        if(eventChecker==6 && priest.transform.position.x > EventZone1.transform.position.x)
        //첫번째 이벤트존 뱀 앞에 도달
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = false;
            Event1.Play();
            //뱀 불 켜짐
        }

        if(eventChecker==7 && timer > 2f)
        //카메라 축소 이벤트
        {
            timer=0;
            eventChecker++;
            CamOutEvent.Play();
        }

        if(eventChecker==8 && timer>1f)
        {
            timer=0;
            eventChecker++;
            PriMove.instance.CanWalk = true;
        }

        //이제 의사까지 가는 중간에 시체 발견

        //의사 벽화에서 상호작용

        //시체와 피가 많아짐

        //감옥 벽화 근처로 가면 쿵쿵거리는거

        //빈벽화

        //제단 앞에서 제어 불가

        if(eventChecker==9 && priest.transform.position.x > PandoraEvent.transform.position.x)
        {
            timer=0;
            eventChecker++;

            StartCoroutine(EncounterPandora());
            PriMove.instance.CanWalk = true;
        }



        /* 
        if(orthSize < 7)
            {
                orthSize+=Time.deltaTime;
                mainCam.orthographicSize = orthSize;
            }
        */
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
