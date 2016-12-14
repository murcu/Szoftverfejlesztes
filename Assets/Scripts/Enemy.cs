using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

	public CharacterData data;
	public int xPos, zPos;

	// Use this for initialization
	void Start () {
		float delay = Random.Range (0f, 3f);
		InvokeRepeating ("enemyAttck", delay, 3f); //outsync them with delay and attack every 3 sec
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setData(CharacterData enemy, int x, int z){
		xPos = x;
		zPos = z;
		data = enemy;
	}

	public void enemyAttck(){
		ActivePlayer activePlayer = GameObject.Find ("Player").GetComponent<ActivePlayer> ();

		if (xPos == activePlayer.xPos && zPos == activePlayer.zPos) {
			Debug.Log (transform.name + " attack");
		}
			
	}
}
