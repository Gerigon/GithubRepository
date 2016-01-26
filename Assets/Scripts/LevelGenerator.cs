using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public int lvlPiece = 0;
	public Vector2 lastSpawnPoint = Vector2.zero -Vector2.left*20;
	public List<GameObject> levelPieces = new List<GameObject>();
	public List<GameObject> spawnedPieces = new List<GameObject>();

	// Use this for initialization
	void Start () {
		//lastSpawnPoint.y = -1;
		InvokeRepeating("AddPiece",0.01f,2);
		//AddPiece();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddPiece() {
		if(spawnedPieces.Count>=10)
			CancelInvoke("AddPiece");
		//pick a random level piece to spawn. Min included,Max excluded.
		lvlPiece = Random.Range(0,levelPieces.Count);
		//store the lastSpawnPoint in a new variable for temporary use.
		Vector2 newSpawnPoint = lastSpawnPoint;
		//the next piece should be spawned 20 units more to the right.
		newSpawnPoint.x += 20;
		//update the last position where a level piece was spawned.
		lastSpawnPoint = newSpawnPoint;


		//spawn the level piece at the newly calculated position and store it so it can be removed later.
		GameObject tmpPiece;
		tmpPiece = Instantiate(levelPieces[lvlPiece],newSpawnPoint, Quaternion.identity) as GameObject;
		spawnedPieces.Add(tmpPiece);
	}

	public void RemovePiece() {
		
	}
}
