﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkContainer : MonoBehaviour {

	public HashSet<Vector2> map = new HashSet<Vector2>();
	public Transform chunk;

	private int chunkSize;
	private float chunkSizeInUnits;
	public int chunkRenderDistance = 3;

	private Camera cam;
	private float camSize;

	LevelImporter levelImporter;
	List< List<string>> levels; 
	
	void Awake(){
		chunkSize = 16;
		chunkSizeInUnits = 0.32f * chunkSize;

		Debug.Log ("Starting world generator...");
		levelImporter = new LevelImporter ();
		levelImporter.import ();

		Debug.Log ("Getting levels from importer...");
		levels = levelImporter.getLevels ();
		Debug.Log ("Nr. of levels imported: " + levels.Count);
	}

	// Use this for initialization
	void Start () {
		cam = Camera.main;

		Debug.Log ("Adding first 9 chunks...");
		// generate the first 9 chunks so people don't see that it's all fake
		spawnChunk (new Vector2 (-1, -1));
		spawnChunk (new Vector2 (0, -1));
		spawnChunk (new Vector2 (1, -1));
		spawnChunk (new Vector2 (-1, 0));
		spawnChunk (new Vector2 (0, 0));
		spawnChunk (new Vector2 (1, 0));
		spawnChunk (new Vector2 (-1, 1));
		spawnChunk (new Vector2 (0, 1));
		spawnChunk (new Vector2 (1, 1));

		// remember which chunks have been set
		map.Add (new Vector2 (-1, -1));
		map.Add (new Vector2 (0, -1));
		map.Add (new Vector2 (1, -1));
		map.Add (new Vector2 (-1, 0));
		map.Add (new Vector2 (0, 0));
		map.Add (new Vector2 (1, 0));
		map.Add (new Vector2 (-1, 1));
		map.Add (new Vector2 (0, 1));
		map.Add (new Vector2 (1, 1));
	}

	public bool isAvailable(Vector2 loc){
		return !map.Contains (loc);
	}

	void addIfAvailable(Vector2 loc){
		if (isAvailable(loc) == true){
			spawnChunk(loc);
			map.Add (loc);
		}
	}

	void spawnChunk(Vector2 loc){
		Vector3 location = new Vector3 ((loc.x * chunkSize) - 8, (loc.y * chunkSize) - 8, 0);

		Transform cloneChunk;
		cloneChunk = Instantiate (chunk, location, new Quaternion()) as Transform;

		cloneChunk.GetComponent<ChunkGenerator> ().populateChunk(levels);

	}

	// Update is called once per frame
	void Update () {

		//generate new chunks when needed
		float x = Mathf.Round (cam.transform.position.x / chunkSizeInUnits);
		float y = Mathf.Round (cam.transform.position.y / chunkSizeInUnits);

		Debug.Log (new Vector2 (x, y));

		for (int xOffset = -chunkRenderDistance; xOffset < chunkRenderDistance; xOffset++){
			for (int yOffset = -chunkRenderDistance; yOffset < chunkRenderDistance; yOffset++){
				addIfAvailable (new Vector2 (x + xOffset, y + yOffset));
			}
		}
	}

}
