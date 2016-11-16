using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

	public LayerMask touchInputMask;

	public float minSwipeDist;
	public float roatationSpeed;

	private Vector2 swipeStartPos;

	private RaycastHit hit;

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			foreach (Touch touch in Input.touches) {
				if(touch.phase == TouchPhase.Began){
					swipeStartPos = touch.position;
				}

				if(touch.phase == TouchPhase.Moved){
					float swipeDist = Vector3.Distance(touch.position, swipeStartPos);

					if(swipeDist > minSwipeDist){
						float swipeSign = Mathf.Sign (touch.position.x - swipeStartPos.x) * -1f;
						GameObject.Find ("World").transform.Rotate (Vector3.up, swipeSign*roatationSpeed*Time.deltaTime, Space.Self);
					}
				}
			}
		}
	}
}
