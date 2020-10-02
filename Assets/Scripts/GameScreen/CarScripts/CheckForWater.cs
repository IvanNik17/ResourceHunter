using UnityEngine;
using System.Collections;

//Check for water under the car ,for getting water cells
public class CheckForWater : MonoBehaviour {

	//water cell prefab
	public GameObject waterCellPrefab;
	//height to which the car is hovering
	public float hoverHeight = 3.5f;
	//initialize timer for gradual filling of cells
	int timer = -1;
	//time it takes to fill one cell
	int cellFillingTime = 100;
	//initialize object of time CollisionWithPowerCell, which contains cargo placeholder arrays
	private CollisionWithPowerCell placeholderScript;
	//how fast the cell fills up
	float fillUpCell = 0.005f;
	GameObject CellObj = null;
	Vector2 matrixIndex = new Vector2(-2.0f,-2.0f);
	// Use this for initialization
	void Start () {
		//get CollisionWithPowerCell component reference
		placeholderScript = GetComponent<CollisionWithPowerCell> ();
	}
	
	// Update is called once per frame
	void Update () {
		//get timer
		int elapsedTime = TimerTick();

		//cast ray from car to ground
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		//if something is hit check if its water and if the car is not moving
		if (Physics.Raycast(ray, out hit, hoverHeight)){
			if (hit.transform.tag == "Water" && (transform.GetComponent<Rigidbody> ().velocity.z < 0.1f && transform.GetComponent<Rigidbody> ().velocity.z > -0.1f )) {
				//if timer is not started
				if (elapsedTime == -1) {
					matrixIndex = placeholderScript.returnMatIndexOfZeroSlot("water");// check for free cargo slot and fill it up with a water cell
					if (matrixIndex.x != -1) {
						// set timer
						timer = cellFillingTime;
						//instantiate a watercell prefab at the place of a free cargo slot
						CellObj = (GameObject)Instantiate(waterCellPrefab, placeholderScript.cargoPlaceholders[(int)matrixIndex.x,(int)matrixIndex.y].transform.position, transform.rotation);
						//parent to the cargo slot
						CellObj.transform.parent = placeholderScript.cargoPlaceholders[(int)matrixIndex.x,(int)matrixIndex.y].transform;
						//move center of mass below cell
						CellObj.GetComponent<Rigidbody>().centerOfMass = new Vector3(0f,-0.4f,0f);
						//make cell very small with localscale
						CellObj.transform.localScale = new Vector3(CellObj.transform.localScale.x,0.05f, CellObj.transform.localScale.z);


					}

				}
			}
			else {
				//if car moves before cell is filled -destro cell and free space
				if (elapsedTime > 0 && CellObj != null) {
					Destroy(CellObj);
					placeholderScript.cargoSlots[(int)matrixIndex.x,(int)matrixIndex.y] = 0;
				}
			}

		}
		if (elapsedTime > 0 && CellObj != null) {
			//gradually make cell larger as timer ticks
			CellObj.transform.localScale += new Vector3(0,fillUpCell, 0);
		}

	}
	//simple timer method
	int TimerTick(){
		if (timer>= 0) {
			timer-= 1;
		}
		return timer;
		
	}
}
