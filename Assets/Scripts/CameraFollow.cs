using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform player;
	public Vector3 offset;

	private Transform target;

	// Use this for initialization
	void Start () {
		target = player;
		transform.position = target.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target);
	}
}
