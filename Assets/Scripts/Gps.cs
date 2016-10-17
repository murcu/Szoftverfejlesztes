using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gps : MonoBehaviour {

	public Text tx;
	private int counter = 0;

	public float lat = 47.1257137f;
	public float lon = 17.7671641f;
	private Vector2 touchPos;
	public int zoom, size;
	public string key;

	public float roationSpeed;
	public Material material;

	// Use this for initialization
	IEnumerator Start () {
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
		{
			tx.text = "not enabled...";
		}

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		tx.text = "waiting...";
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1) {
			tx.text = "init time out...";
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed) {
			tx.text = "service failed...";
		}
		// Access granted and location value could be retrieved
		else{
			counter++;
			lat = Input.location.lastData.latitude;
			lon = Input.location.lastData.longitude;
			tx.text = "times: " + counter + " lat: " + lat + " lon: " + lon;
		}
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
		StartCoroutine ("loadTile");
	}

	public IEnumerator getLocation(){
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
		{
			tx.text = "not enabled...";
		}

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		tx.text = "waiting...";
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1) {
			tx.text = "init time out...";
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed) {
			tx.text = "service failed...";
		}
		// Access granted and location value could be retrieved
		else{
			counter++;
			float lat = Input.location.lastData.latitude;
			float lon = Input.location.lastData.longitude;
			tx.text = "times: " + counter + " lat: " + lat + " lon: " + lon;
		}
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

	public void updateLocation(){
		StartCoroutine ("getLocation");
		StartCoroutine ("loadTile");
	}

	public IEnumerator loadTile(){
		if (lat == 0.0f && lon == 0.0f) {
			lat = 47.0881783f;
			lon = 17.9077427f;
		}
		string url = "https://maps.googleapis.com/maps/api/staticmap?center="+lat+","+lon+"&zoom="+zoom+"&size="+size+"x"+size+"&maptype=roadmap&key=";

		WWW www = new WWW (url+key);
		yield return www;

		Texture texture = www.texture;
		transform.GetComponent<Renderer>().material = material;
		transform.GetComponent<Renderer>().material.mainTexture = texture;		
	}


	public void touchDown(){
		touchPos = Input.GetTouch (0).position;
	}

	public void touchUp(){
		float swipeForce = touchPos.x - Input.GetTouch (0).position.x;
		if (Mathf.Abs (swipeForce) > 100) {
			if(swipeForce < 0)
				transform.Rotate (Vector3.up, swipeForce * roationSpeed * Mathf.Deg2Rad);
			else
				transform.Rotate (Vector3.up, swipeForce * roationSpeed * Mathf.Deg2Rad);

		}
		
	}
}