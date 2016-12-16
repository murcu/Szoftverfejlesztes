using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using System.Collections;

public class ActiveDungeon : MonoBehaviour {

	public GameObject roomPrefab;
	public int dungeonSize; //size * difficulty = roomNumber
	public int roomScale;

	public GameObject player;
	public GameObject enemySpawner;
	private DungeonData data;
	private GameObject[] rooms;

	private bool open = false;

	// Use this for initialization
	void Start () {
		DungeonData[] dungeons = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().dungeons;

		int i = 0;
		while (i < dungeons.Length && dungeons[i].inProgress != true) {
			i++;
		}
		data = dungeons [i];

		buildDungeon ();
		player.GetComponent<ActivePlayer> ().xPos = 0;
		player.GetComponent<ActivePlayer> ().zPos = 0;

		openCloseRooms ();
	}

	// Update is called once per frame
	void Update () {
		compeleteDungeon ();

		ActivePlayer activePlayer = GameObject.Find ("Player").GetComponent<ActivePlayer> ();
		Room currentRoom = GameObject.Find (activePlayer.xPos + "_" + activePlayer.zPos).GetComponent<Room>();

		if (currentRoom.enemies.Length == 0) {
			openCloseRooms ();
		}
	}

	void buildDungeon(){
		int roomNumber = data.difficulty * dungeonSize;
		rooms = new GameObject[roomNumber];

		for (int i = 0; i < roomNumber; i++) {
			//create the room
			rooms[i] = Instantiate (roomPrefab, transform) as GameObject;

			//after the first room place them at random
			if (i > 0) {				
				int roomIndex = Random.Range (0, i); //selecte room to connect the new one
				while(rooms[roomIndex].GetComponent<Room>().conncted >= 4){ //if it's surrounded on every side pick another one
					roomIndex = Random.Range (0, i);
				}
				rooms [i].GetComponent<Room> ().conncted++; //the new room has one connection
				rooms[roomIndex].GetComponent<Room>().conncted++; //the selected room has plus one connection

				int direction;
				int xPos_new;
				int zPos_new;

				do{
					xPos_new = rooms[roomIndex].GetComponent<Room>().xPos;
					zPos_new = rooms[roomIndex].GetComponent<Room>().zPos;
					direction = Random.Range (0, 4);
					switch (direction) {
					case 0://north
						zPos_new++;
						break;
					case 1://east
						xPos_new++;
						break;
					case 2://south
						zPos_new--;
						break;
					case 3://west
						xPos_new--;
						break;
					}
				}while(transform.FindChild(xPos_new + "_" + zPos_new) != null);

				//connect the rooms, save which doors are functional
				switch (direction) {
				case 0:
					rooms [roomIndex].GetComponent<Room> ().doors [0] = rooms [roomIndex].transform.FindChild ("NorthDoor").gameObject;
					rooms [i].GetComponent<Room> ().doors [2] = rooms [i].transform.FindChild ("SouthDoor").gameObject;

					rooms [i].transform.FindChild ("PlayerPos").transform.position = new Vector3(0f, 0f, -3f) * roomScale;
					rooms [i].transform.FindChild ("EnemyPos").transform.position = new Vector3(0f, 0f, 3f) * roomScale;
					break;
				case 1:
					rooms [roomIndex].GetComponent<Room> ().doors [1] = rooms [roomIndex].transform.FindChild ("EastDoor").gameObject;
					rooms [i].GetComponent<Room> ().doors [3] = rooms [i].transform.FindChild ("WestDoor").gameObject;

					rooms [i].transform.FindChild ("PlayerPos").transform.position = new Vector3(-3f, 0f, 0f) * roomScale;
					rooms [i].transform.FindChild ("EnemyPos").transform.position = new Vector3(3f, 0f, 0f) * roomScale;
					break;
				case 2:
					rooms [roomIndex].GetComponent<Room> ().doors [2] = rooms [roomIndex].transform.FindChild ("SouthDoor").gameObject;
					rooms [i].GetComponent<Room> ().doors [0] = rooms [i].transform.FindChild ("NorthDoor").gameObject;

					rooms [i].transform.FindChild ("PlayerPos").transform.position = new Vector3(0f, 0f, 3f) * roomScale;
					rooms [i].transform.FindChild ("EnemyPos").transform.position = new Vector3(0f, 0f, -3f) * roomScale;
					break;
				case 3:
					rooms [roomIndex].GetComponent<Room> ().doors [3] = rooms [roomIndex].transform.FindChild ("WestDoor").gameObject;
					rooms [i].GetComponent<Room> ().doors [1] = rooms [i].transform.FindChild ("EastDoor").gameObject;

					rooms [i].transform.FindChild ("PlayerPos").transform.position = new Vector3(3f, 0f, 0f) * roomScale;
					rooms [i].transform.FindChild ("EnemyPos").transform.position = new Vector3(-3f, 0f, 0f) * roomScale;
					break;
				}

				rooms[i].GetComponent<Room>().xPos = xPos_new;
				rooms[i].GetComponent<Room>().zPos = zPos_new;

				if (i == roomNumber - 1) {
					enemySpawner.GetComponent<EnemySpawner> ().SpawnEnemiesinRoom (rooms[i].GetComponent<Room>(), true);
				} else {
					enemySpawner.GetComponent<EnemySpawner> ().SpawnEnemiesinRoom (rooms[i].GetComponent<Room>(), false);
				}
			}
				
			rooms [i].transform.localScale = new Vector3 (roomScale, rooms [i].transform.localScale.y, roomScale);
			rooms [i].transform.name = rooms[i].GetComponent<Room>().xPos + "_" + rooms[i].GetComponent<Room>().zPos;
			rooms [i].transform.position = new Vector3(rooms[i].GetComponent<Room>().xPos * 10f * roomScale, 0f, rooms[i].GetComponent<Room>().zPos * 10f * roomScale);
		}

		//diffent floor color for starting and ending room, for testing only
		rooms [0].GetComponent<Renderer> ().material.color = new Color (1f, 1f, 1f);
		rooms[roomNumber-1].GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f); 
	}

	public void openCloseRooms(){
		ActivePlayer activePlayer = player.GetComponent<ActivePlayer> ();

		//check if the room has any monsters
		if (GameObject.Find(activePlayer.xPos + "_" + activePlayer.zPos).GetComponent<Room>().enemies.Length == 0) {
			if (!open) {
				for (int i = 0; i < rooms.Length; i++) {
					Room room = rooms [i].GetComponent<Room> ();
					for (int j = 0; j < room.doors.Length; j++) {
						if (room.doors [j]) {
							room.doors [j].transform.position -= new Vector3 (0f, 100f, 0f); //move down
						}
					}
				}
				open = true;
			}
			player.GetComponent<ActivePlayer> ().combatMode = false;
		} else {
			if (open) {
				for (int i = 0; i < rooms.Length; i++) {
					Room room = rooms [i].GetComponent<Room> ();
					for (int j = 0; j < room.doors.Length; j++) {
						if (room.doors [j]) {
							room.doors [j].transform.position += new Vector3 (0f, 100f, 0f); //move up
						}
					}
				}
				open = false;
			}
			player.GetComponent<ActivePlayer> ().combatMode = true;
		}
	}

	void compeleteDungeon(){
		ActivePlayer activePlayer = player.GetComponent<ActivePlayer> ();
		Room bossRoom = rooms [rooms.Length - 1].GetComponent<Room> ();

		if(activePlayer.xPos == bossRoom.xPos && activePlayer.zPos == bossRoom.zPos){//player is in the last room
			if (bossRoom.enemies.Length == 0) {//there are no enemies left in the room
				data.inProgress = false;
				data.completed = true;

				activePlayer.data.currHealth = activePlayer.data.maxHealth;
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex -1);
			}
		}

		if (activePlayer.data.currHealth <= 0) {
			Debug.Log ("player dead");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 2);
		}
	}
}
