using UnityEngine;
using System.Collections;

public class DungeonManager : MonoBehaviour {

	public GameObject dungeonPrefab;
	public DungeonData[] dungeons;

	private float easting_offset;
	private float northing_offset;

	// Use this for initialization
	void Start () {
	}

	public void init(){
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
