using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarControl : MonoBehaviour {

	//Car movement variables
	public float speed = 90f;
	public float turnSpeed = 5f;
	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;

	//Prefab for creating the bullets
	public GameObject bulletPrefab;

	//dead zone area for gyro input on mobile
	private float deadZone = .001f;
	//accelerometer variable
	private Vector3 accel;
	//variables for  turning and forward and backward acceleration
	private float powerInput;
	private float turnInput;

	//rigid body of the car prefab
	private Rigidbody carRigidbody;

	//sensitivity for mobile rotation
	private float rotationSensitivity = 2;

	//object for the gameObject which is used for a starting point of the spawned bullets
	private GameObject bulletSpawnerObj;

	//is the gun shot and force of the shot
	private bool shootGun;
	private float shootForce = 1200.0f;
	void Awake () 
	{
		//get car rigid body, move center of mass to the bottom of the car to avoid turnovers
		carRigidbody = GetComponent <Rigidbody>();
		carRigidbody.centerOfMass = new Vector3(0f,-1f,0f);
		bulletSpawnerObj = GameObject.Find("BulletSpawner");
	}
	
	void Update () {
		//mobile and pc car controls
		ControlCar ();

	}

	void ControlCar(){

		//Determine platform on which the game is running
		if (Application.platform == RuntimePlatform.Android) {
			accel = Input.acceleration * rotationSensitivity;

			//check for the dead zone
			if (accel.x > deadZone || accel.x < -deadZone) {
				turnInput = accel.x; // add stearing acceleration
			}
			else {
				turnInput = 0;
			}
			powerInput = 0;
			//Add forward/backward force
			foreach (Touch touch in Input.touches) {
//				if (touch.position.x > Screen.width - Screen.width/3 && touch.position.y < Screen.height/3){
//					powerInput = 1;
//				}
//				else if (touch.position.x < Screen.width/3 && touch.position.y < Screen.height/3){
//					powerInput = -1;
//				}
				if (touch.position.x < Screen.width/3 && touch.position.y > Screen.height/3){
					powerInput = 1;
				}
				else if (touch.position.x < Screen.width/3 && touch.position.y < Screen.height/3){
					powerInput = -1;
				}

				if (touch.position.x > Screen.width/3 && touch.position.y < Screen.height/3 && Input.GetTouch(0).phase == TouchPhase.Began) {

					shootGun = true;
				}
				else {
					shootGun = false;
				}
			}
		}
		//In editor use keyboard keys 
		else if (Application.platform == RuntimePlatform.WindowsEditor) {
			powerInput = Input.GetAxis ("Vertical");
			turnInput = Input.GetAxis ("Horizontal");
			if (Input.GetKeyDown(KeyCode.Space)) shootGun = true;
			else shootGun = false;
		}
	}
	//fixed update for proper physics movement
	void FixedUpdate()
	{
		//create ray from car towards ground
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		//if ray hits ground or other object
		if (Physics.Raycast(ray, out hit, hoverHeight)){
			//calculate height from car to ground
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			//create a force vector from ground to car, to make it hover
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration); // add the force

			//check if car is on a ramp - change rotation of car to the one of the ramp for smooth ascend/descend
			if (transform.rotation != Quaternion.Euler(hit.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0)) {
				transform.rotation = Quaternion.Euler(hit.transform.rotation.eulerAngles.x * Mathf.Sign(powerInput), transform.rotation.eulerAngles.y, 0);

			}
		}// if there is no ground object or ramp under the car lerp the rotation from the current to a zeroed out one in x and z
		else {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Time.fixedDeltaTime );
		}

		//add force to the car rigid body
		carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
		//check if car is moving forward or backward and apply torque appropriately 
		if (powerInput >= 0) {
			carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
		}
		else {
			carRigidbody.AddRelativeTorque(0f, -turnInput * turnSpeed, 0f);
		}

		//if the gun was shot instantiate a bullet and add force to it
		if (shootGun) {
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawnerObj.transform.position, bulletSpawnerObj.transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * shootForce);

		}


	}


}