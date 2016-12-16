using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Text nameText;
	public Text playerHp;
	public Text playerCoin;

	public float easting;
	public float northing;

	public float easting_offset;
	public float northing_offset;

	private CharacterData data;
	Animator anim;

	void Start(){
		data = GameObject.FindGameObjectWithTag ("data").GetComponent<DataManager> ().player;
		nameText.text = data.characterName;

		anim = transform.FindChild("MorphedHumanDoll").GetComponent<Animator> ();
	}

	public void setOffset(float e, float n){
		easting_offset = e;
		northing_offset = n;
	}



	//move the player in MapView
	public void updateUTM(float e, float n){
		easting = e;
		northing = n;

		//move and animate
		Vector3 pos = new Vector3 (-(easting_offset-easting), transform.position.y, -(northing_offset - northing));
		anim.SetBool ("Moving", true);
		transform.position = Vector3.Lerp (transform.position, pos, Time.deltaTime * 20f);

		if (transform.position == pos) {
			anim.SetBool ("Moving", false);
		}
	}

	void Update(){
		playerHp.text = data.currHealth + " / " + data.maxHealth; 
		playerCoin.text = data.points + "";
	}		
}
