using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gps : MonoBehaviour {

	public Text tx;
	public Material material;

	public float lat;
	public float lon;
	public int zoom, size;
	public string key;

	public float roationSpeed;

	private int counter = 0;
	private float lat_old, lon_old;
	private float lat_new, lon_new;

	private float easting;
	private float northing;

	public float easting_offset;
	public float northing_offset;

	// Use this for initialization
	IEnumerator Start () {
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser){
			tx.text = "not enabled...";
		}

		// Start service before querying location
		Input.location.Start(2.0f, 1.0f);

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
			lat_new = Input.location.lastData.latitude;
			lon_new = Input.location.lastData.longitude;
		}
	}

	void Update(){
		tx.text = lat_new + " " + lon_new;
	}

	public IEnumerator loadTile(){
		string url = "https://maps.googleapis.com/maps/api/staticmap?center="+lat+","+lon+"&zoom="+zoom+"&size="+size+"x"+size+"&maptype=roadmap&key=";

		WWW www = new WWW (url+key);
		yield return www;
		Texture texture = www.texture;
		transform.GetComponent<Renderer>().material = material;
		transform.GetComponent<Renderer>().material.mainTexture = texture;
		Debug.Log ("map loaded");
	}

	public void updateLocation(){
		lat_new = lat;
		lon_new = lon;
	}

	public void convertLatLonToUTM(){
		int zone = (int) Mathf.Floor(lon_old/6+31);

		easting = 0.5f*Mathf.Log((1f+Mathf.Cos(lat_old*Mathf.PI/180f)*Mathf.Sin(lon_old*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f))/(1f-Mathf.Cos(lat_old*Mathf.PI/180f)*Mathf.Sin(lon_old*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f)))*0.9996f*6399593.62f/Mathf.Pow((1f+Mathf.Pow(0.0820944379f, 2f)*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f), 2f)), 0.5f)*(1f+ Mathf.Pow(0.0820944379f,2f)/2f*Mathf.Pow((0.5f*Mathf.Log((1f+Mathf.Cos(lat_old*Mathf.PI/180f)*Mathf.Sin(lon_old*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f))/(1f-Mathf.Cos(lat_old*Mathf.PI/180f)*Mathf.Sin(lon_old*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f)))),2f)*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f),2f)/3f)+500000f;
		easting = (float)Mathf.Round(easting*100f)*0.01f;
		northing = (Mathf.Atan(Mathf.Tan(lat_old*Mathf.PI/180f)/Mathf.Cos((lon_old*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))-lat_old*Mathf.PI/180f)*0.9996f*6399593.625f/Mathf.Sqrt(1f+0.006739496742f*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f),2f))*(1f+0.006739496742f/2f*Mathf.Pow(0.5f*Mathf.Log((1f+Mathf.Cos(lat_old*Mathf.PI/180f)*Mathf.Sin((lon_old*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))/(1f-Mathf.Cos(lat_old*Mathf.PI/180f)*Mathf.Sin((lon_old*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))),2f)*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f),2f))+0.9996f*6399593.625f*(lat_old*Mathf.PI/180f-0.005054622556f*(lat_old*Mathf.PI/180f+Mathf.Sin(2f*lat_old*Mathf.PI/180f)/2f)+4.258201531e-05f*(3f*(lat_old*Mathf.PI/180f+Mathf.Sin(2f*lat_old*Mathf.PI/180f)/2f)+Mathf.Sin(2f*lat_old*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f),2f))/4f-1.674057895e-07f*(5f*(3f*(lat_old*Mathf.PI/180f+Mathf.Sin(2f*lat_old*Mathf.PI/180f)/2f)+Mathf.Sin(2f*lat_old*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f),2f))/4f+Mathf.Sin(2f*lat_old*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f),2f)*Mathf.Pow(Mathf.Cos(lat_old*Mathf.PI/180f),2f))/3f);
		northing = (float)Mathf.Round(northing*100f)*0.01f;
	}


}