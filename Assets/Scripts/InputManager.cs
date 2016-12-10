using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	public LayerMask mask;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches) {
			Ray ray = GameObject.Find ("Camera").GetComponent<Camera> ().ScreenPointToRay (touch.position);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, mask)){
				GameObject recipent = hit.transform.gameObject;

				if(touch.phase == TouchPhase.Ended){
					recipent.SendMessage("openMessage", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
