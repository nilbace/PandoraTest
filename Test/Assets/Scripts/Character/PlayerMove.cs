using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public Rigidbody2D rigidbody2D;
    public float forDebugTimeScale = 1;
    [Header("Move")]
    public bool canmove = true;
    public float maxSpeed;
    public bool isLookingleft = false;
    float originGravity = 19.6f;

    [Header("Jump")]
    public float jumpTimer;
    public float jumpLimitTime = 0.5f;
    public float maxjumpSpeed;
    public bool canJump = true;
    public bool isJumping;
    public bool wasjumping;
    public bool isGround;
    public bool isLanding;
    public float landingtime;

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
    public float NorMalDashCoolTime = 0.5f;
    public float NormalDashTimer;
    public bool canNormalDash = true;
    
    [Header("Attack")]
    public bool canAttack = true; //일단 임시
    [SerializeField] float attCooltime = 0.5f;
    public float atkTimer;
    public bool isAttacking = false;

    [Header("Cling")]
    public bool isclingleft;
    public bool isclingright;
    public bool iscling;
    public bool leftwall;
    public bool rightwall;
    public bool firsttouch = true;
    public float clingJumpPowerX;
    public float clingJumpPowerY;
    public bool isclingJumping;
    Vector3 clingPoz;

    

    

    private void Awake() {
        instance=this;
        Time.timeScale = forDebugTimeScale;
    }

    void FixedUpdate()
    {
        DashTimer+=Time.deltaTime; NormalDashTimer += Time.deltaTime;
        atkTimer += Time.deltaTime;
        
        if(atkTimer > attCooltime)
        {
            isAttacking = false;
        }

        
        float h = Input.GetAxisRaw("Horizontal");
        if(!isDash && !isJumping && !isAttacking && !isLanding && !isclingJumping && h==0)
        {
            AniController.instance.playerState = AniController.PlayerState.CityIdle;
        }

        if(!isDash && !isJumping && !isAttacking && !isLanding && !isclingJumping && h!=0)
        {
            AniController.instance.playerState = AniController.PlayerState.CityWalk;
        }

        if(isJumping && !isDash && !isLanding && !isclingJumping)
        {
            if(rigidbody2D.velocity.y > 0)
            {
                AniController.instance.playerState = AniController.PlayerState.CityUPJumping;
            }
            else
            {
                AniController.instance.playerState = AniController.PlayerState.CityDownJumping;
            }
        }

        if(isJumping && isDash)
        {
            AniController.instance.playerState = AniController.PlayerState.CityJumpDash;
        }

        if(wasjumping == true && isGround)
        {
            isLanding = true; wasjumping=false;
            startLanding(); //landingtime 후 landing 을 flase로
            AniController.instance.playerState = AniController.PlayerState.CityLanding;
        }

        if(!isDash && canmove) 
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
        {                                       //유지
            if(canJump && isGround && !isDash && !iscling && !isclingJumping) //점프시작
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxjumpSpeed);
                rigidbody2D.AddForce(new Vector2(0,500), ForceMode2D.Impulse);
                if(rigidbody2D.velocity.y > maxjumpSpeed)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x ,maxjumpSpeed);
                }
            }
                                                                //유지
            if(canJump && jumpTimer < jumpLimitTime && !isDash && !iscling && !isclingJumping) //점프를 누르는 중
            {
                isJumping=true;
                wasjumping = true;
                jumpTimer+=Time.deltaTime;
                rigidbody2D.AddForce(new Vector2(0,500), ForceMode2D.Impulse);
                if(rigidbody2D.velocity.y > maxjumpSpeed)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x ,maxjumpSpeed);
                }
            }

            if(leftwall)  //왼쪽벽 > 점프
            {
                if(h==1)
                {   
                    isclingleft = false; iscling =false;
                    leftwall = false; isclingJumping = true;
                    AfterClingJump();
                    rigidbody2D.AddForce(new Vector2(clingJumpPowerX,clingJumpPowerY), ForceMode2D.Impulse);
                }
                /*else if(h==-1) //기어올라감
                {
                    iscling = false;
                    leftwall = false; isclingJumping = true;
                    AfterClingJump();
                    rigidbody2D.AddForce(new Vector2(-clingJumpPowerX,clingJumpPowerY), ForceMode2D.Impulse);
                
                }*/
            }
            if(rightwall)  //오른벽 > 점프
            {
                if(h==1)
                {   
                    /*iscling = false;
                    rightwall = false; isclingJumping = true;
                    AfterClingJump();
                    rigidbody2D.AddForce(new Vector2(clingJumpPowerX,clingJumpPowerY), ForceMode2D.Impulse);*/
                }
                else if(h==-1) 
                {
                    iscling = false;  isclingright =false;
                    rightwall = false; isclingJumping = true;
                    AfterClingJump();
                    rigidbody2D.AddForce(new Vector2(-clingJumpPowerX,clingJumpPowerY), ForceMode2D.Impulse);
                
                }
            }


        }

        if(!Input.GetKey(KeyCode.Space))
        {
            if(isJumping && !isGround)
            {
                canJump=false;
            }
        }

        if(Input.GetKey(KeyCode.C))  //이면대쉬
        {
            if(canDash && DashTimer>DashCoolTime) 
            {
                StartCoroutine(Dash());
                if(isJumping && canDash) {  canDash = false; jumpTimer+=5;}
            }
        }

        if(Input.GetKey(KeyCode.X))   //일반대쉬
        {
            if(canNormalDash && NormalDashTimer>NorMalDashCoolTime) 
            {
                StartCoroutine(NormalDash());
                if(isJumping && canNormalDash) {  canNormalDash = false; jumpTimer+=5;}
            }
        }

        if(Input.GetKey(KeyCode.Z))   //기본공격
        {
            if(canAttack && atkTimer > attCooltime)
            {   
                atkTimer = 0;
                isAttacking = true;
                AttackController.instance.CityAttack();
                AniController.instance.playerState=AniController.PlayerState.CityAttack;
                print("StartAtk");
            }
        }
        

        #region 벽, 바닥 검사 레이케스트

        //바닥 검사
        RaycastHit2D rayHit = new RaycastHit2D();
        if(!isLookingleft)    
        {
            Debug.DrawRay(rigidbody2D.position+new Vector2(-0.4f,0), Vector3.down, new Color(0,1,0));
            rayHit = Physics2D.Raycast(rigidbody2D.position+new Vector2(-0.4f,0), Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        }
        else                  
        {
            Debug.DrawRay(rigidbody2D.position+new Vector2(0.4f,0), Vector3.down, new Color(0,1,0));
            rayHit = Physics2D.Raycast(rigidbody2D.position+new Vector2(0.4f,0), Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        }
        //왼쪽 오른쪽 방향 검사 캐릭터 뒷 발끝이 기준

        if(rayHit.collider != null)   //바닥에 닿았다면 초기화해줄것들
        {
            isGround=true;
            jumpTimer=0;
            canDash=true;
            isJumping=false;
            canJump=true;
            canNormalDash=true;
        }
        else
        {
            isGround=false;
        }

        
        //왼쪽벽
        RaycastHit2D rayhit2 = Physics2D.Raycast(rigidbody2D.position, Vector3.left, 0.3f, LayerMask.GetMask("LeftWall"));
        Debug.DrawRay(rigidbody2D.position, Vector3.left, new Color(0,1,0));
        if(rayhit2.collider != null)
        {
            print(5);
            iscling = true; isclingleft=true;
            canDash = false;
            canNormalDash = false;
            jumpTimer=0;
            if(firsttouch) 
            {
                clingPoz = transform.position; firsttouch = false;
            }

            leftwall= true;
        }
        else
        {
            //iscling = false; 
            isclingleft=false;
        }

        //오른벽
        RaycastHit2D rayhit3 = Physics2D.Raycast(rigidbody2D.position, Vector3.right, 0.3f, LayerMask.GetMask("RightWall"));
        Debug.DrawRay(rigidbody2D.position, Vector3.right, new Color(0,1,0));
        if(rayhit3.collider != null)
        {
            print(6);
            iscling = true;  isclingright=true;
            canDash = false;
            canNormalDash = false;
            jumpTimer=0;
            if(firsttouch) 
            {
                clingPoz = transform.position; firsttouch = false;
            }

            rightwall= true;
        }
        else
        {
            //iscling = false; 
            isclingright=false;
        }

        if(iscling)
        {
            canmove=false;
            transform.position = clingPoz;
            canDash = false;
            canNormalDash = false;
            AniController.instance.playerState = AniController.PlayerState.CityClingLeft;

        }
        #endregion
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

    void startLanding()
    {
        StartCoroutine(Landing());
    }

    IEnumerator Landing()
    {
        yield return new WaitForSeconds(landingtime);
        isLanding=false;
    }

    IEnumerator Dash()
    {
        WorldManager.instance.Change();
        DashTimer=0;
        rigidbody2D.gravityScale = 0;
        isDash = true;
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
        rigidbody2D.gravityScale = originGravity;
    }
   
    IEnumerator NormalDash()
    {
        if(isLookingleft) gameObject.GetComponent<TrailEffect>().StartTrailLeft();
        else gameObject.GetComponent<TrailEffect>().StartTrail();
        NormalDashTimer=0;
        rigidbody2D.gravityScale = 0;
        isDash = true; //공용
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
        isDash=false;//공용
        rigidbody2D.gravityScale = originGravity;
    }

    void AfterClingJump()
    {
        StartCoroutine(afterclingjump());
    }

    public float clingjumptime;
    IEnumerator afterclingjump()
    {
        yield return new WaitForSeconds(0.1f);
        canDash = true;
        canNormalDash = true;
        canmove=true;
        firsttouch=true;

        yield return new WaitForSeconds(clingjumptime);
        isclingJumping = false;
    }
}
