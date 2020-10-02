using UnityEngine;
using System.Collections;
//Spawn ramps randomly on the field
public class RampSpawn : MonoBehaviour {


	public GameObject RampPrefab;
	public GameObject PowerCellPrefab;
	private GameObject mapField;
	private Vector3 startingPosGroundXZ;
	private Vector3 endPosGroundZ;
	private Vector3 endPosGroundX;
	private int numberOfRampsToPut;
	private int rampCounter = 0;

	// Use this for initialization
	void Start () {
		mapField = GameObject.Find("Ground");
		
		startingPosGroundXZ = GetComponent<CalculateGround> ().startingPosGroundXZ;
		endPosGroundZ = GetComponent<CalculateGround> ().endPosGroundZ;
		endPosGroundX = GetComponent<CalculateGround> ().endPosGroundX;

		numberOfRampsToPut = WinningCondition.numberOfSolarCellsNeeded + 2;

		float rampCubeY = mapField.transform.position.y + mapField.transform.GetComponent<Renderer> ().bounds.size.y / 2;
		//Identify zone in which ramps should not spawn - too close to the borders
		float sizeOfNoSpawnZone = mapField.transform.GetComponent<Collider> ().bounds.size.x/2 ;

		//spawn a number of ramps - randomize ramp steepness, rotation and width. Add solar cell to each ramp for collecting.
		while (rampCounter < numberOfRampsToPut) {
			float randomXRamp = Random.Range (startingPosGroundXZ.x + sizeOfNoSpawnZone, endPosGroundX.x- sizeOfNoSpawnZone);
			float randomZRamp = Random.Range (endPosGroundZ.z + sizeOfNoSpawnZone, startingPosGroundXZ.z - sizeOfNoSpawnZone);

			while (!GetComponent<IsThereObject> ().CheckForSpawnable(new Vector3( randomXRamp,rampCubeY,randomZRamp))) {
				randomXRamp = Random.Range (startingPosGroundXZ.x, endPosGroundX.x);
				randomZRamp = Random.Range (endPosGroundZ.z, startingPosGroundXZ.z);
			}

			Vector3 rampPosition = new Vector3 (randomXRamp, rampCubeY, randomZRamp);
			Quaternion rampRotation = Quaternion.Euler(Random.Range(4.0f,16.0f),Random.Range(0,360),0);

			GameObject rampTemp = (GameObject)Instantiate(RampPrefab, rampPosition, rampRotation);
			foreach (Transform actualRamp in rampTemp.transform) {
				actualRamp.localScale = new Vector3 (Random.Range(3.0f,10.0f), rampCubeY, Random.Range(5.0f,10.0f));
			}


			Vector3 powerCellPosition = Vector3.zero;

			powerCellPosition = new Vector3 (rampPosition.x, rampPosition.y + Random.Range(5.0f, 7.0f),rampPosition.z ) + rampTemp.transform.forward*12;
			Instantiate(PowerCellPrefab, powerCellPosition, Quaternion.identity);

			rampCounter++;
		}

	}

}
