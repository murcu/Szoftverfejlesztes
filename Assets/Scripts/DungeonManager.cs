using UnityEngine;
using System.Collections;

public class DungeonManager : MonoBehaviour {

	public Transform player;

	[Range(0f, 500f)]
	public float dungeonOpenDistance;
	public GameObject dungeonPrefab;
	private DungeonData[] dungeons;

	private float easting_offset;
	private float northing_offset;

	// Use this for initialization
	void Start () {		
	}

	void Update(){
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild (i);
			float distance = Vector3.Distance (player.position, child.position);

			if (distance < dungeonOpenDistance) {
				child.GetComponent<Dungeon> ().open = true;
			} else {
				child.GetComponent<Dungeon> ().open = false;
			}
		}
	}

	public void init(){
		dungeons = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().dungeons;
		for (int i = 0; i < dungeons.Length; i++) {
			if (!dungeons [i].completed) {
				GameObject obj = Instantiate (dungeonPrefab);
				obj.name = dungeons [i].dungeonName;
				obj.transform.parent = transform;

				obj.transform.localScale = new Vector3(10f, 10f, 10f);
				obj.transform.position = new Vector3(0f, 5f, 0f);

				obj.transform.GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f, 1f);

				obj.GetComponent<Dungeon> ().setData (dungeons [i]);
				obj.GetComponent<Dungeon> ().setOffset (easting_offset, northing_offset);
			}
		}
	}

	public void setOffset(float e, float n){
		easting_offset = e;
		northing_offset = n;
	}
}
