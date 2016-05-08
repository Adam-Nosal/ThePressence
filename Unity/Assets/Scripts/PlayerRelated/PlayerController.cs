using UnityEngine;
using CnControls;
using System.Collections;
using Helpers;

public class PlayerController : MonoBehaviour
{

 
    [Header("Controllers")]
    [SerializeField]
    private PlayerAnimationController mAnimationController;
    [SerializeField]
    private Rigidbody2D mRigidbody2d;
    
    private Transform mTransform;

    [Header("Joysticks")]
    [SerializeField]
    private SimpleJoystick movementJoy;

    [Header("Prefabs")]


    [Header("Movement")]
    [SerializeField]
    [Range(5.0f,15.0f)]
    private float speed = 7.5f;

 


    
    public Vector3 lookVector;
 



    void Awake()
    {
        InitVariables();
            
    }
    


    void Update()
    {
        float horiz = CnInputManager.GetAxis(InputHelper.MOVE_JOYSTICK_HORIZONTAL);
        float vert = CnInputManager.GetAxis(InputHelper.MOVE_JOYSTICK_VERTICAL);


        mRigidbody2d.velocity = new Vector2(horiz * speed, vert * speed);

        lookVector = new Vector3(horiz, vert, 0.0f);

        Vector3 diff = lookVector - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z );
        

        //animation handling
        if (mRigidbody2d.velocity.x > 0 )
            mAnimationController.PlayWalkAnimation(mRigidbody2d.velocity.x);
        else
        if ( mRigidbody2d.velocity.y > 0)
            mAnimationController.PlayWalkAnimation(mRigidbody2d.velocity.y);
        else
            mAnimationController.PlayWalkAnimation(0.0f);
    
    }


 

    private void InitVariables()
    {

        if (mAnimationController == null)
            mAnimationController = this.GetComponent<PlayerAnimationController>();
        if (mRigidbody2d == null)
            mRigidbody2d = this.GetComponent<Rigidbody2D>();
        if (mTransform == null)
            mTransform = this.GetComponent<Transform>();
    }





}
