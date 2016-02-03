using UnityEngine;
using System.Collections;

public class Player : Actor {



	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
	}

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
        GetInput();
	}

    void GetInput()
    {
        direction = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && grounded)
        {
            myCharacterController.Jump();
            //shouldJump = true;
        }
    }
}
