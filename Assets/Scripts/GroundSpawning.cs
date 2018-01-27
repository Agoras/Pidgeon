using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawning : MonoBehaviour {

	public float groundMoveSpeed;
	private float midScreenPos = 0;
	public float spawnPos = 15;
	public float destroyOffset;
	private float destroyPos;
	public float groundHeight;

	public GameObject groundObject;
	public Transform groundParent;


	public GameObject StartGroundObject;
	private List<GameObject> groundObjects = new List<GameObject>();


	void Awake () 
	{
		destroyPos = midScreenPos - destroyOffset;
		groundObjects.Add (StartGroundObject);
	}
	
	void Update () 
	{
		if(groundObjects[0].transform.localPosition.x < midScreenPos &&  groundObjects.Count < 2)
		{
			SpawnObject ();
		}
		if(groundObjects[0].transform.localPosition.x < destroyPos)
		{
			RemoveFirstObject ();
		}
		// FIX THIS DUMMY
		UpdateGroundSpeed(groundMoveSpeed);
	}

	public void RemoveFirstObject ()
	{
		GameObject destroyObject = groundObjects [0];
		groundObjects.RemoveAt (0);
		Destroy (destroyObject);
	}

	public void SpawnObject ()
	{
		
		GameObject newGO = Instantiate(groundObject, new Vector2( midScreenPos + spawnPos, groundHeight ) , Quaternion.identity) as GameObject;
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
