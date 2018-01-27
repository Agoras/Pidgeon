using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegmentBehaivor : MonoBehaviour {

	private GroundSpawning groundSpawner;
	public float moveSpeed;
	private Vector2 currentPos;

	public List<Transform> targetPositions = new List<Transform>();
	public List<GameObject> MailBoxes = new List<GameObject>();


	void Awake () 
	{
		for(int i = 0; i < targetPositions.Count; i++)
		{
			GameObject newMailbox = Instantiate (MailBoxes [0], Vector2.zero, Quaternion.identity) as GameObject;
			Transform boxTransform = newMailbox.transform;

			boxTransform.SetParent (this.transform);
			boxTransform.localPosition = targetPositions [i].localPosition;
			boxTransform.localScale = new Vector2 ( boxTransform.localScale.x * this.transform.localScale.x, boxTransform.localScale.y * this.transform.localScale.y);
		}


		groundSpawner = GameObject.FindGameObjectWithTag ("GroundSpawner").GetComponent<GroundSpawning>();
		moveSpeed = groundSpawner.groundMoveSpeedMin;
	}
	
	void Update () 
	{
		currentPos = transform.localPosition;
		float newPos = currentPos.x - moveSpeed; 

		transform.localPosition = new Vector2 (newPos, currentPos.y);

	}
}
