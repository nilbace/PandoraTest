using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float maxSpeed;
    public float jumpPower;
    public float JumpTimer;
    public float JumpCoolTime = 0.2f;
    public bool isGround = true;
    public bool isLookingleft = false;
    public bool canDash = true;
    public float DashTimer;
    public float DashCoolTime = 0.2f;
    public float xSpeed;
    [SerializeField] SpriteRenderer spriteRenderer;
    enum colorState{
        white, red
    }
    [SerializeField] colorState myColorState = colorState.white;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
        JumpTimer+=Time.deltaTime;
        DashTimer+=Time.deltaTime;
        xSpeed = rigidbody2D.velocity.x;
        float h = Input.GetAxisRaw("Horizontal");  lookingLeftOrRight(h);
        rigidbody2D.AddForce(Vector2.right*h*100, ForceMode2D.Impulse);
        if (rigidbody2D.velocity.x > maxSpeed && DashTimer > DashCoolTime)//오른쪽
        {
            rigidbody2D.velocity = new Vector2(maxSpeed,rigidbody2D.velocity.y);//y값을 0으로 잡으면 공중에서 멈춰버림
        }
        else if (rigidbody2D.velocity.x < maxSpeed*(-1) && DashTimer > DashCoolTime)//왼쪽
        {
            rigidbody2D.velocity = new Vector2(maxSpeed*(-1), rigidbody2D.velocity.y);
        }

        if(Input.GetKey(KeyCode.Space) && isGround && JumpTimer>JumpCoolTime)
        {
            JumpTimer= 0;
            rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isGround= false;
        }
        else if(rigidbody2D.bodyType == RigidbodyType2D.Static && Input.GetKey(KeyCode.Space) && !isGround && JumpTimer> JumpCoolTime)
        {
            JumpTimer=0;
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }

        if(canDash && Input.GetKey(KeyCode.LeftShift) && DashTimer>DashCoolTime)
        {
            DashTimer = 0;
            canDash = false;
            Dash(isLookingleft);
        }
        if (rigidbody2D.velocity.x > maxDashSpeed)//오른쪽
        {
            rigidbody2D.velocity = new Vector2(maxDashSpeed,rigidbody2D.velocity.y);//y값을 0으로 잡으면 공중에서 멈춰버림
        }
        else if (rigidbody2D.velocity.x < maxDashSpeed*(-1))//왼쪽
        {
            rigidbody2D.velocity = new Vector2(maxDashSpeed*(-1), rigidbody2D.velocity.y);
        }
    }

    void lookingLeftOrRight(float dir)
    {
        if(dir == -1)
        {
            isLookingleft = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(dir == 1)
        {
            isLookingleft = false;
           transform.localScale = new Vector3(1, 1, 1); 
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("Wall") && JumpTimer > 0.1f)
        {
            canDash = true;
            rigidbody2D.bodyType = RigidbodyType2D.Static;
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        canDash=true;
        isGround=true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        isGround= false;
    }

    public float maxDashSpeed;
    void Dash(bool isLeft)
    {
        if(rigidbody2D.bodyType == RigidbodyType2D.Dynamic)
        {
            changeColor();
            if(isLeft)
            {
                rigidbody2D.AddForce(new Vector2(-100, 0), ForceMode2D.Impulse);
            }
         else
            {
                rigidbody2D.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
            }
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
