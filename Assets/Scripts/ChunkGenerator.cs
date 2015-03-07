﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour {

	public Transform tileFloor;
	public int tileSize = 5;

	// Use this for initialization
	void Start () {
		// add some tiles yo
		for (int x = 0; x < 16; x++) {
			for (int y = 0; y < 16; y++){
				if (Random.value >= 0.7){
					placeTile(new Vector2(transform.position.x + x, transform.position.y + y), tileFloor);
				}
			}
		}
	}

//	public void populateChunk(List<List<string>> levels){
//
//	}

	void placeTile(Vector2 loc, Transform tile){
		Vector3 location = new Vector3 (loc.x * tileSize, loc.y * tileSize, 0);
		Instantiate (tile, location, new Quaternion());
	}

	// Update is called once per frame
	void Update () {
	
	}
}