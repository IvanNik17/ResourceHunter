using UnityEngine;
using System.Collections;

public class GoToScores : MonoBehaviour {

	// Update is called once per frame
	public void switchToScores(){
		//Zero out counters and load score scene
		WinningCondition.woodCellCounter = 0;
		WinningCondition.solarCellCounter = 0;
		WinningCondition.waterCellCounter = 0;
		Application.LoadLevel ("Scores");
	}
}
