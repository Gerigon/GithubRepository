using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePlayer : MonoBehaviour {

	public bool facingRight = true;
	public bool grounded = true;
	public bool shouldJump;
	public int health = 100;
	public int moveSpeed = 8;
	public int jumpForce = 8;
	public float direction;

	public GameObject defaultWeapon;
	public GameObject currentWeapon;
	public List<GameObject> weapons = new List<GameObject>();
	public Transform weaponPos;
	public Transform groundCheck;
	public Animator animator;
	public Rigidbody2D rigidBody;
	public SpriteRenderer healthBar;

	private int initialHealth;
	private int currentWeaponNr;
	private Collider2D defaultWeaponCollider;

	// Use this for initialization
	void Start() {
		//currentWeapon = weapons[0];
		defaultWeaponCollider = defaultWeapon.GetComponent<Collider2D>();
		initialHealth = health;
		Jump();
	}

	// Update is called once per frame
	void Update() {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		GetInput();
		CheckDirection();
	}

	void FixedUpdate()
	{
		Walk();
		Jump();
	}

	void GetInput()
	{
		direction = Input.GetAxis("Horizontal");
		if (Input.GetButtonDown("Jump") && grounded)
			shouldJump = true;
		if (Input.GetKeyDown(KeyCode.F))
			Attack();
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			currentWeaponNr = 1; SelectWeapon();}
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			currentWeaponNr = 2; SelectWeapon();}
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			currentWeaponNr = 3; SelectWeapon();}
	}
	void CheckDirection()
	{
		if (direction > 0 && !facingRight)
			Flip();
		else if (direction < 0 && facingRight)
			Flip();
	}
	void SelectWeapon() {
		currentWeapon = weapons[currentWeaponNr];
		//HIGHLIGHT THE CORROSPONDING WEAPON ICON
	}

	void Walk()
	{
		//tell the animator to play animation, speed is 1 or -1;
		animator.SetFloat("Speed", Mathf.Abs(direction));
		rigidBody.velocity = new Vector2(moveSpeed * direction, rigidBody.velocity.y);
	}
	void Jump()
	{
		if (shouldJump) {

			rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			shouldJump = false;
		}
	}

	void Die()
	{

	}
    void Attack()
    {
		defaultWeaponCollider.enabled = false;
        Instantiate(currentWeapon, weaponPos.position, Quaternion.identity);
    }
	void GetDamage(int dmgValue)
	{
		health -= dmgValue;
		UpdateHealthBar();
		if (health <= 0)
			Die();
	}

	void Flip()
	{
		facingRight = !facingRight;
		
		Vector3 tmpScale = transform.localScale;
		tmpScale.x *= -1;
		transform.localScale = tmpScale;
	}
	public void UpdateHealthBar()
	{
		//DONT REMOVE THE .0f OR IT WONT CALCULATE IN FLOAT BUT IN INT INSTEAD//
		healthBar.transform.localScale = new Vector3(1.0f / initialHealth * health, 1, 1);
	}
}
