using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSegmentBehaivor : MonoBehaviour {

	private GameManagerThing gm;

	private GroundSpawning groundSpawner;
	public float moveSpeed;
	private Vector2 currentPos;
	public Transform targetParent;

	public List<Transform> targetPositions = new List<Transform>();
	public List<GameObject> MailBoxes = new List<GameObject>();


	void Awake () 
	{

		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerThing>();


		for(int i = 0; i < targetPositions.Count; i++)
		{

			int randPickUpChance = Random.Range (0, 100);


			GameObject newMailbox = Instantiate (MailBoxes [0], Vector2.zero, Quaternion.identity) as GameObject;
			Transform boxTransform = newMailbox.transform;

			if(randPickUpChance >= 65 && gm.collectables <=2)
			{
				TargetBehaivor targetBehaivor = newMailbox.GetComponent<TargetBehaivor> ();
				targetBehaivor.isPickUp = true;
				boxTransform.GetComponentInChildren<Animator> ().SetTrigger ("isPickUp");

			}
			boxTransform.SetParent (targetParent);
			boxTransform.localPosition = targetPositions [i].localPosition;
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
