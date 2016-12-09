using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Gps : MonoBehaviour {
	
	public Material material;

	public float lat;
	public float lon;
	public int zoom, size;
	public string key;

	public float roationSpeed;

	private float lat_new, lon_new;

	private const float EARTH_RADIOUS = 6317f;
	private float distance;

	private float easting;
	private float northing;

	public float easting_offset;
	public float northing_offset;

	private bool isEnabled = false;

	// Use this for initialization
	IEnumerator Start () {
		distance = 0f;
		if (!Input.location.isEnabledByUser) {
			Debug.Log ("not enabled");
		}else{
			Input.location.Start(5f, 5f);
			int maxWait = 15;
			while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
				yield return new WaitForSeconds(1);
				maxWait--;
			}
			if (maxWait == 1) {
			}else if (Input.location.status == LocationServiceStatus.Failed) {
			}else{
				isEnabled = true;
				lat_new = Input.location.lastData.latitude;
				lon_new = Input.location.lastData.longitude;
				if (lat_new == 0f && lon_new == 0f) {
					lat_new = lat;
					lon_new = lon;
				}
			}
		}
			
		convertLatLonToUTM (lat_new, lon_new);
		northing_offset = northing;
		easting_offset = easting;
		StartCoroutine ("loadTile");
		GameObject.Find ("Dungeon Manager").gameObject.GetComponent<DungeonManager> ().setOffset (easting_offset, northing_offset);
		GameObject.Find ("Player").gameObject.GetComponent<Player> ().setOffset (easting_offset, northing_offset);
		GameObject.Find ("Dungeon Manager").gameObject.GetComponent<DungeonManager> ().init ();
	}

	void Update(){
		if (isEnabled) {			
			float deltaDistance = Harversine (lat_new, lon_new) * 1000f;
			if(deltaDistance > 0f){
				distance += deltaDistance;
			}

			convertLatLonToUTM (lat_new, lon_new);
			GameObject.Find ("Player").gameObject.GetComponent<Player> ().updateUTM (easting, northing);
		}
	}

	public void changeLatLon(){
		lat_new = Input.location.lastData.latitude;
		lon_new = Input.location.lastData.longitude;
	}

	IEnumerator loadTile(){
		string url = "https://maps.googleapis.com/maps/api/staticmap?center="+lat_new+","+lon_new+"&zoom="+zoom+"&size="+size+"x"+size+"&maptype=roadmap&key=";

		WWW www = new WWW (url+key);
		yield return www;
		Texture2D texture = www.texture;
		//save the texture!
		transform.GetComponent<Renderer>().material = material;
		transform.GetComponent<Renderer>().material.mainTexture = texture;
	}

	void convertLatLonToUTM(float lat, float lon){
		int zone = (int) Mathf.Floor(lon/6+31);

		easting = 0.5f*Mathf.Log((1f+Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f))/(1f-Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f)))*0.9996f*6399593.62f/Mathf.Pow((1f+Mathf.Pow(0.0820944379f, 2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f), 2f)), 0.5f)*(1f+ Mathf.Pow(0.0820944379f,2f)/2f*Mathf.Pow((0.5f*Mathf.Log((1f+Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f))/(1f-Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin(lon*Mathf.PI/180f-(6f*zone-183f)*Mathf.PI/180f)))),2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f)/3f)+500000f;
		easting = (float)Mathf.Round(easting*100f)*0.01f;
		northing = (Mathf.Atan(Mathf.Tan(lat*Mathf.PI/180f)/Mathf.Cos((lon*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))-lat*Mathf.PI/180f)*0.9996f*6399593.625f/Mathf.Sqrt(1f+0.006739496742f*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))*(1f+0.006739496742f/2f*Mathf.Pow(0.5f*Mathf.Log((1f+Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin((lon*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))/(1f-Mathf.Cos(lat*Mathf.PI/180f)*Mathf.Sin((lon*Mathf.PI/180f-(6f*zone -183f)*Mathf.PI/180f)))),2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))+0.9996f*6399593.625f*(lat*Mathf.PI/180f-0.005054622556f*(lat*Mathf.PI/180f+Mathf.Sin(2f*lat*Mathf.PI/180f)/2f)+4.258201531e-05f*(3f*(lat*Mathf.PI/180f+Mathf.Sin(2f*lat*Mathf.PI/180f)/2f)+Mathf.Sin(2f*lat*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))/4f-1.674057895e-07f*(5f*(3f*(lat*Mathf.PI/180f+Mathf.Sin(2f*lat*Mathf.PI/180f)/2f)+Mathf.Sin(2f*lat*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))/4f+Mathf.Sin(2f*lat*Mathf.PI/180f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f)*Mathf.Pow(Mathf.Cos(lat*Mathf.PI/180f),2f))/3f);
		northing = (float)Mathf.Round(northing*100f)*0.01f;
	}

	float Harversine(float lat_old, float lon_old){
		lat_new = Input.location.lastData.latitude;
		lon_new = Input.location.lastData.longitude;

		float deltaLatitude = (lat_new - lat_old) * Mathf.Deg2Rad;
		float deltaLongitude = (lon_new - lon_old) * Mathf.Deg2Rad;

		float a = Mathf.Pow (Mathf.Sin (deltaLatitude / 2), 2) + Mathf.Cos (lat_old * Mathf.Deg2Rad) * Mathf.Cos(lat_new * Mathf.Deg2Rad) * Mathf.Pow(Mathf.Sin(deltaLongitude/2), 2);

		float c = 2 * Mathf.Atan2 (Mathf.Sqrt(a), Mathf.Sqrt(1-a));
		return EARTH_RADIOUS * c;
	}

	public void updateLoc(){
		lat_new = Input.location.lastData.latitude;
		lon_new = Input.location.lastData.longitude;
	}
}