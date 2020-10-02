using UnityEngine;
using System.Collections;
//Check if there is object at random location before spawning another object at that location
public class IsThereObject : MonoBehaviour {
	

	public bool CheckForSpawnable(Vector3 center/*, float radius*/) {
		var mask = 1<<8;
		Collider[] hitColliders = Physics.OverlapSphere(center, 5,mask);

		if (hitColliders.Length > 0) {
			return false;
		}
		else {
			return true;
		}

	}
}
