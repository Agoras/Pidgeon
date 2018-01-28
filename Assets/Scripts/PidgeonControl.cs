﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonControl : MonoBehaviour {

    public AudioSource wingFlapSource;
    public AudioSource playerSfxSource;
    public AudioClip playerDropSfx;
    public AudioClip playerDeadSfx;

	public GameObject pigeonSprite;

	private GroundSpawning groundSpawner;
    private SceneFlowManager sceneManager;
	private Rigidbody2D pidgeon_rb;

	private float gravityForce;
	public  float gravityForceMax;
	public  float gravityForceMin;
	public  float gravityLerpSpeed;

	public List<GameObject> Projectiles = new List<GameObject> ();
	public List<GameObject> ProjectilesLoaded = new List<GameObject> ();

	public Transform projectileSpawnPoint;

	public  float flapForce;
	public  float flapDelay          = 0.3f;
	private float flapDelayTimer     = 0.3f;
	private float deltaTime          = 0.0f;

	public  float dropDelay          = 0.3f;
	private float dropDelayTimer     = 0.3f;
	private int randomDropType       = 0;

	private Animator anim;
	public bool isFlapping;
	public float isFlappingTimer;
	public float isFlappingDuration;
	private float currentFlapSpeed;
	public  float idleFlapSpeed;
	public  float maxFlapSpeed;

	private bool hitKite  =  false;
	public float kitePunishDelay;
	private float kitePunishTimer  =  0.0f;


	//private bool isLoaded;


	void Awake () 
	{
		sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<SceneFlowManager>();
		pidgeon_rb   = this.transform.GetComponent<Rigidbody2D>();
		anim = this.transform.GetComponentInChildren<Animator>();
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



		if(hitKite == true)
		{
			kitePunishTimer += deltaTime;

			if(kitePunishTimer >= kitePunishDelay)
			{
				hitKite = false;
				kitePunishTimer = 0.0f;
			}
		}

		//Input
		if (hitKite == false) 
		{

			if (Input.GetButtonDown ("Flap")) 
			{
				if (flapDelayTimer > flapDelay) 
				{
					pidgeon_rb.AddForce (transform.up * flapForce, ForceMode2D.Impulse);
					flapDelayTimer = 0.0f;
				}
				isFlapping = true;
				isFlappingTimer = 0.0f;
			}
			// load up projectile



			if (Input.GetButtonDown ("Fire1")) 
			{
				if (dropDelayTimer > dropDelay) 
				{
					
					GameObject newProjectile = Instantiate (Projectiles[randomDropType], projectileSpawnPoint.position, Quaternion.identity) as GameObject;
					if(randomDropType == 1)
					{
						newProjectile.GetComponent<Rigidbody2D> ().AddTorque (-2.5f);
					}
					dropDelayTimer = 0.0f;

					playerSfxSource.PlayOneShot (playerDropSfx, 0.35f);

					randomDropType = Random.Range (0, Projectiles.Count);
					if (randomDropType == 0) 
					{
						ProjectilesLoaded [0].GetComponent<SpriteRenderer> ().enabled = true;
						ProjectilesLoaded [1].GetComponent<SpriteRenderer> ().enabled = false;
					} 
					else 
					{
						ProjectilesLoaded [1].GetComponent<SpriteRenderer> ().enabled = true;
						ProjectilesLoaded [0].GetComponent<SpriteRenderer> ().enabled = false;						
					}
				}
			}
		}

		if (isFlapping == false) 
		{
			anim.speed = idleFlapSpeed;
		} 
		else 
		{
			isFlappingTimer += deltaTime;
			anim.speed = maxFlapSpeed;
		}

		if(isFlappingTimer > isFlappingDuration)
		{
			isFlapping = false;
			isFlappingTimer = 0.0f;
		}



	}

	IEnumerator OnCollisionEnter2D (Collision2D collision2D)
	{
		if(collision2D.transform.tag == "death")
		{
            playerSfxSource.PlayOneShot(playerDeadSfx, 0.35f);
			pigeonSprite.SetActive (false);
            yield return new WaitForSeconds(1.0f);
            sceneManager.ReloadLevel ("DeathScreen", true);
		}
	}

	void OnTriggerEnter2D (Collider2D collider2D)
	{
		if (collider2D.tag == "AirDelay") 
		{
			GameObject.FindGameObjectWithTag ("KiteSpawner").GetComponent<KiteGenerationManager> ().GotHit ();
			hitKite = true;
		}
	}
}
