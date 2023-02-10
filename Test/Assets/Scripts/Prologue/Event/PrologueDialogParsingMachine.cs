using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PrologueDialogParsingMachine : MonoBehaviour
{
    public static PrologueDialogParsingMachine instance;

    public Dialogue[] infos;
    
    void Start()
    {
        //SaveJson();
        LoadJson();
        instance = this;
        
    }

    

    void SaveJson()
    {
        string fileName = "ProlgueDialogue";
        string path = Application.dataPath+"/"+fileName+".Json";
        DialogueWrapper data = new DialogueWrapper();
        data.infos = infos;
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    void LoadJson()
    {
        string fileName = "ProlgueDialogue";
        string path = Application.dataPath+"/"+fileName+".Json";

        var text = File.ReadAllText(path);
        var json=JsonUtility.FromJson<DialogueWrapper>(text);

        infos = json.infos;        
    }
}

[Serializable]
public class Dialogue  //Serializable과 public 필수
{
    public string Name;
    public bool isleft;
    public string diaData;
    public float speed;
    public float FontSize;
    public Dialogue()
    {
        Name = "이름입력";
        isleft = true;
        diaData = "Write Anything";
        speed = 5;
        FontSize=20;
    }

}

public class DialogueWrapper
{
    public Dialogue[] infos;
}