using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFlowManager : MonoBehaviour {

	private bool isFE = true;
	public int finalScore;

	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	void Update ()
	{
		if(Input.anyKey && isFE == true)
		{
			ReloadLevel ("Gameplay", false);
		}

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	public void ReloadLevel (string level, bool feState)
	{
		SceneManager.LoadScene (level, LoadSceneMode.Single);
		isFE = feState;
	}

}
