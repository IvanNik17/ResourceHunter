using UnityEngine;
using System.Collections;

public class ReturnToMain : MonoBehaviour {

	public void switchToStartScreen(){
		//Zero out counters and time and load score scene
		WinningCondition.woodCellCounter = 0;
		WinningCondition.solarCellCounter = 0;
		WinningCondition.waterCellCounter = 0;
		WinningCondition.textTime = "";
		Application.LoadLevel ("StartScreen");
	}
}
