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
    }

    
    private void InitVariables()
    {
        if (mAnimator == null)
            mAnimator = this.GetComponent<Animator>();


    }
    


    public void PlayWalkAnimation(float hv, float vv)
    {
        mAnimator.SetFloat(AnimatorHelper.PLAYER_HORIZONTAL_VELOCITY_PARAM, hv);
        mAnimator.SetFloat(AnimatorHelper.PLAYER_VERTICAL_VELOCITY_PARAM, vv);

    }
    

}
