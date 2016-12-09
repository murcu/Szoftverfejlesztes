using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public int xPos, zPos;

	public Vector3 enemySpawnPoint;
	public Vector3 playerPos;

	public int conncted = 0;
	public bool open = false;
	public GameObject[] doors = new GameObject[4];

	// Use this for initialization
	void Start () {
		playerPos = Vector3.zero;
		enemySpawnPoint = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
