using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public float minSwipeDist;
	public float speed;

	private Camera cameraMain;
	private Vector3 startPos;
	private Vector3 endPos;

	void Start(){
		cameraMain = GameObject.Find ("Camera").GetComponent<Camera> ();
	}

	void Update () {
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startPos = touch.position;
			}else if (touch.phase == TouchPhase.Moved) {
				endPos = touch.position;

				float swipDistance = (endPos - startPos).magnitude;

				if (swipDistance > minSwipeDist) {
					Vector2 vec = endPos - startPos;
					Vector3 pos = new Vector3 (
						Mathf.Clamp(vec.x, -200f, 200f),
						0f,
						Mathf.Clamp(vec.y, -200f, 200f)
					);
					transform.position = Vector3.Lerp (transform.position, pos, Time.deltaTime);
				}
			}
		}else if (Input.touchCount == 2) {
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if (cameraMain.orthographic) {
				cameraMain.orthographicSize += deltaMagnitudeDiff * 0.5f;
				cameraMain.orthographicSize = Mathf.Max (cameraMain.orthographicSize, 50f);
			} else {
				cameraMain.fieldOfView += deltaMagnitudeDiff * 0.5f;
				cameraMain.fieldOfView = Mathf.Clamp (GameObject.Find ("Camera").GetComponent<Camera> ().fieldOfView, 60f, 75f);
			}
		}
	}

	public void cameraToPlayer(){
		Transform player = GameObject.Find ("Player").transform;
		transform.position = new Vector3 (player.position.x, 0f, player.position.z);
	}
}
