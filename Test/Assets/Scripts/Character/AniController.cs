using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    public Animator animator;
    public static AniController instance;
    public enum PlayerState{
        CityIdle, CityWalk, CityAttack, CityAttack2, CityUPJumping, CityDownJumping, CityLanding, CityClingLeft, CityClingLeftJump, CityClingRight, CityJumpDash, 

        DreamIdle, DreamWalk, DreamAttack 
    }
    public PlayerState playerState = PlayerState.CityIdle;
    public PlayerState lastPlayerState = PlayerState.CityIdle;

    void Start() {
        instance = this;
    }
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
                case PlayerState.CityWalk:
                {
                    animator.CrossFade("CityWalk",0, -1, 0);
                    break;
                }
                case PlayerState.CityUPJumping:
                {
                    animator.CrossFade("CityUPJumping",0);
                    break;
                }
                case PlayerState.CityDownJumping:
                {
                    animator.CrossFade("CityDownJumping",0);
                    break;
                }
                case PlayerState.CityLanding:
                {
                    animator.CrossFade("CityLanding",0);
                    break;
                }
                case PlayerState.CityClingLeft:
                {
                    animator.CrossFade("CityClingLeft",0);
                    break;
                }
                case PlayerState.CityClingLeftJump:
                {
                    animator.CrossFade("CityClingLeftJump",0);
                    break;
                }
                case PlayerState.CityJumpDash:
                {
                    animator.CrossFade("CityJumpDash",0);
                    break;
                }


                case PlayerState.DreamIdle:
                {
                    animator.CrossFade("DreamIdle",0);
                    break;
                }
            }
        }
    }
}
