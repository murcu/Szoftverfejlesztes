using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float easting;
	public float northing;

	void Start(){
	}

	void Update(){
		transform.position = new Vector3 (easting, transform.position.y, northing);
	}

	public void setEasting(float easting){
		Debug.Log ("easting set");
		this.easting = easting;
	}

	public void setNorthing(float northing){
		Debug.Log ("northing set");
		this.northing = northing;
	}
}
