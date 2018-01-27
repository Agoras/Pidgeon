using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegmentBehaivor : MonoBehaviour {

	private GroundSpawning groundSpawner;

	public float moveSpeed;
	//public float spawnPosition;
	//public float destroyPosition;
	//private float trueDestroyPosition;

	private Vector2 currentPos;



	void Awake () 
	{
		groundSpawner = GameObject.FindGameObjectWithTag ("GroundSpawner").GetComponent<GroundSpawning>();
		moveSpeed = groundSpawner.groundMoveSpeed;
		//trueDestroyPosition = transform.localPosition.x + destroyPosition;
	}
	
	void Update () 
	{
		currentPos = transform.localPosition;
		float newPos = currentPos.x - moveSpeed; 

		transform.localPosition = new Vector2 (newPos, currentPos.y);
		/*
		if (currentPos.x <= trueDestroyPosition) 
		{
			groundSpawner.RemoveFirstObject ();
			Destroy (this.gameObject);
		}
		*/
	}
}
