using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

	public CharacterData data;
	public int xPos, zPos;
	public int HP;

	Animator anim;

	// Use this for initialization
	void Start () {
		float delay = Random.Range (0f, 3f);
		anim = transform.FindChild("MorphedHumanDoll").GetComponent<Animator> ();

		InvokeRepeating ("enemyAttack", delay, 3f); //outsync them with delay and attack every 3 sec
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find ("Player");
		transform.LookAt (player.transform.position);
		if (HP <= 0 && dataSet) {
			Room currRoom = GameObject.Find (xPos + "_" + zPos).GetComponent<Room>();
			currRoom.killEnemies ();
			ActivePlayer activePlayer = GameObject.Find ("Player").GetComponent<ActivePlayer> ();
			activePlayer.data.points += data.points;
		}
	}

	bool dataSet = false;
	public void setData(CharacterData enemy, int x, int z){
		xPos = x;
		zPos = z;
		data = enemy;
		HP = data.maxHealth;
		Debug.Log (xPos + "_" + zPos);
		dataSet = true;
	}

	public void enemyAttack(){
		ActivePlayer activePlayer = GameObject.Find ("Player").GetComponent<ActivePlayer> ();
		if (xPos == activePlayer.xPos && zPos == activePlayer.zPos) {
			Debug.Log ("player found: " + xPos + " " + zPos);
			if (!activePlayer.defend) {
				anim.Play ("Attack");
				activePlayer.data.currHealth -= 1;
			}
		}
		anim.Play ("Idle");	
	}
}
