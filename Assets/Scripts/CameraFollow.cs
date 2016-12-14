using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.position.x + offset.x, offset.y, target.position.z + offset.z);	
	}
}
