using UnityEngine;
using CnControls;
using System;
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

	[Header("Movement")]
	[Range(5.0f,100.0f)]
	public float speed = 7.5f;

	[HideInInspector]
	private AuraManager auraManager;

    [SerializeField]
    private Inventory playerInventory; 

	public Vector3 lookVector;
	public event Action OnPlayerDeath;

    void Awake()
    {
        InitVariables();
    }

    void Update()
    {
        float horiz, vert;
#if UNITY_ANDROID
        horiz = CnInputManager.GetAxis(InputHelper.MOVE_JOYSTICK_HORIZONTAL);
        vert = CnInputManager.GetAxis(InputHelper.MOVE_JOYSTICK_VERTICAL);
#else
        horiz = CnInputManager.GetAxis(InputHelper.MOVE_HORIZONTAL);
        vert = CnInputManager.GetAxis(InputHelper.MOVE_VERTICAL);

#endif
        mRigidbody2d.velocity = new Vector2(horiz * speed * auraManager.modifiedSpeed,
                                            vert * speed * auraManager.modifiedSpeed);

        mAnimationController.PlayWalkAnimation(horiz, vert);
        //lookVector = new Vector3(horiz, vert, 0.0f);

        //Vector3 diff = lookVector - transform.position;
        //diff.Normalize();
        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rot_z );

        //animation handling
        //if (mRigidbody2d.velocity.x > 0 )
        //    mAnimationController.PlayWalkAnimation(mRigidbody2d.velocity.x);
        //else
        //if ( mRigidbody2d.velocity.y > 0)
        //    mAnimationController.PlayWalkAnimation(mRigidbody2d.velocity.y);
        //else
        //    mAnimationController.PlayWalkAnimation(0.0f);
    }

    private void InitVariables()
    {
        if (mAnimationController == null)
            mAnimationController = this.GetComponent<PlayerAnimationController>();
        if (mRigidbody2d == null)
            mRigidbody2d = this.GetComponent<Rigidbody2D>();
        if (mTransform == null)
            mTransform = this.GetComponent<Transform>();
		auraManager = transform.GetComponentInChildren<AuraManager>();
	}

	void OnCollisionEnter2D (Collision2D collider) {
		if (collider.gameObject.tag == "Enemy") {
			// LET ENEMY FEAST ON PLAYER'S CORPSE
			speed /= 10;
			OnPlayerDeath();
		}
	}
}
