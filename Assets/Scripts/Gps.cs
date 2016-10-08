using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gps : MonoBehaviour {

	public Text tx;
	private int counter = 0;

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
			float lat = Input.location.lastData.latitude;
			float lon = Input.location.lastData.longitude;
			tx.text = "times: " + counter + " lat: " + lat + " lon: " + lon;
		}
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
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

	public void what(){
		StartCoroutine ("getLocation");
	}
}