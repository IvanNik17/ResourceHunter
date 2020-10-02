using UnityEngine;
using System.Collections;

//Randomly spawn and procedurally generate randomized trees on the field
public class TreeSpawn : MonoBehaviour {

	//prefabs for the trunk, branch, small and big top
	public GameObject trunkPrefab;
	public GameObject smallBranchPrefab;
	public GameObject TopBranchPrefab;
	public GameObject topPrefab;

	private GameObject mapField;
	private GameObject treeHolder;
	private Vector3 startingPosGroundXZ;
	private Vector3 endPosGroundZ;
	private Vector3 endPosGroundX;
	private float trunkFirstCubeY;

	//number of trees to spawn and a counter
	public int numberOfTreesToGrow = 4;
	private int treeCounter = 0;


	// Use this for initialization
	void Start () {


		treeHolder = GameObject.Find ("TreesHolder");
		mapField = GameObject.Find("Ground");


		startingPosGroundXZ = GetComponent<CalculateGround> ().startingPosGroundXZ;
		endPosGroundZ = GetComponent<CalculateGround> ().endPosGroundZ;
		endPosGroundX = GetComponent<CalculateGround> ().endPosGroundX;

		trunkFirstCubeY = mapField.transform.position.y + mapField.transform.GetComponent<Renderer> ().bounds.size.y / 2 + trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.y / 2;
		// For each tree select a random location, plant first segment, then randomly select how tall the tree will be and spawn additional segments with a top.
		//Then select random number of branches and random places for the branches
		while (treeCounter < numberOfTreesToGrow) {
			float randomXTrunk = Random.Range (startingPosGroundXZ.x, endPosGroundX.x);
			float randomZTrunk = Random.Range (endPosGroundZ.z, startingPosGroundXZ.z);

			while (!GetComponent<IsThereObject> ().CheckForSpawnable(new Vector3( randomXTrunk,trunkFirstCubeY,randomZTrunk))) {
				randomXTrunk = Random.Range (startingPosGroundXZ.x, endPosGroundX.x);
				randomZTrunk = Random.Range (endPosGroundZ.z, startingPosGroundXZ.z);
			}

			Vector3 seedPosition = new Vector3 (randomXTrunk, trunkFirstCubeY, randomZTrunk);
			GameObject bottomTrunk = (GameObject)Instantiate(trunkPrefab, seedPosition, Quaternion.identity);
			bottomTrunk.name = "Tree";
			bottomTrunk.transform.parent = treeHolder.transform;
			
			int treeHeight = Random.Range(1,10);
			Vector3 trunkPiecePos = Vector3.zero;
			for (int i = 1; i <= treeHeight; i++) {
				trunkPiecePos = new Vector3(0,i*trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.y ,0);
				GameObject tempTrunkPiece =  (GameObject)Instantiate(trunkPrefab, seedPosition + trunkPiecePos, Quaternion.identity);
				tempTrunkPiece.transform.parent = bottomTrunk.transform;
			}
			
			Vector3 topPiecePos = new Vector3(0,trunkPiecePos.y + trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.y/2 + topPrefab.transform.GetComponent<Renderer> ().bounds.size.y/2 ,0);
			GameObject tempTopPiece =  (GameObject)Instantiate(topPrefab, seedPosition + topPiecePos, Quaternion.identity);
			tempTopPiece.transform.parent = bottomTrunk.transform;
			
			if (treeHeight > 2) {
				int numberOfBranches = Random.Range(1,treeHeight - 1);
				for (int i = 0; i <= numberOfBranches; i++) {
					int whichTrunk = Random.Range(2,treeHeight);
					Vector3 BranchTrunkPiecePos = new Vector3(0,whichTrunk*trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.y ,0);
					int chooseTrunkSide = Random.Range(1,5);
					
					Vector3 branchPiecePos = Vector3.zero;
					switch (chooseTrunkSide) {
					case 1: branchPiecePos = new Vector3(0,BranchTrunkPiecePos.y,trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.z/2 + smallBranchPrefab.transform.GetComponent<Renderer> ().bounds.size.z/2); break;
					case 2: branchPiecePos = new Vector3(0,BranchTrunkPiecePos.y, -trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.z/2 - smallBranchPrefab.transform.GetComponent<Renderer> ().bounds.size.z/2); break;
					case 3: branchPiecePos = new Vector3(trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.x/2 + smallBranchPrefab.transform.GetComponent<Renderer> ().bounds.size.x/2,BranchTrunkPiecePos.y, 0); break;
					case 4: branchPiecePos = new Vector3(-trunkPrefab.transform.GetComponent<Renderer> ().bounds.size.x/2 - smallBranchPrefab.transform.GetComponent<Renderer> ().bounds.size.x/2,BranchTrunkPiecePos.y, 0); break;
					default:
						break;
					}
					GameObject firstBranch = (GameObject)Instantiate(smallBranchPrefab, seedPosition + branchPiecePos, Quaternion.identity);
					firstBranch.transform.parent = bottomTrunk.transform;
					branchPiecePos = new Vector3(branchPiecePos.x,0,branchPiecePos.z);
					GameObject tempBranchTop = (GameObject)Instantiate(TopBranchPrefab, firstBranch.transform.position + branchPiecePos, Quaternion.identity);
					tempBranchTop.transform.parent = bottomTrunk.transform;
					
				}
			}
			
			treeCounter++;
		}
	
	
	}	

}
