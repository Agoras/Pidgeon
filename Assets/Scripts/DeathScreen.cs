using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour {

	private SceneFlowManager sceneManager;

	void Awake ()
	{
		sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<SceneFlowManager>();
	}

	void Update () 
	{
		if (Input.anyKey)
		{
			sceneManager.ReloadLevel ("bootstrap", true);
		}
	}
}
