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
    public bool isLookingleft = false;
    public bool canDash = true;
    [SerializeField] SpriteRenderer spriteRenderer;
    enum colorState{
        white, red
    }
    [SerializeField] colorState myColorState = colorState.white;
    void Start()
    {
        
    }

    void Update()
    {
        if(isLookingleft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
           transform.localScale = new Vector3(1, 1, 1); 
        }

        clingCoolTime+=Time.deltaTime;
        if(Input.GetKey(KeyCode.A) )
        {
            isLookingleft = true;
            rigidbody2D.AddForce(new Vector2(-moveSpeed, 0));
        }

        if(Input.GetKey(KeyCode.D) )
        {
            isLookingleft = false;
            rigidbody2D.AddForce(new Vector2(moveSpeed, 0));
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpPower));
            isGround= false;
        }

        if(rigidbody2D.bodyType == RigidbodyType2D.Static && Input.GetKeyDown(KeyCode.Space) && !isGround)
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

        if(canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            canDash = false;
            Dash(isLookingleft);
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

    private void OnCollisionStay2D(Collision2D other) {
        canDash=true;
        
    }

    public float DashPower;
    void Dash(bool isLeft)
    {
        changeColor();
        if(isLeft)
        {
            rigidbody2D.AddForce(new Vector2(-DashPower, 0));
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(DashPower, 0));
        }
    }

    void changeColor()
    {
        if(myColorState == colorState.white)
        {
            spriteRenderer.color= Color.red;
            myColorState = colorState.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
            myColorState = colorState.white;
        }
    }
}
