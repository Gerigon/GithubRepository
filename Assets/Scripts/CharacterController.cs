using UnityEngine;
using System.Collections;

public class CharacterController {

    private Actor _owner;
    private int _speed;
    private int _jumpForce;

	public CharacterController (Actor owner)
    {
        if (owner != null)
        {
            this._owner = owner;
        }
        else
        {
            Debug.LogError("No Actor Attached (Character Controller)");
        }
    }

    public int speed
    {
        get { return _speed;  }
        set { _speed = value; }
    }
    public int jumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    public void Move(float direction)
    {
        _owner.myRigidBody.velocity = new Vector2(_speed * direction, _owner.myRigidBody.velocity.y);
    }
    void Jump()
    {
        _owner.myRigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
