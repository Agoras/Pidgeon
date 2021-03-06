﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaivor : MonoBehaviour {

	private GameManagerThing gm;

	public bool isPickUp = false;

	public float zone0Distance;
	public int zone0Points = 3;
	public float zone1Distance;
	public int zone1Points = 1;

	private bool isHit = false;

	private SpriteRenderer spriteRenderer;

	void Awake () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerThing>();
		spriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
	}


	public void CalculatePoints (string hitType)
	{
		if (isHit == false && isPickUp == false) 
		{
			if (hitType == "direct") 
			{
				gm.pigeonRep += zone0Points;
				gm.UpdatePigonRep ();
				spriteRenderer.material.color = Color.green;
			} 
			else 
			{
				gm.pigeonRep += zone1Points;
				gm.UpdatePigonRep ();
				spriteRenderer.material.color = Color.blue;
			}
			isHit = true;
		}
			
	}
}


