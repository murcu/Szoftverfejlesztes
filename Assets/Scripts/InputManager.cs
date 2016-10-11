using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	public LayerMask touchInputMask;

	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;
	private RaycastHit hit;

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear ();

			foreach (Touch touch in Input.touches) {
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				if (Physics.Raycast (ray, out hit, touchInputMask)) {
					GameObject recipent = hit.transform.gameObject;
					touchList.Add (recipent);

					if(touch.phase == TouchPhase.Began){
						recipent.SendMessage ("touchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Ended){
						recipent.SendMessage ("touchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved){
						recipent.SendMessage ("toucStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Canceled){
						recipent.SendMessage ("touchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				} 		
			}
			foreach(GameObject g in touchesOld){
				if(!touchList.Contains(g)){
					g.SendMessage ("touchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
