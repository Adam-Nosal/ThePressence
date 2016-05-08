using UnityEngine;
using System.Collections;
using Helpers;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator mAnimator;

    



    void Awake()
    {
        InitVariables();
        PlayIdleAnimation();
    }

    
    private void InitVariables()
    {
        if (mAnimator == null)
            mAnimator = this.GetComponent<Animator>();


    }
    
    public void PlayReloadAnimation(bool origin)
    {
         mAnimator.SetBool(AnimatorHelper.PLAYER_IS_RELOADING_PARAM, true);
    }

    public void PlayShootAnimation(bool origin)
    {
         mAnimator.SetBool(AnimatorHelper.PLAYER_IS_SHOOTING_PARAM, true);
    }

    public void PlayIdleAnimation(){

              mAnimator.SetBool(AnimatorHelper.PLAYER_IS_SHOOTING_PARAM, false);
             mAnimator.SetBool(AnimatorHelper.PLAYER_IS_RELOADING_PARAM, false);
    }

    public void PlayWalkAnimation(float velocity)
    {
        mAnimator.SetFloat(AnimatorHelper.PLAYER_VELOCITY_PARAM, velocity);
    }
    

}
