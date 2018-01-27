using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegmentBehaivor : MonoBehaviour {

	private GroundSpawning groundSpawner;
	public float moveSpeed;
	private Vector2 currentPos;

	void Awake () 
	{
		groundSpawner = GameObject.FindGameObjectWithTag ("GroundSpawner").GetComponent<GroundSpawning>();
		moveSpeed = groundSpawner.groundMoveSpeed;
	}
	
	void Update () 
	{
		currentPos = transform.localPosition;
		float newPos = currentPos.x - moveSpeed; 

		transform.localPosition = new Vector2 (newPos, currentPos.y);

	}
}
