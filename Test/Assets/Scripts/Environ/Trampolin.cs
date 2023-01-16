using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    public float Power;
    public float controledGravity;
    public float jumptime;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Power), ForceMode2D.Impulse);
            StartCoroutine(GravityControl(other.gameObject));
        }
    }

    IEnumerator GravityControl(GameObject player)
    {
        float GravityMemory =  player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = controledGravity;
        yield return new WaitForSeconds(jumptime);
        player.GetComponent<Rigidbody2D>().gravityScale = GravityMemory;
    }
}
