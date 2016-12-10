using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivePlayer : MonoBehaviour {

	public Text nameText;

	private CharacterData data;

	private Vector3 startPos;
	private Vector3 endPos;
	public float minSwipeDist;
	public int xPos, zPos;

	public bool combatMode = false;

	// Use this for initialization
	void Start () {
		data = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().player;
		nameText.text = data.characterName;
	}

	// Update is called once per frame
	void Update () {
		if (!combatMode) {
			int xPos_new = xPos;
			int zPos_new = zPos;
			int direction = -1;
			if (Input.touchCount == 1) {
				Touch touch = Input.GetTouch (0);

				if (touch.phase == TouchPhase.Began) {
					startPos = touch.position;
				}else if (touch.phase == TouchPhase.Ended) {
					endPos = touch.position;

					float swipeDistVertical = (new Vector3 (0f, endPos.y, 0f) - new Vector3(0f, startPos.y, 0f)).magnitude;
					float swipeDistHorizontal = (new Vector3(endPos.x, 0f, 0f) - new Vector3(startPos.x, 0f, 0f)).magnitude;

					float swipDistance = (endPos - startPos).magnitude;

					if (swipDistance > minSwipeDist) {
						if (swipeDistVertical > swipeDistHorizontal) {
							float swipeValue = Mathf.Sign (endPos.y - startPos.y);
							if (swipeValue > 0) {//north
								direction = 0;
								zPos_new++;
							} else {//south
								direction = 2;
								zPos_new--;
							}
						} else {
							float swipeValue = Mathf.Sign (endPos.x - startPos.x);
							if (swipeValue > 0) {//east
								direction = 1;
								xPos_new++;
							} else {//west
								direction = 3;
								xPos_new--;
							}
						}
					}
				}
			}

			if (xPos != xPos_new || zPos != zPos_new) {
				if (GameObject.Find (xPos_new + "_" + zPos_new) != null) {
					GameObject room = GameObject.Find (xPos + "_" + zPos);
					xPos = xPos_new;
					zPos = zPos_new;

					if (room.GetComponent<Room> ().doors [direction]) {
						room = GameObject.Find (xPos + "_" + zPos);
						transform.position = room.transform.position;
						//after movement check if movement is still possible
						GameObject.Find ("ActiveDungeon").GetComponent<ActiveDungeon> ().openCloseRooms ();
					}
				}
			}
		}
	}

	public void playerAttack(){
		if (combatMode) {
			Room currentRoom = GameObject.Find (xPos + "_" + zPos).GetComponent<Room>();

			GameObject[] alive = new GameObject[currentRoom.enemies.Length - 1];

			for (int i = 0; i < currentRoom.enemies.Length - 1; i++) {
				alive [i] = currentRoom.enemies [i];
			}

			Destroy (currentRoom.enemies [currentRoom.enemies.Length - 1]);
			currentRoom.enemies = alive;

			if (currentRoom.enemies.Length == 0) {
				GameObject.Find ("ActiveDungeon").GetComponent<ActiveDungeon> ().openCloseRooms ();
			}
		}
	}
}
