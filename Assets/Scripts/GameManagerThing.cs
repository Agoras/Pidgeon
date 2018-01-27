using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerThing : MonoBehaviour {

	public int pigeonRep;
	public Text repPoints;

	public void UpdatePigonRep ()
	{
		repPoints.text = pigeonRep.ToString();
	}
}
