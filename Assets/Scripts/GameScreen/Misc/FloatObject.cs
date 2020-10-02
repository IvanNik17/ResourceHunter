using UnityEngine;
using System.Collections;

//Make floating and rotation effect on the solar and wood cells when they are not picked up
public class FloatObject : MonoBehaviour {

	private float step = 0.0f;
	public float offset;
	private float startPosY;
	int randomSinStart;
	float randomStartPos;
	float rotationAmount = 90.0f;
	void Start() {
		//Randomize starting position in Y direction and sin up-down movement for better variaty.
		startPosY = transform.position.y;
		randomSinStart = Random.Range (0, 2);
		if (randomSinStart == 0) {
			randomSinStart = -1;
		}
		randomStartPos = Random.Range (-0.2f, 0.2f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		step += 0.01f;
		if (step > 99) {
			step = 1;
		}

		//add new starting position and rotation to the cell objects.

		Vector3 newYPos = new Vector3 (transform.position.x,startPosY + randomStartPos+ randomSinStart*Mathf.Sin (step)/5, transform.position.z);
		transform.position = newYPos;
		transform.Rotate (0, rotationAmount * Time.deltaTime/2, 0);
	}
}