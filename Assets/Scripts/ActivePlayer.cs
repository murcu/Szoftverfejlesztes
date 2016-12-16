using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivePlayer : MonoBehaviour {

	public Text nameText;
	public Text playerHp;
	public Text playerCoin;

	public CharacterData data;

	private Vector3 startPos;
	private Vector3 endPos;
	public float minSwipeDist;
	public int xPos, zPos;

	public bool combatMode = false;
	public bool defend = false;

	private bool startRead = false;

	Animator anim;

	// Use this for initialization
	void Start () {
		data = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().player;
		nameText.text = data.characterName;

		anim = transform.FindChild("MorphedHumanDoll").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		playerHp.text = data.currHealth + " / " + data.maxHealth; 
		playerCoin.text = data.points + "";

		if (combatMode) {
			startRead = false;
		}

		if (!combatMode) {
			int xPos_new = xPos;
			int zPos_new = zPos;
			int direction = -1;

			if (Input.touchCount == 1) {
				Touch touch = Input.GetTouch (0);

				if (touch.phase == TouchPhase.Began) {
					startPos = touch.position;
					startRead = true;
				}
				if (touch.phase == TouchPhase.Ended && startRead) {
					endPos = touch.position;
					float swipeDistVertical = (new Vector3 (0f, endPos.y, 0f) - new Vector3(0f, startPos.y, 0f)).magnitude;
					float swipeDistHorizontal = (new Vector3(endPos.x, 0f, 0f) - new Vector3(startPos.x, 0f, 0f)).magnitude;

					float swipDistance = (endPos - startPos).magnitude;
					if (swipDistance > minSwipeDist) {
						if (swipeDistVertical > swipeDistHorizontal) {
							float swipeValue = Mathf.Sign (endPos.y - startPos.y);
							if (swipeValue > 0) {//north
								Debug.Log("up");
								direction = 0;
								zPos_new++;
							} else {//south
								Debug.Log("down");
								direction = 2;
								zPos_new--;
							}
						} else {
							float swipeValue = Mathf.Sign (endPos.x - startPos.x);
							if (swipeValue > 0) {//east
								Debug.Log("right");
								direction = 1;
								xPos_new++;
							} else {//west
								Debug.Log("left");
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

						Vector3 relativePos = room.transform.FindChild ("PlayerPos").transform.position - transform.position;
						Quaternion rotation = Quaternion.LookRotation (relativePos);

						transform.FindChild ("MorphedHumanDoll").transform.rotation = rotation;

						anim.SetBool ("Moving", true);
						transform.position = room.transform.FindChild ("PlayerPos").transform.position;

						anim.SetBool ("Moving", false);
						//after movement check if movement is still possible
						GameObject.Find ("ActiveDungeon").GetComponent<ActiveDungeon> ().openCloseRooms ();
					}
				}
			}
		}
	}

	public void playerAttack(){
		if (combatMode && !defend) {
			anim.Play ("Attack");
			Room currentRoom = GameObject.Find (xPos + "_" + zPos).GetComponent<Room>();

			Enemy currentEnemy = currentRoom.enemies [currentRoom.enemies.Length - 1].GetComponent<Enemy> ();

			currentEnemy.HP -= 2;
		}
		anim.Play ("Idle");
	}

	public void playerDefend(){
		Debug.Log ("defend");
		/*if (!defend) {
			float startTime = Time.time;
			defend = true;
			Debug.Log (defend + " " + Time.time);
			while (Time.time != startTime + 15) {
			}
			defend = false;
			Debug.Log (defend + " " + Time.time);
		}*/
	}
}
