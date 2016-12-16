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

	public void killEnemies(){
		GameObject[] alive = new GameObject[enemies.Length - 1];
		int i = 0;
		int j = 0;


		int killIndex = 0;
		for (i = 0; i < enemies.Length; i++) {
			if (enemies [i].GetComponent<Enemy> ().HP <= 0) {
				killIndex = i;
			} else {
				alive [j] = enemies [i];
				j++;
			}
		}

		Destroy (enemies [killIndex]);

		enemies = alive;
		Debug.Log (enemies.Length);
	}
}
