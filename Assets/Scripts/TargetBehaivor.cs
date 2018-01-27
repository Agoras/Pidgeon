using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaivor : MonoBehaviour {

	private GameManagerThing gm;

	public float zone0Distance;
	public int zone0Points = 3;
	public float zone1Distance;
	public int zone1Points = 1;

	private SpriteRenderer spriteRenderer;

	void Awake () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerThing>();
		spriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
	}

	public void CalculatePoints (float distance)
	{
		if (distance <= zone0Distance) 
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
	}
}


