using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float moveSpeed;
    public float jumpPower;
    public float JumpTimer;
    public float JumpCoolTime = 0.2f;
    public bool isGround = true;
    public bool isLookingleft = false;
    public bool canDash = true;
    public float DashTimer;
    public float DashCoolTime = 0.2f;
    public float Dashheight;
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
        transform.Translate(new Vector3(h * moveSpeed, 0));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            JumpTimer= 0;
            transform.Translate(new Vector2(0, jumpPower));
            isGround= false;
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
        Dashheight = transform.position.y;

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
