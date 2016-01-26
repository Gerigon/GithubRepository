using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

    private CircleCollider2D hitBox;
    private List<Collider2D> enemiesHit = new List<Collider2D>();
	public int dmg = 20;
    public float attackDuration = 2; //how long the hitbox lasts in seconds.

	// Use this for initialization
	void Start () {
        hitBox = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator DealDmg(GameObject obj)
    {
        obj.SendMessage("GetDamage", dmg, SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(attackDuration);
        //hitBox.enabled = false;
		//enemiesHit.Clear();
        Destroy(transform.parent.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("entered trigger");
		// If it hits an enemy...
		if (col.tag == "enemy")
		{
			Debug.Log("hit enemy");
			if (!enemiesHit.Contains(col))
			{
				enemiesHit.Add(col);
				StartCoroutine(DealDmg(col.gameObject));
			}
		}
    }
}
