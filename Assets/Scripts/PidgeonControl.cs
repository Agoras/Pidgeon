using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonControl : MonoBehaviour {

	private Rigidbody2D pidgeon_rb;

	private float gravityForce;
	public  float gravityForceMax;
	public  float gravityForceMin;
	public  float gravityLerpSpeed;


	public float flapForce;

	public  float flapDelay          = 0.3f;
	private float flapDelayTimer     = 0.3f;

	private float deltaTime          = 0.0f;
	void Awake () 
	{
		pidgeon_rb= this.transform.GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
		deltaTime = Time.deltaTime;
		flapDelayTimer += deltaTime;

		if (pidgeon_rb.velocity.y > 0.0f) 
		{
			gravityForce = Mathf.Lerp(gravityForce, gravityForceMax, gravityLerpSpeed * deltaTime);
		} 
		else 
		{
			gravityForce = Mathf.Lerp(gravityForce, gravityForceMin, gravityLerpSpeed * deltaTime);

		}

		pidgeon_rb.AddForce (-transform.up * gravityForce, ForceMode2D.Force);



		if (Input.GetButtonDown ("Flap")) 
		{
			if (flapDelayTimer > flapDelay) 
			{
				pidgeon_rb.AddForce (transform.up * flapForce, ForceMode2D.Impulse);

				Debug.Log ("flapping");
				flapDelayTimer = 0.0f;
			}
		}

	}
}
