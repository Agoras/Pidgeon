using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerThing : MonoBehaviour {

	public SceneFlowManager sceneFlow;

	public int pigeonRep;
	public Text repPoints;

	public float gameLength;
	private float gameTimer;
	public Text timerText;


	void Awake()
	{
		timerText.text = gameLength.ToString ();
		gameTimer = gameLength;

		sceneFlow = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<SceneFlowManager>();
	}

	void Update ()
	{
		float deltaTime = Time.deltaTime;

		gameTimer =  gameTimer - deltaTime ;
		int newTime = Mathf.FloorToInt ( gameTimer );

		timerText.text = newTime.ToString ();

		if(gameTimer <= 0.0f)
		{
			sceneFlow.ReloadLevel ("EndScreen", false);
		}
	}

	public void UpdatePigonRep ()
	{
		repPoints.text = pigeonRep.ToString();
	}
}
