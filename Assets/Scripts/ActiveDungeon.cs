using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class ActiveDungeon : MonoBehaviour {

	public GameObject roomPrefab;
	public int dungeonSize; //size * difficulty = roomNumber
	public int roomScale;

	public GameObject player;
	public DungeonData data;
	GameObject[] rooms;

	// Use this for initialization
	void Start () {
		/*DungeonData[] dungeons = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().dungeons;

		int i = 0;
		while (i < dungeons.Length && dungeons[i].inProgress != true) {
			i++;
		}
		data = dungeons [i];*/

		buildDungeon ();
		movePlayerToRoom (rooms.Length-1);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void movePlayerToRoom(int i){
		player.transform.position = rooms [i].transform.GetComponent<Room> ().playerPos;
	}

	void buildDungeon(){
		int roomNumber = data.difficulty * dungeonSize;
		rooms = new GameObject[roomNumber];

		for (int i = 0; i < roomNumber; i++) {
			//create the room
			rooms[i] = Instantiate (roomPrefab, transform) as GameObject;

			//after the first room place them at random
			if (i > 0) {				
				int roomIndex = Random.Range (0, i); //selected room
				while(rooms[roomIndex].GetComponent<Room>().conncted >= 4){ //if it's surrounded on every side pick another one
					roomIndex = Random.Range (0, i);
				}
				rooms[roomIndex].GetComponent<Room>().conncted++;

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

				//connect the rooms
				switch (direction) {
				case 0:
					rooms [i].transform.GetComponent<Room> ().playerPos = new Vector3 (0f, 0f, 3f) + rooms [i].transform.FindChild ("SouthDoor").position;

					Destroy (rooms [roomIndex].transform.FindChild ("NorthDoor").gameObject);
					Destroy (rooms [i].transform.FindChild ("SouthDoor").gameObject);
					break;
				case 1:
					rooms [i].transform.GetComponent<Room> ().playerPos = new Vector3 (3f, 0f, 0f) + rooms [i].transform.FindChild ("WestDoor").position;

					Destroy (rooms [roomIndex].transform.FindChild ("EastDoor").gameObject);
					Destroy (rooms [i].transform.FindChild ("WestDoor").gameObject);
					break;
				case 2:
					rooms [i].transform.GetComponent<Room> ().playerPos = new Vector3 (0f, 0f, -3f) + rooms [i].transform.FindChild ("NorthDoor").position;

					Destroy (rooms [roomIndex].transform.FindChild ("SouthDoor").gameObject);
					Destroy (rooms [i].transform.FindChild ("NorthDoor").gameObject);
					break;
				case 3:
					rooms [i].transform.GetComponent<Room> ().playerPos = new Vector3 (-3f, 0f, 0f) + rooms [i].transform.FindChild ("EastDoor").position;

					Destroy (rooms [roomIndex].transform.FindChild ("WestDoor").gameObject);
					Destroy (rooms [i].transform.FindChild ("EastDoor").gameObject);
					break;
				}

				Debug.Log (rooms [i].transform.GetComponent<Room> ().playerPos);
				rooms[i].GetComponent<Room>().xPos = xPos_new;
				rooms[i].GetComponent<Room>().zPos = zPos_new;
			}
				
			rooms [i].transform.localScale = new Vector3 (roomScale, rooms [i].transform.localScale.y, roomScale);
			rooms [i].transform.name = rooms[i].GetComponent<Room>().xPos + "_" + rooms[i].GetComponent<Room>().zPos;
			rooms [i].transform.position = new Vector3(rooms[i].GetComponent<Room>().xPos * 10f * roomScale, 0f, rooms[i].GetComponent<Room>().zPos * 10f * roomScale);

			rooms [i].transform.GetComponent<Room> ().playerPos += rooms [i].transform.position;
			rooms [i].transform.GetComponent<Room> ().playerPos.y = 0f;
		}
	}
}
