using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointsScreen : MonoBehaviour {

	private SceneFlowManager sceneManager;
	public Text pointsText; 

	void Awake ()
	{
		sceneManager = GameObject.FindGameObjectWithTag ("SceneManager").GetComponent<SceneFlowManager>();
		pointsText.text = sceneManager.finalScore.ToString();
	}
	
	void Update () 
	{
		if (Input.anyKey)
		{
            StartCoroutine(MoveAlong());
		}
	}

    IEnumerator MoveAlong()
    {
        yield return new WaitForSeconds(2.0f);
        sceneManager.ReloadLevel("bootstrap", true);
    }
}
