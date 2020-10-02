using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ManagerScore : MonoBehaviour {

	private GameObject scoreTextObj;
	// Use this for initialization
	void Start () {
		scoreTextObj = GameObject.Find("Score");
		if (WinningCondition.textTime != null) {
			SetHighscore (WinningCondition.textTime);
		} else {
			PlayerPrefs.GetString ("highscore");
		}

		if (PlayerPrefs.HasKey ("highscore")) {
			scoreTextObj.GetComponent<Text>().text = PlayerPrefs.GetString ("highscore");
		}
		else {
			scoreTextObj.GetComponent<Text>().text = "";
		}
	}


	void SetHighscore(string myHighScore)
	{
		
		string key = "highscore";
		string highscore = myHighScore;

		
		if (PlayerPrefs.HasKey (key)) {
			DateTime t1 = DateTime.Parse(highscore);
			DateTime t2 = DateTime.Parse(PlayerPrefs.GetString (key));

			if (t2 >= t1){
				PlayerPrefs.SetString (key, highscore);
			}
		} 
		else {
			PlayerPrefs.SetString (key, highscore);
		}
		
		PlayerPrefs.Save();
		
	}

	public void switchScoresToMain(){
		WinningCondition.woodCellCounter = 0;
		WinningCondition.solarCellCounter = 0;
		WinningCondition.waterCellCounter = 0;
		WinningCondition.textTime = "";
		WinningCondition.numberOfWoodCellsNeeded = 0;
		WinningCondition.numberOfWaterCellsNeeded = 0;
		WinningCondition.numberOfSolarCellsNeeded = 0;
		Application.LoadLevel ("StartScreen");
	}
}
