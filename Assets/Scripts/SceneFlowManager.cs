using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFlowManager : MonoBehaviour {




	public void ReloadLevel ()
	{
		SceneManager.LoadScene ("Gameplay", LoadSceneMode.Single);
	}

}
