using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBehaivor : MonoBehaviour {

	private GameManagerThing gm;

	public int missDeduction;

	private Rigidbody2D projectile_rb;
	public float dropForceForward;

	public Transform currentTarget;

	void Awake () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerThing>();

		projectile_rb = this.transform.GetComponent<Rigidbody2D> ();
		projectile_rb.AddForce (Vector2.right * dropForceForward, ForceMode2D.Impulse);
	}

	void OnTriggerEnter2D (Collider2D collider2D)
	{
		if (collider2D.transform.tag == "Target") 
		{
			currentTarget = collider2D.transform;
		}
	}

	void OnTriggerExit2D (Collider2D collider2D)
	{
		if (collider2D.transform.tag == "Target") 
		{
			currentTarget = null;
		}
	}

	void OnCollisionEnter2D (Collision2D collision2D)
	{
		Debug.Log ("Bombed a mother fucker");

		// Get nearest Target and send distance

		if(currentTarget == null)
		{
			gm.pigeonRep -= missDeduction;
			gm.UpdatePigonRep ();
		}
		else 
		{
			float dist = Vector2.Distance (currentTarget.position, this.transform.position);
			currentTarget.GetComponent<TargetBehaivor> ().CalculatePoints (dist);
		}


		Destroy (this.gameObject);
	}
}
