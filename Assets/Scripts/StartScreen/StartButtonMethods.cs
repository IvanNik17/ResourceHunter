using UnityEngine;
using System.Collections;

public class StartButtonMethods : MonoBehaviour {

	public void switchStartToScores(){
		Application.LoadLevel ("Scores");
	}

	public void switchStartToSelection(){
		Application.LoadLevel ("SelectScreen");
	}

	public void ExitStartApplication(){
		Application.Quit();
	}
}
