using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float moveSpeed;
    public float jumpPower;
    public float clingCoolTime;
    public float clingJumpPower;
    public bool isGround = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clingCoolTime+=Time.deltaTime;
        if(Input.GetKey(KeyCode.A) )
        {
            rigidbody2D.AddForce(new Vector2(-moveSpeed, 0));
        }

        if(Input.GetKey(KeyCode.D) )
        {
            rigidbody2D.AddForce(new Vector2(moveSpeed, 0));
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpPower));
            isGround= false;
        }

        if(rigidbody2D.bodyType == RigidbodyType2D.Static && Input.GetKeyDown(KeyCode.Space))
        {
            if(Input.GetKey(KeyCode.A))
            {
                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                clingCoolTime = 0f;
                rigidbody2D.AddForce(new Vector2(-clingJumpPower, jumpPower));
            }

            if(Input.GetKey(KeyCode.D))
            {
                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                clingCoolTime = 0f;
                rigidbody2D.AddForce(new Vector2(clingJumpPower, jumpPower));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall") && clingCoolTime > 0.1f)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
