using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//When car is in the unloading dock unload cells from it and increment counters.
public class UnloadCells : MonoBehaviour {
	public float hoverHeight = 3.5f;
	private CollisionWithPowerCell placeholderScript;
	public int numberOfWoodCellsNeeded = 20;
	public int numberOfWaterCellsNeeded = 20;
	public int numberOfSolarCellsNeeded = 5;
	

	// Use this for initialization
	void Start () {
		placeholderScript = GetComponent<CollisionWithPowerCell> ();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, hoverHeight)) {
			if (hit.transform.tag == "Dock") {

				for (int i = 0; i < placeholderScript.cargoSlots.GetLength(0); i++) {

					for (int j = 0; j < placeholderScript.cargoSlots.GetLength(1); j++) {
						switch (placeholderScript.cargoSlots[i,j]) {
						case 1: WinningCondition.woodCellCounter++; break;
						case 2: WinningCondition.solarCellCounter++; break;
						case 3: WinningCondition.waterCellCounter++; break;
						default:
							//Debug.Log("No such cell");
							break;
						}
						if (placeholderScript.cargoSlots[i,j] != 0) {
							placeholderScript.cargoSlots[i,j] = 0;
							Destroy(placeholderScript.cargoPlaceholders[i,j].transform.GetChild(0).gameObject);
						}
					}
				}
			}
		}

	}
}
