using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject priest;
    public int eventChecker = 1;
    public GameObject camera;
    public float camerayPOs;

    //1번은 암전화면 첫 대사
    //2번은 암전화면 밝아짐 & 조작가능
    //3번은 이벤트존 도달시 다들 어디간거야 대사

    public GameObject EventZone1;
    public GameObject EventZone2;
    public GameObject BlackImage;
    public Image blackImage;
    public float timer;
    public float bendBodyTime =2f;  //사제 발견후 몸 숙임
    public float wakeuptime = 2f;   //일어남
    public float wakeAndWalk = 2f;  //일어나고 다시 걷기까지

    public float EnPanDiaDelay = 2f;
    
    private void Start() {
        camera.transform.position += new Vector3(0, camerayPOs, 0);
        BlackImage.SetActive(true);
        SpeechManager.instance.text.text = "";
    }

    void Update()
    {
        timer+=Time.deltaTime;
        camera.transform.position = new Vector3(priest.transform.position.x + 1, camera.transform.position.y, camera.transform.position.z);
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
            StartCoroutine(fadein());
        }

        if(eventChecker==5 && Input.GetKeyDown(KeyCode.Space) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[3]);
            //아무도 안 계십니까
        }

        if(eventChecker==6 && Input.GetKeyDown(KeyCode.Space) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[4]);
            //왜 대답이 없으시지
        }

        if(eventChecker==7 && Input.GetKeyDown(KeyCode.Space) && timer > 0.5f)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[5]);
            PriMove.instance.CanWalk = true;
            //원래 여기가 이렇게 붉었나
        }

        if(eventChecker==8 && priest.transform.position.x > EventZone1.transform.position.x)
        {
            timer=0;
            eventChecker++;
            SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[6]);
            //...!!
            PriMove.instance.CanWalk = false;
            StartCoroutine(CheckPriest());
        }

        if(eventChecker==9 && priest.transform.position.x > EventZone2.transform.position.x)
        {
            eventChecker++;
            PriMove.instance.CanWalk=false;
            StartCoroutine(EncounterPandora());
        }


        /* 
        if(orthSize < 7)
            {
                orthSize+=Time.deltaTime;
                mainCam.orthographicSize = orthSize;
            }
        */
    }

    IEnumerator fadein()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Color color = blackImage.color;
            color.a -= 0.1f;
            blackImage.color = color;
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
        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[10]);
        yield return new WaitForSeconds(EnPanDiaDelay);
        //네가 어쨰서

        SpeechManager.instance.PrintSpeech(PrologueDialogParsingMachine.instance.infos[11]);
        //왜

        //판도라 애니메이션

        //타이틀 띄움
    
    }
}
