using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonFuntionsSelect : MonoBehaviour {

	private GameObject terrainSliderObj;
	private GameObject woodSliderObj;
	private GameObject waterSliderObj;
	private GameObject solarSliderObj;

	private GameObject terrainTextObj;
	private GameObject woodTextObj;
	private GameObject waterTextObj;
	private GameObject solarTextObj;


	void Start(){
		terrainSliderObj = GameObject.Find ("SliderTerrain");
		woodSliderObj = GameObject.Find ("SliderWood");
		waterSliderObj = GameObject.Find ("SliderWater");
		solarSliderObj = GameObject.Find ("SliderSolar");

		terrainTextObj = GameObject.Find ("TerrainSizeText");
		woodTextObj = GameObject.Find ("WoodCellsText");
		waterTextObj = GameObject.Find ("WaterCellsText");
		solarTextObj = GameObject.Find ("SolarCellsText");

		terrainTextObj.GetComponent<Text> ().text = terrainSliderObj.GetComponent<Slider> ().value.ToString();
		woodTextObj.GetComponent<Text> ().text = ((int)woodSliderObj.GetComponent<Slider> ().value).ToString();
		waterTextObj.GetComponent<Text> ().text = ((int)waterSliderObj.GetComponent<Slider> ().value).ToString();
		solarTextObj.GetComponent<Text> ().text = ((int)solarSliderObj.GetComponent<Slider> ().value).ToString();

	}

	public void switchSelectToStart(){
		Application.LoadLevel ("StartScreen");
	}
	
	public void switchSelectToGame(){
		Application.LoadLevel ("CarAndroid2");
		CalculateGround.groundSize = new Vector2 (terrainSliderObj.GetComponent<Slider> ().value, terrainSliderObj.GetComponent<Slider> ().value);
		WinningCondition.numberOfWoodCellsNeeded = (int)woodSliderObj.GetComponent<Slider> ().value;
		WinningCondition.numberOfWaterCellsNeeded = (int)waterSliderObj.GetComponent<Slider> ().value;
		WinningCondition.numberOfSolarCellsNeeded = (int)solarSliderObj.GetComponent<Slider> ().value;
	}
	
	public void refreshLabels(int whichLabel) {
		switch (whichLabel) {
		case 1: terrainTextObj.GetComponent<Text> ().text = terrainSliderObj.GetComponent<Slider> ().value.ToString(); break;
		case 2: woodTextObj.GetComponent<Text> ().text = ((int)woodSliderObj.GetComponent<Slider> ().value).ToString(); break;
		case 3: waterTextObj.GetComponent<Text> ().text = ((int)waterSliderObj.GetComponent<Slider> ().value).ToString(); break;
		case 4: solarTextObj.GetComponent<Text> ().text = ((int)solarSliderObj.GetComponent<Slider> ().value).ToString(); break;
		default:
			break;
		}
	}
}
