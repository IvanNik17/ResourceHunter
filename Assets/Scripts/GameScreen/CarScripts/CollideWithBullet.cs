using UnityEngine;
using System.Collections;

public class CollideWithBullet : MonoBehaviour {

	//wood cell prefab
	public GameObject powerCellPrefab;

	//is the wood cell hit
	private bool isHit = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//check for hit, if cell is hit instantiate a wood cell object at random location around tree
		if (isHit) {
			for (int i = 0; i < 2; i++) {
				Instantiate(powerCellPrefab, transform.position + new Vector3(Random.Range(0,4),0, Random.Range(0,4)), Quaternion.identity);
				//woodCell.GetComponent<Rigidbody>().isKinematic = false;
				//woodCell.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(20,40),00,Random.Range(20,40)));
			}
			isHit = false;
		}

	}

	//on collision enter, check for bullet hit.
	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Bullet") {
			// check if parent or child tree part is hit
			GameObject parentOfCollision = null;
			if (transform.name == "Tree") {
				parentOfCollision = transform.gameObject;
			}
			else {
				parentOfCollision = transform.parent.gameObject;
			}

			isHit = true;


			// once a tree part is hit make all of the trees parts non-kinematic and  make them fly around with additon of force. Destroy them after 10 sec
			foreach (Transform treeObj in parentOfCollision.transform) {
				treeObj.GetComponent<Rigidbody>().isKinematic = false;
				treeObj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0,60),0,Random.Range(0,60)));
				Destroy (treeObj.gameObject,10);
			}
		}
		
	}
}
