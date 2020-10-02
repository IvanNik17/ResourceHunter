using UnityEngine;
using System.Collections;

//First script from the spawner/ procedural generation scripts
public class CalculateGround : MonoBehaviour {

	//playing field object
	private GameObject mapField;
	//vector3 positions of the top left, bottom left and top right corner of the the field object.
	public Vector3 startingPosGroundXZ;
	public Vector3 endPosGroundZ;
	public Vector3 endPosGroundX;
	private float trunkFirstCubeY;

	private Vector3 halfWidthHeightGroundXminZ;
	private Vector3 halfWidthHeightGroundXZ;
	private Vector3 halfWidthHeightGroundMinZminX;

	public static Vector2 groundSize;
	// Use this for initialization
	void Awake () {
		mapField = GameObject.Find("Ground");
		//scale the map according to what you choose in the selection screen
		mapField.transform.localScale  = new Vector3(groundSize.x,0.1f,groundSize.y);

		//calculate positios on the map
		halfWidthHeightGroundXminZ = new Vector3(mapField.transform.GetComponent<Renderer>().bounds.size.x/2,0,-mapField.transform.GetComponent<Renderer>().bounds.size.z/2);
		halfWidthHeightGroundXZ = new Vector3(mapField.transform.GetComponent<Renderer>().bounds.size.x/2,0,mapField.transform.GetComponent<Renderer>().bounds.size.z/2);
		halfWidthHeightGroundMinZminX = new Vector3(-mapField.transform.GetComponent<Renderer>().bounds.size.x/2,0,-mapField.transform.GetComponent<Renderer>().bounds.size.z/2);

		//get sizes of the positions
		startingPosGroundXZ = mapField.transform.position - halfWidthHeightGroundXminZ;
		endPosGroundZ = mapField.transform.position + halfWidthHeightGroundMinZminX;
		endPosGroundX = mapField.transform.position + halfWidthHeightGroundXZ;

		//Create walls for the map - top, bottom, left and right
		GameObject topWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		topWall.transform.position = new Vector3 (mapField.transform.position.x, mapField.transform.position.y, mapField.transform.position.z + mapField.transform.GetComponent<Renderer> ().bounds.size.z / 2);
		topWall.transform.localScale = new Vector3 (mapField.transform.localScale.x, 20, 1);
		topWall.layer = 8;

		GameObject bottomWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		bottomWall.transform.position = new Vector3 (mapField.transform.position.x, mapField.transform.position.y, mapField.transform.position.z - mapField.transform.GetComponent<Renderer> ().bounds.size.z / 2);
		bottomWall.transform.localScale = new Vector3 (mapField.transform.localScale.x, 20, 1);
		bottomWall.layer = 8;

		GameObject leftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		leftWall.transform.position = new Vector3 (mapField.transform.position.x - mapField.transform.GetComponent<Renderer> ().bounds.size.x / 2, mapField.transform.position.y, mapField.transform.position.z);
		leftWall.transform.localScale = new Vector3 (1, 20, mapField.transform.localScale.z);
		leftWall.layer = 8;

		GameObject rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		rightWall.transform.position = new Vector3 (mapField.transform.position.x + mapField.transform.GetComponent<Renderer> ().bounds.size.x / 2, mapField.transform.position.y, mapField.transform.position.z);
		rightWall.transform.localScale = new Vector3 (1, 20, mapField.transform.localScale.z);
		rightWall.layer = 8;

	}


}
