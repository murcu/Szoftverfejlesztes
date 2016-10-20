using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	public LayerMask touchInputMask;

	public float minSwipeDist;
	public float roatationSpeed;

	private Vector2 swipeStartPos;

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
						swipeStartPos = touch.position;
					}
					if(touch.phase == TouchPhase.Ended){
					}
					if(touch.phase == TouchPhase.Moved){
						float swipeDist = Vector3.Distance(touch.position, swipeStartPos);

						if(swipeDist > minSwipeDist){
							float swipeSign = Mathf.Sign (touch.position.x - swipeStartPos.x) * -1f;
							GameObject.Find ("World").transform.Rotate (Vector3.up, swipeSign*roatationSpeed*Time.deltaTime, Space.World);
						}
					}
					if(touch.phase == TouchPhase.Stationary){						
					}
					if(touch.phase == TouchPhase.Canceled){
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
