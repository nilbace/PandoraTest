using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    public Animator animator;
    public static AniController instance;
    public enum PlayerState{
        CityIdle, CityWalk, CityAttack, CityFrontJump, CitySideJump,
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
                case PlayerState.CityFrontJump:
                {
                    animator.CrossFade("CityFrontJump",0);
                    break;
                }
                case PlayerState.CitySideJump:
                {
                    animator.CrossFade("CitySideJump",0);
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
