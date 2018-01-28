using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBehaivor : MonoBehaviour {

	private GameManagerThing gm;
    private EnvironmentAudioTerminal eat;
    public int missDeduction;

	private Rigidbody2D projectile_rb;
	public float dropForceForward;

	public Transform currentTarget;

	void Awake () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerThing>();
		//anim = transform.GetComponentInChildren<Animator> ();
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

		if( collision2D.transform.tag == "BoxHead")
		{
            eat.PlaySuccessSound();
            currentTarget.GetComponent<TargetBehaivor> ().CalculatePoints ("direct");
			currentTarget.GetComponentInChildren<Animator>().SetBool ("takeMessage", true);
			Destroy (this.gameObject);
		}
		else if(currentTarget == null)
		{
            eat.PlayMissedSound();
            gm.pigeonRep -= missDeduction;
			gm.UpdatePigonRep ();
			this.transform.SetParent (collision2D.transform.parent);
			projectile_rb.Sleep();
		}
		else 
		{
            eat.PlayMissedSound();
            float dist = Vector2.Distance (currentTarget.position, this.transform.position);
			currentTarget.GetComponent<TargetBehaivor> ().CalculatePoints ("nearHit");
			this.transform.SetParent (collision2D.transform.parent);
			projectile_rb.Sleep();
		}
	}
}
