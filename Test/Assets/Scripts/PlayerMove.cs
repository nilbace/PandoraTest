using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    
    public float moveSpeed;
    public bool isLookingleft = false;


    public float jumpSpeed;
    public float jumpTimer;
    public float jumpLimit = 0.5f;
    public float fallingSpeed;
    public bool isJumping;
    public bool isGround;


    public bool isDash = false;
    public bool canDash = true;
    public float DashTimer;
    public float DashCoolTime = 0.5f;
    public float Dashheight;
    public float DashDuration= 0.2f;
    public float DashSpeed;



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
        DashTimer+=Time.deltaTime;
        jumpTimer+=Time.deltaTime;
        float h = Input.GetAxisRaw("Horizontal");  
        if(!isDash) 
        {
            lookingLeftOrRight(h);
            transform.Translate(new Vector3(h * moveSpeed, 0));
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(!isJumping && !isDash && isGround) isJumping=true;
            if(isJumping && jumpTimer<jumpLimit && !isDash)
            {
                transform.Translate(new Vector3(0, jumpSpeed));
            }
            if(jumpTimer>jumpLimit && !isDash)
            {
                transform.Translate(new Vector3(0, -fallingSpeed));
            }
        }
        else
        {
            isJumping=false;
            if(!isGround)
            {
                isJumping=false;
            }
            if(!isJumping && !isDash)
            {
                transform.Translate(new Vector3(0, -fallingSpeed));

            }
        } 

        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(canDash && DashTimer>DashCoolTime) StartCoroutine(Dash());
        }

        //바닥 검사
        Debug.DrawRay(rigidbody2D.position, Vector3.down, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2D.position, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        if(rayHit.collider != null)
        {
            isGround=true;
        }
        else
        {
            isGround=false;
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
        
        if(other.gameObject.CompareTag("Wall"))
        {
            canDash = true;
        }
        
    }

    private void OnCollisionStay2D(Collision2D other) {
        canDash=true;
        jumpTimer=0;
    }



    IEnumerator Dash()
    {
        WorldManager.instance.Change();
        DashTimer=0;
        changeColor();
        isDash = true;
        isJumping=false;
        if(isLookingleft == true)
        {
            rigidbody2D.velocity = new Vector2(-DashSpeed,0);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(DashSpeed,0);
        }
        yield return new WaitForSeconds(DashDuration);
        rigidbody2D.velocity = Vector2.zero;
        isDash=false;
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
