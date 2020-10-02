using UnityEngine;
using System.Collections;

//Spawn docking station randomly on the field
public class DockingStationSpawn : MonoBehaviour {

	public GameObject dockPrefab;
	private GameObject mapField;
	private Vector3 startingPosGroundXZ;
	private Vector3 endPosGroundZ;
	private Vector3 endPosGroundX;

	// Use this for initialization
	void Start () {
		mapField = GameObject.Find("Ground");
		
		startingPosGroundXZ = GetComponent<CalculateGround> ().startingPosGroundXZ;
		endPosGroundZ = GetComponent<CalculateGround> ().endPosGroundZ;
		endPosGroundX = GetComponent<CalculateGround> ().endPosGroundX;
		
		
		Vector3 prefabSize = dockPrefab.transform.GetComponent<Collider> ().bounds.size;
		float dockCubeY = mapField.transform.position.y + mapField.transform.GetComponent<Renderer> ().bounds.size.y;
		

		float randomXDock = Random.Range (startingPosGroundXZ.x + prefabSize.x/2, endPosGroundX.x- prefabSize.x/2);
		float randomZDock = Random.Range (endPosGroundZ.z + prefabSize.z/2, startingPosGroundXZ.z - prefabSize.z/2);
		
		while (!GetComponent<IsThereObject> ().CheckForSpawnable(new Vector3( randomXDock,dockCubeY,randomZDock))) {
			randomXDock = Random.Range (startingPosGroundXZ.x, endPosGroundX.x);
			randomZDock = Random.Range (endPosGroundZ.z, startingPosGroundXZ.z);
		}
		
		Vector3 dockPosition = new Vector3 (randomXDock, dockCubeY, randomZDock);
		Instantiate(dockPrefab, dockPosition, Quaternion.identity);
			

	}
	

}
