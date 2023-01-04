using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
public class WorldManager : MonoBehaviour
{
    public Sprite dream;
    public Sprite City;
    public bool isCity = false;
    [SerializeField]SpriteRenderer mySR;
    public static WorldManager instance;
    public GameObject binaryTile;
    public GameObject dreamTile;
    public GameObject cityTile;
    public GameObject countdownBlock;
    private void Start() {
        instance = this;
        cityTile.SetActive(false);
    }
    public void Change()
    {
        if(isCity)
        {
            mySR.sprite = dream;
            isCity=false;
            cityTile.SetActive(false);
            dreamTile.SetActive(true);
        }
        else
        {
            mySR.sprite = City;
            isCity=true;
            dreamTile.SetActive(false);
            cityTile.SetActive(true);
        }
    }

    public void DestroyCountdownBlock()
    {
        StartCoroutine(breakthisObject());
    }

    IEnumerator breakthisObject()
    {
        countdownBlock.SetActive(false);
        yield return new WaitForSeconds(5);
        countdownBlock.SetActive(true);
    }
    
}
