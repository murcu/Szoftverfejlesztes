using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	[Range(1, 4)]
	public int enemyCount;
	public CharacterData[] enemies;
	public CharacterData[] bosses;
	// Use this for initialization
	void Start () {
		enemies = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().monsters;
		bosses = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().bosses;
	}
	
	// Update is called once per frame
	void Update () {	
	}

	public void SpawnEnemiesinRoom(Room room, bool boss){
		if (!boss) {//spawn random enemies at the room
			int enemyIndex = Random.Range(0, enemies.Length-1);
			room.enemies = new GameObject[enemyCount];
			for(int i = 0; i < enemyCount; i++){
				GameObject enemy = Instantiate (enemies [enemyIndex].characterPrefab, room.transform) as GameObject;
				enemy.transform.name = "Enemy_" + i;

				enemy.GetComponent<Enemy> ().setData (enemies [enemyIndex], room.xPos, room.zPos);

				enemy.transform.position = room.transform.position;
				if (i > 0) {
					enemy.transform.position = room.enemies [i - 1].transform.position + new Vector3 (10f, 0f, 0f);
				}

				room.enemies [i] = enemy;
			}
		} else {//spawn a boos at the room
			room.enemies = new GameObject[1];
			GameObject enemy = Instantiate (bosses [0].characterPrefab, room.transform) as GameObject;

			enemy.transform.position = room.transform.position;
			room.enemies [0] = enemy;
		}
	}
}
