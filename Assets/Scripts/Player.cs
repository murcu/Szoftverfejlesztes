using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Text nameText;

	public float easting;
	public float northing;

	public float easting_offset;
	public float northing_offset;

	private CharacterData data;

	void Start(){
		data = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().player;
		nameText.text = data.characterName;
	}

	public void setOffset(float e, float n){
		easting_offset = e;
		northing_offset = n;
	}

	public void updateUTM(float e, float n){
		easting = e;
		northing = n;
		Vector3 pos = new Vector3 (-(easting_offset-easting), transform.position.y, -(northing_offset - northing));

		if (pos != transform.position) {
			//move and play animation
			transform.position = Vector3.Lerp (transform.position, pos, Time.deltaTime*20f);
		}
	}

	void Update(){		
	}
		
}
