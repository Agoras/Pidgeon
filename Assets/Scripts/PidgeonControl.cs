using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonControl : MonoBehaviour {

	private Rigidbody2D pidgeon_rb;

	private float gravityForce;
	public  float gravityForceMax;
	public  float gravityForceMin;
	public  float gravityLerpSpeed;

	public GameObject basicMessageProjectile;
	public Transform projectileSpawnPoint;

	public float flapForce;
	public  float flapDelay          = 0.3f;
	private float flapDelayTimer     = 0.3f;
	private float deltaTime          = 0.0f;

	public  float dropDelay          = 0.3f;
	private float dropDelayTimer     = 0.3f;


	void Awake () 
	{
		pidgeon_rb= this.transform.GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
		deltaTime = Time.deltaTime;
		flapDelayTimer += deltaTime;
		dropDelayTimer += deltaTime;

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
				flapDelayTimer = 0.0f;
			}
		}

		if (Input.GetButtonDown ("Fire1")) 
		{
			if (dropDelayTimer > dropDelay) 
			{
				GameObject newProjectile = Instantiate (basicMessageProjectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
				dropDelayTimer = 0.0f;
			}
		}
			

	}
}
