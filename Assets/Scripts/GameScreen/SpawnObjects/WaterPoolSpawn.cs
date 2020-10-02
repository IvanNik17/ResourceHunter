using UnityEngine;
using System.Collections;
//Spawn water pools at random locations on the field.
public class WaterPoolSpawn : MonoBehaviour {

	public GameObject waterPoolPrefab;
	private GameObject mapField;
	private Vector3 startingPosGroundXZ;
	private Vector3 endPosGroundZ;
	private Vector3 endPosGroundX;
	public int numberOfPoolsToPut = 2;
	private int poolCounter = 0;
	// Use this for initialization
	void Start () {
		mapField = GameObject.Find("Ground");
		
		startingPosGroundXZ = GetComponent<CalculateGround> ().startingPosGroundXZ;
		endPosGroundZ = GetComponent<CalculateGround> ().endPosGroundZ;
		endPosGroundX = GetComponent<CalculateGround> ().endPosGroundX;


		Vector3 prefabSize = waterPoolPrefab.transform.GetComponent<Collider> ().bounds.size;
		float poolCubeY = mapField.transform.position.y + mapField.transform.GetComponent<Renderer> ().bounds.size.y;

		while (poolCounter < numberOfPoolsToPut) {
			float randomXPool = Random.Range (startingPosGroundXZ.x + prefabSize.x/2, endPosGroundX.x- prefabSize.x/2);
			float randomZPool = Random.Range (endPosGroundZ.z + prefabSize.z/2, startingPosGroundXZ.z - prefabSize.z/2);

			while (!GetComponent<IsThereObject> ().CheckForSpawnable(new Vector3( randomXPool,poolCubeY,randomZPool))) {
				randomXPool = Random.Range (startingPosGroundXZ.x, endPosGroundX.x);
				randomZPool = Random.Range (endPosGroundZ.z, startingPosGroundXZ.z);
			}

			Vector3 poolPosition = new Vector3 (randomXPool, poolCubeY, randomZPool);
			Instantiate(waterPoolPrefab, poolPosition, Quaternion.identity);

			poolCounter++;
		}
	}

}
