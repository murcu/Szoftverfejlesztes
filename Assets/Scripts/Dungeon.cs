using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Dungeon : MonoBehaviour {

	public string text;
	public Color textColor;
	public Color fieldColor;

	private GameObject decision;

	void Start(){
		decision = GameObject.Find ("Canvas").transform.FindChild("Decision").gameObject;
		decision.transform.GetComponent<Image> ().color = fieldColor;
		decision.SetActive (false);
	}

	void touchDown(){
		if (decision != null) {
			Text tx = decision.transform.FindChild ("Text").GetComponent<Text>();
			tx.text = text;
			tx.color = textColor;
			decision.SetActive (true);
		}
	}

}