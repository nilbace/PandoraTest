using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Animator animator;
    [Header("Move")]
    public float maxSpeed;
    public bool isLookingleft = false;

    [Header("Jump")]
    public float jumpTimer;
    public float jumpLimitTime = 0.5f;
    public float maxjumpSpeed;
    public bool jumpStarted;
    public bool isJumping;
    public bool isGround;

    [Header("Dash")]
    public bool isDash = false;
    public bool canDash = true;   //점프중엔 대쉬한번제한
    public float DashTimer;
    public float DashCoolTime = 0.5f;
    public float Dashheight;
    public float DashDuration= 0.2f;
    public float DashSpeed;
    [Header("NormalDash")]
    public float NormalDashSpeed;
    public float NormalDashDuration;
    [Header("Attack")]
    public bool canAttack = true; //일단 임시

    enum PlayerState{
        CityIdle, CityWalk, CityAttack,
        DreamIdle, DreamWalk, DreamAttack 
    }

    PlayerState playerState = PlayerState.CityIdle;
    PlayerState lastPlayerState = PlayerState.CityIdle;

    void FixedUpdate()
    {
        if(lastPlayerState != playerState)
        {
            lastPlayerState = playerState;
            switch(playerState)
            {
                case PlayerState.CityIdle:
                {
                    animator.CrossFade("StartUmbr",0);
                    break;
                }
                case PlayerState.CityAttack:
                {
                    animator.CrossFade("TempAttack", 0);
                    break;
                }
            }
        }
        DashTimer+=Time.deltaTime;
        float h = Input.GetAxisRaw("Horizontal");  
        if(!isDash) 
        {
            lookingLeftOrRight(h);
            rigidbody2D.AddForce(new Vector2(h*50,0), ForceMode2D.Impulse);
            if(!isDash &&  rigidbody2D.velocity.x > maxSpeed)
            {
                rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
            }
            else if(!isDash && rigidbody2D.velocity.x < -maxSpeed)
            {
                rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
            }
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(!jumpStarted && isGround && !isDash) //점프시작
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxjumpSpeed);
                jumpStarted = true;
                rigidbody2D.AddForce(new Vector2(0,500), ForceMode2D.Impulse);
                if(rigidbody2D.velocity.y > maxjumpSpeed)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x ,maxjumpSpeed);
                }
            }

            if(jumpStarted && jumpTimer < jumpLimitTime && !isDash) //점프를 누르는 중
            {
                isJumping=true;
                jumpTimer+=Time.deltaTime;
                rigidbody2D.AddForce(new Vector2(0,500), ForceMode2D.Impulse);
                if(rigidbody2D.velocity.y > maxjumpSpeed)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x ,maxjumpSpeed);
                }
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))  //이면대쉬
        {
            if(canDash && DashTimer>DashCoolTime) 
            {
                StartCoroutine(Dash());
                if(isJumping && canDash) {  canDash = false; jumpTimer+=5;}
            }
        }

        if(Input.GetKey(KeyCode.LeftControl))   //일반대쉬
        {
            if(canDash && DashTimer>DashCoolTime) 
            {
                StartCoroutine(NormalDash());
                if(isJumping && canDash) {  canDash = false; jumpTimer+=5;}
            }
        }

        if(Input.GetKey(KeyCode.J))   //기본공격
        {
            if(canAttack)
            {
                playerState = PlayerState.CityAttack;
                StartCoroutine(CityAttack());
            }
        }
        

        //바닥 검사
        if(!isLookingleft)    Debug.DrawRay(rigidbody2D.position+new Vector2(-0.5f,0), Vector3.down, new Color(0,1,0));
        else                  Debug.DrawRay(rigidbody2D.position+new Vector2(0.5f,0), Vector3.down, new Color(0,1,0));
        //왼쪽 오른쪽 방향 검사 캐릭터 뒷 발끝이 기준

        RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2D.position, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        if(rayHit.collider != null)   //바닥에 닿았다면 초기화해줄것들
        {
            isGround=true;
            jumpTimer=0;
            canDash=true;
            isJumping=false;
            jumpStarted=false;
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
    }



    IEnumerator Dash()
    {
        float gravityMemory = rigidbody2D.gravityScale;
        WorldManager.instance.Change();
        DashTimer=0;
        rigidbody2D.gravityScale = 0;
        isDash = true;
        if(WorldManager.instance.isCity)
        {
            animator.SetBool("isDream", true);
        }
        else
        {
            animator.SetBool("isDream", false);

        }

        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        if(isLookingleft == true)
        {
            rigidbody2D.AddForce(new Vector2(-DashSpeed,0), ForceMode2D.Impulse);
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(DashSpeed,0), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(DashDuration);
        rigidbody2D.velocity = Vector2.zero;
        isDash=false;
        rigidbody2D.gravityScale = gravityMemory;
    }
   
    IEnumerator NormalDash()
    {
        float gravityMemory = rigidbody2D.gravityScale;
        DashTimer=0;
        rigidbody2D.gravityScale = 0;
        isDash = true;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        if(isLookingleft == true)
        {
            rigidbody2D.AddForce(new Vector2(-NormalDashSpeed,0), ForceMode2D.Impulse);
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(NormalDashSpeed,0), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(NormalDashDuration);
        rigidbody2D.velocity = Vector2.zero;
        isDash=false;
        rigidbody2D.gravityScale = gravityMemory;
    }
    
    IEnumerator CityAttack()
    {
        yield return new WaitForSeconds(3f);
        playerState = PlayerState.CityIdle;
    }

}
