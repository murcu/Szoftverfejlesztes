using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float easting;
	public float northing;

	public float easting_offset;
	public float northing_offset;

	void Start(){
	}

	public void setOffset(float e, float n){
		easting_offset = e;
		northing_offset = n;
	}

	public void updateUTM(float e, float n){
		easting = e;
		northing = n;

		Vector3 pos = new Vector3 (-(easting_offset-easting), transform.position.y, -(northing_offset - northing));
		transform.position = pos;
	}

	void Update(){		
	}
		
}
