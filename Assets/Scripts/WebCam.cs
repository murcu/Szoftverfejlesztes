using UnityEngine;
using System.Collections;

public class WebCam : MonoBehaviour {

	WebCamTexture mCamera = null;

	// Use this for initialization
	void Start () {
		mCamera = new WebCamTexture ();

		transform.GetComponent<Renderer> ().material.mainTexture = mCamera;
		mCamera.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
