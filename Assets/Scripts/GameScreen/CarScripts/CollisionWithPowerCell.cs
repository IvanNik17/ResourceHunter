using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Check if car collided with solar or water cell
public class CollisionWithPowerCell : MonoBehaviour {

	//cargo slots of the car
	public int[,] cargoSlots;
	//placeholder objects which are spawned on each cargo slot to keep power cells
	public GameObject[,] cargoPlaceholders;
	//cargo plate holding all the slots
	public GameObject cargoPlate;
	//solar and wood power cell
	public GameObject powerCellPrefab;
	public GameObject SolarCellPrefab;
	// Use this for initialization
	void Awake () {
		//initialize matrix arrays
		cargoSlots = new int[3,4];
		cargoPlaceholders = new GameObject[3,4];
		//calculate half the wide and height of the plate and cell for positioning
		Vector3 halfWidthHeightPlate = new Vector3(cargoPlate.transform.GetComponent<Renderer>().bounds.size.x/2,0,-cargoPlate.transform.GetComponent<Renderer>().bounds.size.z/2);
		Vector3 halfWidthHeightCell = new Vector3(powerCellPrefab.transform.GetComponent<Renderer>().bounds.size.x/2,powerCellPrefab.transform.GetComponent<Renderer>().bounds.size.y/2,-powerCellPrefab.transform.GetComponent<Renderer>().bounds.size.z/2);

		//starting position of the first cell in the cargo
		Vector3 startingPos = cargoPlate.transform.position - halfWidthHeightPlate + halfWidthHeightCell;
		//fill cargoSlots zeros and instantiate empty game objects at specific positions in the cargoPlaceholder matrix array
		for (int i = 0; i < cargoSlots.GetLength(0); i++) {
			for (int j = 0; j < cargoSlots.GetLength(1); j++) {
				cargoSlots[i,j] = 0;
				GameObject tempGameObj = new GameObject ();
				tempGameObj.name = "placeHolder" + i + j;
				cargoPlaceholders[i,j] = tempGameObj;
				//cargoPlaceholders[i,j].name = i.ToString + j.ToString;

				Vector3 moveCellCenter = new Vector3((2*halfWidthHeightCell.x)*i,0, (2*halfWidthHeightCell.z)*j );
				//GameObject tempObj = (GameObject)Instantiate(powerCellPrefab, startingPos + moveCellCenter, Quaternion.identity);
				//Position properly empty game objects and parent them to the car
				cargoPlaceholders[i,j].transform.position = startingPos + moveCellCenter;
				cargoPlaceholders[i,j].transform.parent = transform;
			}
		}
	}

	//As solar and wood cells are triggers use onTriggerEnter
	void OnTriggerEnter(Collider other) {
		//Check if trigger is wood or solar cell
		if (other.transform.tag == "WoodCell" || other.transform.tag == "SolarCell") {
			//Fill cargo slot appropriately 
			Vector2 matrixIndex = Vector2.zero;
			if (other.transform.tag == "WoodCell" ) {
				matrixIndex = returnMatIndexOfZeroSlot("wood");
			}
			else if (other.transform.tag == "SolarCell") {
				matrixIndex = returnMatIndexOfZeroSlot("solar");
			}

			//instantiate power cell 
			if (matrixIndex.x != -1) {
				GameObject tempObj = null;
				if (other.transform.tag == "WoodCell" ) {
					tempObj = (GameObject)Instantiate(powerCellPrefab, cargoPlaceholders[(int)matrixIndex.x,(int)matrixIndex.y].transform.position, transform.rotation);

				}
				else if (other.transform.tag == "SolarCell") {
					tempObj = (GameObject)Instantiate(SolarCellPrefab, cargoPlaceholders[(int)matrixIndex.x,(int)matrixIndex.y].transform.position, transform.rotation);

				}
				//destroy the float script of the prefab
				Destroy(tempObj.GetComponent<FloatObject>());
				//destroy the collided object
				tempObj.transform.parent = cargoPlaceholders[(int)matrixIndex.x,(int)matrixIndex.y].transform;
			}
			Destroy(other.gameObject);
			
		}
		//		if (other.relativeVelocity.magnitude > 2)
//			audio.Play();
		
	}
	//check if there are free slots in the cargo, if there are - fill them with wood, solar or water cells
	public Vector2 returnMatIndexOfZeroSlot(string typeCollected){
		for (int i = 0; i < cargoSlots.GetLength(0); i++) {
			for (int j = 0; j < cargoSlots.GetLength(1); j++) {
				if (cargoSlots[i,j] == 0) {
					if (typeCollected == "wood") {
						cargoSlots[i,j] = 1;
					}
					else if (typeCollected == "solar"){
						cargoSlots[i,j] = 2;
					}
					else if (typeCollected == "water"){
						cargoSlots[i,j] = 3;
					}
					Vector2 returnMatIndex = new Vector2(i,j);
					return returnMatIndex;
				}
			}
		}

		return new Vector2 (-1, -1);

	}


}
