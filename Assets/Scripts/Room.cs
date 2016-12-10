using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public int xPos, zPos;

	public int conncted = 0;
	public GameObject[] doors = new GameObject[4];

	public GameObject [] enemies; //the enemies in the room

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
