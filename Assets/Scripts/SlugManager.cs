using UnityEngine;
using System.Collections;

public class SlugManager : MonoBehaviour
{
    public bool facingRight;
    public bool grounded = true;
    public bool shouldAttack;

	public int dmg = 20; // how much dmg the enemy deals when attackig the player.
	public int health = 100;

	public int moveSpeed = 8;
	public int jumpForce = 8;

	public int agroRange = 5; // if the player is within this range the enemy will start moving towards him (unity units).
	public int attackRange = 1; // the enemy must be this close before it will attack (unity units).
	public float minDistance = 3; // minimum distance the enemy keeps from the player (unity units).

	public int attackDelay = 2; // time until the enemy can attack again (seconds).
	public float attackDuration = 1; //how long the hitbox lasts (seconds).
    private float nextAttack = 0.0f;
	

	public Transform player;
    public Transform groundCheck;
    public Animator animator;
    public Rigidbody2D rigidBody;

    public  SpriteRenderer healthBar;
	private CircleCollider2D dmgBox;
	private int initialHealth;

    // Use this for initialization
    void Start()
    {
        initialHealth = health;
		dmgBox = gameObject.GetComponent<CircleCollider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        DecideAction();
        CheckDirection();
    }

    void FixedUpdate()
    {
        Walk();
        Attack();
    }

    void DecideAction()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange && grounded && Time.time > nextAttack)
            shouldAttack = true;
    }
    void CheckDirection()
    {
        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();
    }
    void Walk()
    {
        Vector2 distance = player.position - transform.position;
        if (Mathf.Abs(distance.x) < agroRange && Mathf.Abs(distance.x) > minDistance) {
            //Make the enmey only move toward the players x axis//
            Vector2 tmpPos = player.position;
            tmpPos.y = transform.position.y;
            //animator.SetFloat("Speed", Mathf.Abs(direction));
            transform.position = Vector2.MoveTowards(transform.position, tmpPos, moveSpeed * Time.deltaTime);
        }
    }

	void Die()
	{
		Destroy(gameObject);
	}

    void Attack()
    {
        if(shouldAttack)
        {
            nextAttack = Time.time + attackDelay;
			dmgBox.enabled = true;
			animator.SetTrigger("Attack");
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            shouldAttack = false;
        }
    }

    void GetDamage(int dmgValue)
    {
        health-= dmgValue;
        UpdateHealthBar();
		if (health <= 0)
			Die();
	}

	IEnumerator DealDmg(GameObject obj)
	{
		obj.SendMessage("GetDamage", dmg, SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(attackDuration);
		dmgBox.enabled = false;
		//enemiesHit.Clear();
	}

	public void UpdateHealthBar()
    {   
        //DONT REMOVE THE .0f OR IT WONT CALCULATE IN FLOAT BUT INT INSTEAD//
        healthBar.transform.localScale = new Vector3(1.0f/initialHealth * health, 1, 1);
    }

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 tmpScale = transform.localScale;
		tmpScale.x *= -1;
		transform.localScale = tmpScale;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("hit something");
		// If it hits the player
		if (col.tag == "Player")
		{
			Debug.Log("hit player");
			StartCoroutine(DealDmg(col.gameObject));
		}
	}
}
