using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteGenerationManager : MonoBehaviour {

	public GameObject kiteObject;

	private GroundSpawning groundSpawner;

	public float minSpawnHeight;
	public float maxSpawnHeight;

	public float minGenTime;
	public float maxGenTime;

	private float randTime;

	private float genTimer;

	void Awake () 
	{
		randTime = Random.Range ( minGenTime, maxGenTime );
		groundSpawner = GameObject.FindGameObjectWithTag ("GroundSpawner").GetComponent<GroundSpawning>();
	}
	
	void Update () 
	{

		float deltaTime = Time.deltaTime;
		genTimer += deltaTime;

		if ( genTimer >= randTime )
		{
			float spawnPosY = Random.Range ( minSpawnHeight, maxSpawnHeight );

			GameObject newKite = Instantiate ( kiteObject, new Vector2( transform.localPosition.x , spawnPosY ), Quaternion.identity ) as GameObject;

			Transform parentObject =  groundSpawner.groundObjects[ groundSpawner.groundObjects.Count -1].transform ;

			newKite.transform.SetParent (parentObject);

			randTime = Random.Range ( minGenTime, maxGenTime );
			genTimer = 0.0f;
		}
	}

	public void GotHit()
	{
		groundSpawner.currentGroundSpeed = groundSpawner.currentGroundSpeed / 2;
	}
}
