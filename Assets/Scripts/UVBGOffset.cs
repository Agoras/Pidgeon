using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVBGOffset : MonoBehaviour {


	private MeshRenderer meshRend;
	public float moveSpeed;

	void Awake () 
	{
		meshRend = transform.GetComponent<MeshRenderer> ();
	}
	
	void Update () 
	{
		float deltaTime = Time.deltaTime;

		meshRend.material.mainTextureOffset += new Vector2( -moveSpeed * deltaTime, 0.0f ) ;

	}
}
