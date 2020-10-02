using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {
		//script used by bullets to destroy themselve after 10 sec to no fill CPU
	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, 10);
	}

}
