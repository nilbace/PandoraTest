using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject atkRangeCollider;
    public static AttackController instance;
    public float durationtime = 0.2f;
    private void Start() {
        instance = this;
    }
    public void CityAttack()
    {
        StartCoroutine(AttackCor());
    }

    IEnumerator AttackCor()
    {
        atkRangeCollider.SetActive(true);
        yield return null;
        atkRangeCollider.SetActive(false);
    }

    public void EndAttack()
    {
        print("end");
        AniController.instance.playerState = AniController.PlayerState.CityIdle;
    }
}
