using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//Select winning conditions
public class WinningCondition : MonoBehaviour {

	//static variable which are set in the select screen and determine the needed resources
	public static int numberOfWoodCellsNeeded;
	public static int numberOfWaterCellsNeeded;
	public static int numberOfSolarCellsNeeded;

	//static varibles that count the collected variables
	public static int woodCellCounter = 0;
	public static int waterCellCounter = 0;
	public static int solarCellCounter = 0;

	//timer variables - the string for showing the time elapsed and the starting time
	public static string textTime;
	private float startTimer;

	//stop the timer
	private bool stopTimer = false;
	//objects for the two canvases - the overlay one and the one for the winning menu
	private GameObject winingCanvas;
	private GameObject overlayCanvas;

	//object for displaying the wood, solar and water cells collected and how much more are needed.
	private GameObject woodCellsTxt;
	private GameObject solarCellsTxt;
	private GameObject waterCellsTxt;

	void Awake() {

		//initialize everything on awake
		startTimer = Time.time;
		winingCanvas = GameObject.FindGameObjectWithTag("MenuWinning");
		overlayCanvas = GameObject.FindGameObjectWithTag("OverlayCanvas");

		woodCellsTxt = GameObject.Find ("WoodCells");
		solarCellsTxt = GameObject.Find ("SolarCells");
		waterCellsTxt = GameObject.Find ("WaterCells");
	}
	
	// Update is called once per frame
	void Update () {
		//if the condition for winning is met - switch canvases
		if (!stopTimer) {
			winingCanvas.GetComponent<Canvas>().enabled = false;
			overlayCanvas.GetComponent<Canvas>().enabled = true;
		}
		else {
			winingCanvas.GetComponent<Canvas>().enabled = true;
			overlayCanvas.GetComponent<Canvas>().enabled = false;
		}
		//timer method for a clock with minutes, seconds and fractions
		ClockTimer ();
		//condition for winning
		if (woodCellCounter >= numberOfWoodCellsNeeded && waterCellCounter >= numberOfWaterCellsNeeded && solarCellCounter >= numberOfSolarCellsNeeded) {
			stopTimer = true;

		}
		//show collected and how much are needed
		showCollected ();

	}
	void ClockTimer() {

		if (!stopTimer) {
			float currTime = Time.time - startTimer;
			
			float minutes = currTime / 60;
			float seconds = currTime % 60;
			float fraction = (currTime * 100) % 100;
			textTime = string.Format ("{0:00}:{1:00}", minutes, seconds);
			GameObject timeLabel = GameObject.Find("Time");
			timeLabel.GetComponent<Text>().text = textTime;
		}

	}

	void showCollected() {
		woodCellsTxt.GetComponent<Text> ().text = "Wood Cells: " + woodCellCounter + "/" + numberOfWoodCellsNeeded;
		solarCellsTxt.GetComponent<Text> ().text = "Solar Cells: " + solarCellCounter + "/" + numberOfSolarCellsNeeded;
		waterCellsTxt.GetComponent<Text> ().text = "Water Cells: " + waterCellCounter + "/" + numberOfWaterCellsNeeded;

		//change color do indicate that the needed number is met.
		if (woodCellCounter >= numberOfWoodCellsNeeded) {
			woodCellsTxt.GetComponent<Text> ().color = Color.green;
		}
		if (solarCellCounter >= numberOfSolarCellsNeeded) {
			solarCellsTxt.GetComponent<Text> ().color = Color.green;
		}
		if (waterCellCounter >= numberOfWaterCellsNeeded) {
			waterCellsTxt.GetComponent<Text> ().color = Color.green;
		}

	}

}
