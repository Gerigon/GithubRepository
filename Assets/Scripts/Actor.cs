using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour {

    protected bool facingRight = true;
    protected bool grounded = true;

    protected int health;
    public int moveSpeed;
    public int jumpForce;
    public float direction;

    public Animator myAnimator;
    public Rigidbody2D myRigidBody;
    public CharacterController myCharacterController;
    // Use this for initialization

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCharacterController = new CharacterController(this);
        /* not sure about this part, kan beter :P */
        myCharacterController.speed = moveSpeed;
        myCharacterController.jumpForce = jumpForce;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        GroundCheck();
        myCharacterController.Move(direction);
        //animator kan nog in andere class worden gezet mogt er veel meer functies bijkomen
        myAnimator.SetFloat("Speed", Mathf.Abs(direction));
    }
    void GroundCheck()
    {
        grounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down, 1 << LayerMask.NameToLayer("Ground"));
    }
}
