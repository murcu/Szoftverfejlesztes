using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public float speed = 10.0f;
	public float amplitude = 1.0f;
	public float frequency = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
		transform.position += amplitude*(Mathf.Sin(2*Mathf.PI*frequency*Time.time) - Mathf.Sin(2*Mathf.PI*frequency*(Time.time - Time.deltaTime)))*transform.up;
	}
}
