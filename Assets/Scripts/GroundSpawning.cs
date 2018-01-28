using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawning : MonoBehaviour {

	public float groundMoveSpeedMin;
	public float groundMoveSpeedMax;
	public float currentGroundSpeed;

	public float groundSpeedAcceleration;

	private float midScreenPos = 0;
	public float spawnPos = 15;
	public float destroyOffset;
	private float destroyPos;
	public float groundHeight;

	public List<GameObject> groundSegPrefab = new List<GameObject>();
	public Transform groundParent;


	public GameObject StartGroundObject;
	public List<GameObject> groundObjects = new List<GameObject>();


	void Awake () 
	{
		destroyPos = midScreenPos - destroyOffset;
		groundObjects.Add (StartGroundObject);

		currentGroundSpeed = groundMoveSpeedMin;
	}
	
	void Update () 
	{
		float deltaTime = Time.deltaTime;


		if(groundObjects[0].transform.localPosition.x < midScreenPos &&  groundObjects.Count < 2)
		{
			SpawnObject ();
		}
		if(groundObjects[0].transform.localPosition.x < destroyPos)
		{
			RemoveFirstObject ();
		}



		// speed up level
		currentGroundSpeed = Mathf.Lerp (currentGroundSpeed, groundMoveSpeedMax, groundSpeedAcceleration * deltaTime);

		UpdateGroundSpeed(currentGroundSpeed);
	}

	public void RemoveFirstObject ()
	{
		GameObject destroyObject = groundObjects [0];
		groundObjects.RemoveAt (0);
		Destroy (destroyObject);
	}

	public void SpawnObject ()
	{

		int randomGroundSeg = Random.Range (0, groundSegPrefab.Count);

		GameObject newGO = Instantiate(groundSegPrefab[randomGroundSeg], new Vector2( midScreenPos + spawnPos, groundHeight ) , Quaternion.identity) as GameObject;
		newGO.transform.SetParent (groundParent);
		//newGO.transform.localPosition = new Vector2 (midScreenPos + spawnPos, groundHeight);
		groundObjects.Add (newGO);

	}

	void UpdateGroundSpeed (float newSpeed)
	{
		for (int i = 0; i < groundObjects.Count; i++) 
		{
			groundObjects [i].GetComponent<GroundSegmentBehaivor> ().moveSpeed = newSpeed;
		}
	}

}
