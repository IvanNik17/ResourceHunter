using UnityEngine;
using System.Collections;
//Audio for the hover car - change pitch of the audio with the velocity of the car for different pitch when it is moving and staying still
public class HoverAudio : MonoBehaviour {
	
	public AudioSource jetSound;
	private float jetPitch;
	private const float LowPitch = .1f;
	private const float HighPitch = 2.0f;
	private const float SpeedToRevs = .01f;
	Vector3 myVelocity;
	Rigidbody carRigidbody;
	
	void Awake () 
	{
		carRigidbody = GetComponent<Rigidbody>();
	}
	
	private void FixedUpdate()
	{
		myVelocity = carRigidbody.velocity;
		float forwardSpeed = transform.InverseTransformDirection(myVelocity).z;
		float engineRevs = Mathf.Abs (forwardSpeed) * SpeedToRevs;
		jetSound.pitch = Mathf.Clamp (engineRevs, LowPitch, HighPitch);
	}
	
}